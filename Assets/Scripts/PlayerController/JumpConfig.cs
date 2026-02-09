/*****************************************************************************
* Project : 2D Jump'n'Run Steuerung (K2, S2)
* File    : JumpConfig.cs
* Date    : 25.12.2025
* Author  : Eric Rosenberg
*
* Description :
* ScriptableObject holding all configuration values related to the player's
* jump behaviour, such as jump force, coyote time, jump buffering and air jumps.
*
* History :
* 25.12.2025 ER Created
******************************************************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "JumpConfig", menuName = "Scriptable Objects/JumpConfig")]
public class JumpConfig : ScriptableObject
{
    [Header("JumpBehaviour Settings")]
    [Tooltip("Defines the vertical force applied when the player jumps. Default : 6f")]
    [SerializeField] private float _jumpforce = 6f;

    [Tooltip("Defines the duration after leaving the ground in which a jump is still allowed. Default: 0.2f")]
    [SerializeField] private float _coyoteTime = 0.2f;

    [Tooltip("Defines how long a buffered jump input is stored before it expires. Fefault : 0.15f")]
    [SerializeField] private float _jumpBufferTime = 0.15f;

    [Tooltip("Defines how many jumps the player can perform while airborne.")]
    [SerializeField] private int _maxJumpCountAir = 1;

    /// <summary>
    /// Gets the vertical force applied to the Rigidbody when a jump is executed.
    /// </summary>
    public float JumpForce => _jumpforce;

    /// <summary>
    /// Gets the duration in seconds for which a buffered jump input remains valid.
    /// </summary>
    public float JumpBufferTime => _jumpBufferTime;

    /// <summary>
    /// Gets the maximum number of jumps that can be performed while the player is airborne.
    /// </summary>
    public int MaxJumpCountAir => _maxJumpCountAir;

    /// <summary>
    /// Gets the coyote time duration in seconds, allowing a jump shortly after leaving the ground.
    /// </summary>
    public float CoyoteTime => _coyoteTime;
}