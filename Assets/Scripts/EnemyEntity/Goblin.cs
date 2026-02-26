/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : Goblin.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Represents a Goblin enemy with patrol and attack behavior.
* Uses PatrolRoute for movement and performs box-based detection
* to switch between Patrol and Attack states.
* Implements IDamageable and supports boss-specific behavior
* (activating a PortalGate when defeated).
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class Goblin : MonoBehaviour, IDamageable
{
    //--- Dependencies ---
    [Header("Settings")]
    [Tooltip("Base data for this enemy (HP, damage, speed, detection range, etc.).")]
    [SerializeField] EnemyData _baseData;

    [Tooltip("Reference to the PortalGate (used if this goblin is a boss).")]
    [SerializeField] PortalGate _portalGate;

    //--- Detection ---
    [Header("Detection Settings")]
    [Tooltip("Maximum horizontal detection range for target scanning.")]
    [SerializeField] private float _detectionRange;

    [Tooltip("Layer mask defining which targets can be detected and attacked.")]
    [SerializeField] private LayerMask _attackableTargets;

    [Tooltip("Size of the BoxCast used for detection.")]
    [SerializeField] private Vector2 _boxCastSize;

    [Tooltip("Gizmo color used to visualize the detection area.")]
    [SerializeField] private Color _color;

    //--- Fields ----
    private PatrolRoute _patrol;
    private Vector2 _enemyPos; 
    private EnemyState _currentState;
    private string _name;
    private int _maxHP;
    private int _currentHP;
    private int _damage;
    private bool _isAlive=true;
    private float _dirX;

    /// <summary>
    /// Returns whether the goblin is alive.
    /// </summary>
    public bool IsAlive => _isAlive;

    /// <summary>
    /// Applies incoming damage to the goblin.
    /// Triggers death if HP reaches zero.
    /// </summary>
    /// <param name="damage">Amount of damage received.</param>
    public void TakeDamge(int damage)
    {
        _currentHP -= damage;
        Debug.Log(_currentHP);

        if (_currentHP <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// nitializes enemy values from EnemyData
    /// and caches required components.
    /// </summary>
    private void Awake()
    {
        _patrol = GetComponent<PatrolRoute>();
        _name = _baseData.Name;
        _damage = _baseData.Damage;
        _maxHP = _baseData.MaxHP;
        _currentHP = _maxHP;
        _detectionRange = _baseData.DetectionRange;
        _attackableTargets = _baseData.TargetAttackable;
    }

    /// <summary>
    /// Checks for target detection and updates
    /// the movement state accordingly.
    /// </summary>
    private void FixedUpdate()
    {
        _dirX = _patrol.DirX;
        bool detected = CheckDetection();

        if (detected && _currentState != EnemyState.Attack)
        {
            _currentState = EnemyState.Attack;
            _patrol.SetEnemyState(_currentState,_enemyPos);
        }
        else if (!detected && _currentState != EnemyState.Patrol)
        {
            _currentState = EnemyState.Patrol;
            _patrol.SetEnemyState(_currentState);
        }
    }

    /// <summary>
    /// Deals damage to the player on collision.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerEntity player = collision.gameObject.GetComponentInChildren<PlayerEntity>();
        if (player == null)
        {
            return;
        }
        player.TakeDamge(_damage);

       
        
    }

    /// <summary>
    /// Performs a BoxCast in facing direction
    /// to detect attackable targets within range.
    /// </summary>
    /// <returns>True if a target was detected.</returns>
    private bool CheckDetection()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, _boxCastSize, 0f, Vector2.right * _dirX, _detectionRange, _attackableTargets);
        if (hit.collider != null)
        {
            _enemyPos = hit.collider.transform.position;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Handles goblin death logic.
    /// If this goblin is the boss, it notifies the PortalGate.
    /// </summary>
    public void Die()
    {
#if DEBUG
        Debug.Log($"{_name} got killed.");
#endif
        _isAlive = false;
        if (_name == "BossGoblin")
        {
            _portalGate.BossDied();
        }
        
        gameObject.SetActive(false);
    }


#if UNITY_EDITOR
    /// <summary>
    /// Draws a gizmo representing the detection area.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Vector2 origin = transform.position;
        Vector2 size = _boxCastSize;

        float width = _detectionRange + _boxCastSize.x;
        float height = _boxCastSize.y;

        Vector2 center = origin + Vector2.right * _dirX * (_detectionRange / 2);
        Gizmos.DrawWireCube(center, new Vector2(width, height));
    }
#endif
}
