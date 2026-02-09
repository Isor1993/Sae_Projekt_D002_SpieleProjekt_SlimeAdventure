/*****************************************************************************
* Project : 2D Jump'n'Run Steuerung (K2, S2)
* File    : PlayerController.cs
* Date    : 25.12.2025
* Author  : Eric Rosenberg
*
* Description :
* Main player controller responsible for handling input, movement, jumping,
* state transitions (ground, wall, coyote time) and coordinating all related
* behaviour components.
*
* History :
* 25.12.2025 ER Created
******************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Data container holding all relevant state information required
/// to evaluate and execute jump behaviour.
/// </summary>
public struct JumpStateData
{
    public bool IsGrounded;
    public bool IsCoyoteActive;
    public bool IsTouchingWall;
    public bool MultiJumpEnabled;
    public bool WallJumpEnabled;
}

public class PlayerController : MonoBehaviour
{
    private JumpStateData JumpData;
    private float _horizentalInput = 0.0f;
    private float _coyoteTimeCounter = 0f;
    private float _jumpBufferCounter = 0f;
    private bool _isGrounded = false;
    private bool _wasGrounded = false;
    private bool _isTouchingWall = false;
    private bool _wasTouchingWall = false;
    private bool _isSprinting = false;

    /// <summary>
    /// True during the frame in which the player has just landed on the ground.
    /// </summary>
    public bool JustLanded => !_wasGrounded && _isGrounded;

    /// <summary>
    /// True during the frame in which the player has just left the ground.
    /// </summary>
    public bool JustLeftGround => _wasGrounded && !_isGrounded;

    /// <summary>
    /// True during the frame in which the player has just touched a wall.
    /// </summary>
    public bool JustHitWall => !_wasTouchingWall && _isTouchingWall;

    /// <summary>
    /// True during the frame in which the player has just left a wall.
    /// </summary>
    public bool JustLeftWall => _wasTouchingWall && !_isTouchingWall;

    [Header("Dependencies")]
    [SerializeField] private MoveConfig _moveConfig;

    [SerializeField] private JumpConfig _jumpConfig;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private WallCheck _wallCheck;

    private MoveBehaviour _movement;
    private JumpBehaviour _jumpBehaviour;

    [Header("Options")]
    [Tooltip("Activates Jump")]
    [SerializeField] private bool _jumpIsEnabled = true;

    [Tooltip("Activates Multi Jump")]
    [SerializeField] private bool _multiJumpEnabled = true;

    [Tooltip("Activates Wall Jump")]
    [SerializeField] private bool _wallJumpEnabled = true;

    // --- Input ----
    private PlayerInputActions _inputActions;

    private InputAction _move;
    private InputAction _jump;
    private InputAction _sprint;

    /// <summary>
    /// Initializes input mappings and behaviour components.
    /// </summary>
    private void Awake()
    {
        MappingInputAction();
        _movement = new MoveBehaviour(_moveConfig, _rb);
        _jumpBehaviour = new JumpBehaviour(_jumpConfig, _rb);
        JumpData = new JumpStateData();
    }

    /// <summary>
    /// Enables the input action map when the object becomes active.
    /// </summary>
    private void OnEnable()
    {
        _inputActions.Slime.Enable();
    }

    /// <summary>
    /// Disables the input action map when the object becomes inactive.
    /// </summary>
    private void OnDisable()
    {
        _inputActions.Slime.Disable();
    }

    /// <summary>
    /// Handles all physics-related updates and state transitions.
    /// </summary>
    private void FixedUpdate()
    {
        UpdateGroundState();
        UpdateWallState();
        ReduceCoyoteTimer();
        ReduceJumpBuffer();
        HandleGroundTransition();
        HandleWallTransition();
        _movement.SetGroundedState(_isGrounded);
        HandleMovement(_isSprinting);
        HandleJump();
    }

    /// <summary>
    /// Handles frame-based input updates.
    /// </summary>
    private void Update()
    {
        UpdateInput();
    }

    /// <summary>
    /// Handles transitions related to entering or leaving the ground.
    /// </summary>
    private void HandleGroundTransition()
    {
        if (JustLanded)
        {
            ResetGroundJumpCounter();
            ResetAirJumpCounter();
            Debug.Log("Player landed on Ground.");
        }
        if (JustLeftGround)
        {
            ResetCoyoteTimer();
        }
    }

    /// <summary>
    /// Handles transitions related to entering or leaving a wall.
    /// </summary>
    private void HandleWallTransition()
    {
        if (JustHitWall)
        {
            ResetAirJumpCounter();
            Debug.Log("Player landed on Wall.");
        }
        if (JustLeftWall)
        {
            ResetCoyoteTimer();
        }
    }

    /// <summary>
    /// Reads and updates all player input states.
    /// </summary>
    public void UpdateInput()
    {
        _horizentalInput = _move.ReadValue<float>();

        if (_jump.WasPressedThisFrame())
        {
            ResetJumpBuffer();
        }
        if (_sprint.IsPressed())
        {
            _isSprinting = true;
        }
        else
        {
            _isSprinting = false;
        }
    }

    /// <summary>
    /// Attempts to execute a jump based on buffered input and current state.
    /// </summary>
    private void HandleJump()
    {
        if (!_jumpIsEnabled)
            return;

        if (_jumpBufferCounter <= 0f)
            return;

        JumpData = BuildJumpData(JumpData);
        Debug.Log("kurz vor springen");
        if (_jumpBehaviour.Jump(JumpData))
        {
            Debug.Log(" gesprungen einmal");
            _jumpBufferCounter = 0f;
        }
    }

    /// <summary>
    /// Builds and returns the current jump state data.
    /// </summary>
    /// <param name="JumpData">
    /// Existing jump state data instance to populate.
    /// </param>
    /// <returns>
    /// Fully populated JumpStateData struct.
    /// </returns>
    private JumpStateData BuildJumpData(JumpStateData JumpData) //ok
    {
        JumpData.IsGrounded = _isGrounded;
        JumpData.IsCoyoteActive = IsCoyoteTimeActive();
        JumpData.IsTouchingWall = _isTouchingWall;
        JumpData.MultiJumpEnabled = _multiJumpEnabled;
        JumpData.WallJumpEnabled = _wallJumpEnabled;

        return JumpData;
    }

    /// <summary>
    /// Applies horizontal movement based on sprint state.
    /// </summary>
    /// <param name="isSprinting">
    /// Indicates whether sprint movement is active.
    /// </param>
    private void HandleMovement(bool isSprinting)
    {
        _movement.Move(_horizentalInput, isSprinting);
    }

    /// <summary>
    /// Resets the coyote time counter.
    /// </summary>
    private void ResetCoyoteTimer()
    {
        _coyoteTimeCounter = _jumpConfig.CoyoteTime;
    }

    /// <summary>
    /// Resets the airborne jump counter.
    /// </summary>
    private void ResetAirJumpCounter()
    {
        _jumpBehaviour.ResetJumpCountAir();
        Debug.Log("[JumpCountAir] is reseted.");
    }

    /// <summary>
    /// Resets the ground jump availability.
    /// </summary>
    private void ResetGroundJumpCounter()
    {
        _jumpBehaviour.ResetJumpCountGround();
        Debug.Log($"[JumpCountGround] is reseted.");
    }

    /// <summary>
    /// Decreases the coyote time counter over time.
    /// </summary>
    private void ReduceCoyoteTimer()
    {
        if ((!_isGrounded && !_isTouchingWall) && _coyoteTimeCounter > 0f)
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Checks whether coyote time is currently active.
    /// </summary>
    /// <returns>
    /// True if coyote time is active; otherwise, false.
    /// </returns>
    private bool IsCoyoteTimeActive()
    {
        return _coyoteTimeCounter > 0f;
    }

    /// <summary>
    /// Updates the grounded state and tracks ground transitions.
    /// </summary>
    private void UpdateGroundState()
    {
        _wasGrounded = _isGrounded;
        _isGrounded = _groundCheck.CheckGround();
    }

    /// <summary>
    /// Updates the wall contact state and tracks wall transitions.
    /// </summary>
    private void UpdateWallState()
    {
        _wasTouchingWall = _isTouchingWall;
        _isTouchingWall = _wallCheck.CheckWall();
    }

    /// <summary>
    /// Decreases the jump buffer timer over time.
    /// </summary>
    private void ReduceJumpBuffer()
    {
        if (_jumpBufferCounter > 0f)
        {
            _jumpBufferCounter -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Resets the jump buffer timer.
    /// </summary>
    private void ResetJumpBuffer()
    {
        _jumpBufferCounter = _jumpConfig.JumpBufferTime;
    }

    /// <summary>
    /// Maps all input actions to their corresponding references.
    /// </summary>
    private void MappingInputAction()
    {
        _inputActions = new PlayerInputActions();
        _move = _inputActions.Slime.Move;
        _jump = _inputActions.Slime.Jump;
        _sprint = _inputActions.Slime.Sprint;
    }
}