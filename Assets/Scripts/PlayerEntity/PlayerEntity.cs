/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : PlayerEntity.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Represents the core player entity.
* Manages health, slime type state, respawn logic,
* evolution progression, and damage handling.
* Implements IDamageable to receive damage from enemies or projectiles.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

/// <summary>
/// Defines the available slime types the player can have.
/// Determines skill behavior and visual state.
/// </summary>
public enum SlimeType
{
    None,
    Fire,
}
public class PlayerEntity : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [Tooltip("Reference to the player's inventory for evolution tracking.")]
    [SerializeField] private Player_Inventory _inventory;

    [Tooltip("Maximum health points of the player.")]
    [SerializeField] private int _maxHP = 3;

    [Tooltip("Default spawn position used for respawning.")]
    [SerializeField] private Vector2 _spawnPosition = new Vector2(0f, 0.7f);

    [Tooltip("Required fire element amount to unlock fire evolution.")]
    [SerializeField] private int _evolutionBreakPoint = 5;

    [Tooltip("Indicates whether the player is currently in fire slime form.")]
    [SerializeField] private bool _isFireSlime = false;

    private SlimeType _type;
    private int _currentHP;   
    private bool _canChangeFire;
    private Vector2 _spawnPoint_1 = new Vector2(6.67f, 3.75f);
    private Vector2 _spawnPoint_2 = new Vector2(10.61f, 0.97f);
    private Vector2 _spawnPoint_3 = new Vector2(24.03f, 0.88f);
    private Animator _animator;

    /// <summary>
    ///  Gets the current slime type of the player.
    /// </summary>
    public SlimeType Type => _type;

    /// <summary>
    /// Gets the player's current health points.
    /// </summary>
    public int CurrentHP => _currentHP;

    /// <summary>
    /// Indicates whether the player has unlocked
    /// the ability to switch to fire slime.
    /// </summary>
    public bool CanChangeFire => _canChangeFire;

    /// <summary>
    /// Initializes health, slime type,
    /// animator reference, and evolution state.
    /// </summary>
    private void Awake()
    {       
        _currentHP = _maxHP;
        _type = SlimeType.None;
        _animator = GetComponent<Animator>();
        _canChangeFire = false;
    }

    /// <summary>
    /// Toggles the slime element between normal and fire.
    /// Only possible if the evolution requirement is met.
    /// Updates animator parameters and slime type state.
    /// </summary>
    public void ChangeSlimeElement()
    {       
        if (CanChangeFire)
        {
            _isFireSlime = !_isFireSlime;
            _animator.SetBool("isFireSlime", _isFireSlime);
            _type = _isFireSlime ? SlimeType.Fire : SlimeType.None;           
        }
    }

    /// <summary>
    /// Updates dynamic spawn position based on player progression,
    /// checks for death condition,
    /// and unlocks fire evolution when enough elements are collected.
    /// </summary>
    private void Update()
    {
        if (transform.position.x <= _spawnPoint_1.x)
        {
            _spawnPosition = _spawnPoint_1;
        }
        else if (transform.position.x <= _spawnPoint_2.x)
        {
            _spawnPosition = _spawnPoint_2;
        }
        else if (transform.position.x<=_spawnPoint_3.x)
        {
            _spawnPosition = _spawnPoint_3;
        }
                
        if (_currentHP<=0)
        {
           
            Die();
        }
        if (_inventory.FireElement >= _evolutionBreakPoint)
        {
            _canChangeFire = true;
        }
    }

    /// <summary>
    /// Applies incoming damage to the player.
    /// Reduces current HP and triggers respawn.
    /// </summary>
    /// <param name="damage">
    /// The amount of damage received.
    /// </param>
    public void TakeDamge(int damage)
    {
        _currentHP -= damage; 
        Respawn(_spawnPosition);
    }

    /// <summary>
    /// Handles player death.
    /// Transitions back to the main menu scene.
    /// </summary>
    public void Die()
    {        
        GameBootstrapper.Instance.GoMenu();
    }

    /// <summary>
    /// Respawns the player at the given position.
    /// </summary>
    /// <param name="spawnPosition">
    /// The world position where the player should reappear.
    /// </param>
    public void Respawn(Vector2 spawnPosition)
    {        
        transform.position = spawnPosition;
    }
}
