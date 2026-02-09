/*****************************************************************************
* Project : 2D Jump'n'Run Steuerung (K2, S2)
* File    : MoveConfig.cs
* Date    : 25.12.2025
* Author  : Eric Rosenberg
*
* Description :
* ScriptableObject that stores all movement-related configuration values,
* including walking speed, sprint speed, acceleration, deceleration and
* air control behaviour.
*
* History :
* 25.12.2025 ER Created
******************************************************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "MoveConfig", menuName = "Scriptable Objects/MoveConfig")]
public class MoveConfig : ScriptableObject
{
    [Header("MoveBehaviour Settings")]
    [Tooltip("Base movement speed when walking. Default : 2f")]
    [SerializeField] private float moveSpeed = 2f;

    [Tooltip("Base movement speed when sprinting. Default : 6f")]
    [SerializeField] private float sprintSpeed = 6f;

    [Tooltip("Maximum movement speed while sprinting. Default : 7f")]
    [SerializeField] private float maxSprintSpeed = 7f;

    [Tooltip("Maximum movement speed while walking. : 2.5f")]
    [SerializeField] private float maxMoveSpeed = 2.5f;

    [Tooltip("Acceleration rate applied on the ground when increasing speed. Default : 20f")]
    [SerializeField] private float acceleration = 20f;

    [Tooltip("Deceleration rate applied on the ground when reducing speed. Default : 20f")]
    [SerializeField] private float deceleration = 20f;

    [Tooltip("Acceleration rate applied while airborne. Default : 8f")]
    [SerializeField] private float airAcceleration = 8f;

    [Tooltip("Deceleration rate applied while airborne. Default : 2f")]
    [SerializeField] private float airDeceleration = 2f;

    [Tooltip("Factor controlling how much horizontal control the player has while airborne. Default: 0.8f")]
    [SerializeField] private float airControlFactor = 0.8f;

    /// <summary>
    /// Gets the base walking movement speed.
    /// </summary>
    public float MoveSpeed => moveSpeed;

    /// <summary>
    /// Gets the base sprinting movement speed.
    /// </summary>
    public float SprintSpeed => sprintSpeed;

    /// <summary>
    /// Gets the maximum sprinting movement speed.
    /// </summary>
    public float MaxSprintSpeed => maxSprintSpeed;

    /// <summary>
    /// Gets the maximum walking movement speed.
    /// </summary>
    public float MaxSpeed => maxMoveSpeed;

    /// <summary>
    /// Gets the acceleration rate applied while moving on the ground.
    /// </summary>
    public float Acceleration => acceleration;

    /// <summary>
    /// Gets the deceleration rate applied while moving on the ground.
    /// </summary>
    public float Deceleration => deceleration;

    /// <summary>
    /// Gets the acceleration rate applied while the player is airborne.
    /// </summary>
    public float AirAcceleration => airAcceleration;

    /// <summary>
    /// Gets the deceleration rate applied while the player is airborne.
    /// </summary>
    public float AirDeceleration => airDeceleration;

    /// <summary>
    /// Gets the air control factor influencing horizontal movement while airborne.
    /// </summary>
    public float AirControlFactor => airControlFactor;
}