/*****************************************************************************
* Project : 2D Jump'n'Run Steuerung (K2, S2)
* File    : MoveBehaviour.cs
* Date    : 25.12.2025
* Author  : Eric Rosenberg
*
* Description :
* Handles horizontal movement logic for the player, including ground and air
* movement, sprinting, acceleration, deceleration and speed clamping.
*
* History :
* 25.12.2025 ER Created
******************************************************************************/

using UnityEngine;

public class MoveBehaviour
{
    //--- Dependencies ---
    private readonly MoveConfig _config;

    private readonly Rigidbody2D _rb;

    //--- Fields ---
    private bool _isGrounded = false;

    /// <summary>
    /// Initializes a new instance of the MoveBehaviour class.
    /// </summary>
    /// <param name="config">
    /// Reference to the MoveConfig containing movement configuration values.
    /// </param>
    /// <param name="rb">
    /// Reference to the Rigidbody2D used to apply movement velocity.
    /// </param>
    public MoveBehaviour(MoveConfig config, Rigidbody2D rb)
    {
        _config = config;
        _rb = rb;
    }

    /// <summary>
    /// Applies horizontal movement based on the current grounded state and sprint input.
    /// </summary>
    /// <param name="inputX">
    /// Horizontal movement input value, typically ranging from -1 to 1.
    /// </param>
    /// <param name="isSprinting">
    /// Indicates whether sprint movement is currently active.
    /// </param>
    public void Move(float inputX, bool isSprinting)
    {
        Vector2 velocity;
        if (_isGrounded)
        {
            velocity = OnGround(inputX, isSprinting);
        }
        else
        {
            velocity = InAir(inputX, isSprinting);
        }
        _rb.linearVelocity = velocity;
    }

    /// <summary>
    /// Calculates horizontal velocity while the player is grounded.
    /// </summary>
    /// <param name="inputX">
    /// Horizontal movement input value.
    /// </param>
    /// <param name="isSprinting">
    /// Indicates whether sprint movement is active.
    /// </param>
    /// <returns>
    /// The calculated velocity vector to be applied to the Rigidbody2D.
    /// </returns>
    public Vector2 OnGround(float inputX, bool isSprinting)
    {
        float moveSpeed = inputX * _config.MoveSpeed;
        float sprintSpeed = inputX * _config.SprintSpeed;
        float targetSpeed = isSprinting ? sprintSpeed : moveSpeed;
        float MaxSpeed = isSprinting ? _config.MaxSprintSpeed : _config.MaxSpeed;
        var currentVelocity = _rb.linearVelocity;
        if (inputX != 0)
        {
            currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, targetSpeed, _config.Acceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, 0, _config.Deceleration * Time.fixedDeltaTime);
        }
        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -MaxSpeed, MaxSpeed);
        return currentVelocity;
    }

    /// <summary>
    /// Calculates horizontal velocity while the player is airborne.
    /// </summary>
    /// <param name="InputX">
    /// Horizontal movement input value.
    /// </param>
    /// <param name="isSprinting">
    /// Indicates whether sprint movement is active.
    /// </param>
    /// <returns>
    /// The calculated velocity vector to be applied to the Rigidbody2D.
    /// </returns>
    private Vector2 InAir(float InputX, bool isSprinting)
    {
        float airMaxSpeed = (isSprinting ? _config.MaxSprintSpeed : _config.MaxSpeed) * _config.AirControlFactor;
        float targetspeed = InputX * airMaxSpeed;
        var currentVelocity = _rb.linearVelocity;
        if (InputX != 0)
        {
            currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, targetspeed, _config.AirAcceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, 0, _config.AirDeceleration * Time.fixedDeltaTime);
        }
        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -airMaxSpeed, airMaxSpeed);
        return currentVelocity;
    }

    /// <summary>
    /// Sets whether the player is currently grounded.
    /// </summary>
    /// <param name="isGrounded">
    /// True if the player is touching the ground; otherwise, false.
    /// </param>
    public void SetGroundedState(bool isGrounded)
    {
        _isGrounded = isGrounded;
    }
}