/*****************************************************************************
* Project : 2D Jump'n'Run Steuerung (K2, S2)
* File    : JumpBehaviour.cs
* Date    : 25.12.2025
* Author  : Eric Rosenberg
*
* Description :
* Handles all jump-related logic for the player, including ground jumps,
* coyote time jumps, wall jumps and multi-jumps while airborne.
*
* History :
* 25.12.2025 ER Created
******************************************************************************/

using UnityEngine;

public class JumpBehaviour
{
    // --- Dependencies ---
    private readonly JumpConfig _config;

    private readonly Rigidbody2D _rb;

    // --- Field ---
    private int _jumpCountAir;

    private bool _groundJumpAvailable = true;

    /// <summary>
    /// Initializes a new instance of the JumpBehaviour class.
    /// </summary>
    /// <param name="config">
    /// Reference to the JumpConfig containing jump-related configuration values.
    /// </param>
    /// <param name="rb">
    /// Reference to the Rigidbody2D used to apply jump physics.
    /// </param>
    public JumpBehaviour(JumpConfig config, Rigidbody2D rb)
    {
        _config = config;
        _rb = rb;
    }

    /// <summary>
    /// Attempts to perform a jump based on the provided jump state data.
    /// </summary>
    /// <param name="JumpState">
    /// Struct containing all relevant jump state information such as grounding,
    /// coyote time, wall contact and jump permissions.
    /// </param>
    /// <returns>
    /// True if a jump was successfully performed; otherwise, false.
    /// </returns>
    public bool Jump(JumpStateData JumpState)
    {
        if (CanJumpOnGround(JumpState.IsGrounded, JumpState.IsCoyoteActive))
        {
            PerformJumpPhysic();
            _groundJumpAvailable = false;
            Debug.Log("Player jumped from Ground.");
            return true;
        }
        if (CanWallJump(JumpState.IsTouchingWall, JumpState.IsGrounded, JumpState.WallJumpEnabled))
        {
            PerformJumpPhysic();
            _jumpCountAir++;
            Debug.Log("Player jumped from wall.");
            return true;
        }
        if (CanJumpInAir(JumpState.IsGrounded, JumpState.MultiJumpEnabled))
        {
            PerformJumpPhysic();
            _jumpCountAir++;
            Debug.Log("Player jumped in air.");
            return true;
        }
        return false;
    }

    /// <summary>
    /// Applies the vertical jump force to the Rigidbody2D.
    /// </summary>
    private void PerformJumpPhysic()
    {
        Vector2 currentVelocity = _rb.linearVelocity;
        currentVelocity.y = _config.JumpForce;
        _rb.linearVelocity = currentVelocity;
    }

    /// <summary>
    /// Determines whether a ground or coyote-time jump is allowed.
    /// </summary>
    /// <param name="isGrounded">
    /// Indicates whether the player is currently grounded.
    /// </param>
    /// <param name="isCoyoteAktive">
    /// Indicates whether coyote time is currently active.
    /// </param>
    /// <returns>
    /// True if a ground jump is allowed; otherwise, false.
    /// </returns>
    private bool CanJumpOnGround(bool isGrounded, bool isCoyoteAktive)
    {
        return _groundJumpAvailable && (isGrounded || isCoyoteAktive);
    }

    /// <summary>
    /// Determines whether an airborne multi-jump is allowed.
    /// </summary>
    /// <param name="isGrounded">
    /// Indicates whether the player is currently grounded.
    /// </param>
    /// <param name="multiJumpEnabled">
    /// Indicates whether multi-jumping is enabled.
    /// </param>
    /// <returns>
    /// True if an air jump is allowed; otherwise, false.
    /// </returns>
    private bool CanJumpInAir(bool isGrounded, bool multiJumpEnabled)
    {
        return !isGrounded && _jumpCountAir < _config.MaxJumpCountAir && multiJumpEnabled;
    }

    /// <summary>
    /// Determines whether a wall jump is allowed.
    /// </summary>
    /// <param name="isTouchingWall">
    /// Indicates whether the player is currently touching a wall.
    /// </param>
    /// <param name="isGrounded">
    /// Indicates whether the player is currently grounded.
    /// </param>
    /// <param name="wallJumpEnabled">
    /// Indicates whether wall jumping is enabled.
    /// </param>
    /// <returns>
    /// True if a wall jump is allowed; otherwise, false.
    /// </returns>
    private bool CanWallJump(bool isTouchingWall, bool isGrounded, bool wallJumpEnabled)
    {
        return !isGrounded && isTouchingWall && _jumpCountAir < _config.MaxJumpCountAir && wallJumpEnabled;
    }

    /// <summary>
    /// Resets the airborne jump counter.
    /// </summary>
    public void ResetJumpCountAir()
    {
        _jumpCountAir = 0;
    }

    /// <summary>
    /// Resets the ground jump availability.
    /// </summary>
    public void ResetJumpCountGround()
    {
        _groundJumpAvailable = true;
    }
}