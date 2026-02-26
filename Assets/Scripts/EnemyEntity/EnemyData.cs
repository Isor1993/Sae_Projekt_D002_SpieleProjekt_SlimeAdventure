/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : EnemyData.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Defines a ScriptableObject containing base configuration
* data for enemy entities.
* Stores health, damage, movement, detection values,
* and target layer information used at runtime.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Settings")]
    [Tooltip("Display name of the enemy.")]
    [SerializeField] private string _name;

    [Tooltip("Maximum health points of the enemy.")]
    [SerializeField] private int _maxHP;

    [Tooltip("Damage dealt to the player on collision.")]
    [SerializeField] private int _damage;

    [Tooltip("Movement speed used during patrol.")]
    [SerializeField] private int _speed;

    [Tooltip("Movement speed used while chasing a target.")]
    [SerializeField] private float _huntingSpeed;

    [Tooltip("Maximum detection range for target scanning.")]
    [SerializeField] private float _detectionRange;

    [Tooltip("Layer mask defining which objects can be detected and attacked.")]
    [SerializeField] private LayerMask _targetAttackable;

    /// <summary>
    /// Gets the display name of the enemy.
    /// </summary>
    public string Name => _name;

    /// <summary>
    /// Gets the maximum health points of the enemy.
    /// </summary>
    public int MaxHP => _maxHP;

    /// <summary>
    /// Gets the damage value inflicted on valid targets.
    /// </summary>
    public int Damage => _damage;

    /// <summary>
    /// Gets the patrol movement speed.
    /// </summary>
    public int Speed => _speed;

    /// <summary>
    /// Gets the movement speed used during attack/chase state.
    /// </summary>
    public float HuntingSpeed => _huntingSpeed;

    /// <summary>
    /// Gets the detection range used for target scanning.
    /// </summary>
    public float DetectionRange => _detectionRange;

    /// <summary>
    /// Gets the layer mask defining valid attackable targets.
    /// </summary>
    public LayerMask TargetAttackable => _targetAttackable;

 
}
