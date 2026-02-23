/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : PatrolRoute.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Controls a simple enemy movement state machine (Patrol / Attack).
* In Patrol state, the enemy moves along predefined route points.
* In Attack state, the enemy moves horizontally towards a target position.
* Includes optional gizmo visualization for patrol points in the editor.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Defines the available movement states for the enemy.
/// </summary>
public enum EnemyState
{
    Patrol,
    Attack
}

public class PatrolRoute : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Patrol route points in order (world positions).")]
    [SerializeField] private List<Vector2> _patrolPos = new List<Vector2>();
    [Tooltip("Enemy data ScriptableObject (e.g., speed).")]
    [SerializeField] private EnemyData _enemyData;

    [Header("Special Debug Settings")]
    [Tooltip("Defines whether the enemy is patrolling or chasing a target.")]
    [SerializeField] private EnemyState _enemyState = EnemyState.Patrol;
    [Tooltip("Target position used for chasing (Attack state). ")]
    [SerializeField] private Vector2 _enemyPos;

    //--- Gizmo Settings ---

    [Header("Gizmo Draw Settings")]
    [Tooltip("Size of the gizmo markers.")]
    [SerializeField] private Vector3 _cubeSize;
    [Tooltip("Color used for patrol route gizmos.")]
    [SerializeField] private Color _color;

    //--- Fields ---
    private Rigidbody2D _rb;
    private float _speed = 1;
    private int _indexPos = 0;
    private float _dirX;
    private float _huntingSpeed;
    private SpriteRenderer _spriteRenderer;
    private float _borderLeft;
    private float _borderRight;

    /// <summary>
    /// 
    /// </summary>
    public float DirX => _dirX;


    /// <summary>
    /// Caches required components and loads configuration values.
    /// </summary>
    private void Awake()
    {
        MappingData();

    }



    /// <summary>
    /// Loads configuration values from EnemyData and caches component references.
    /// Ensures speed is not zero to prevent a "stuck" enemy.
    /// </summary>
    private void MappingData()
    {
        _speed = _enemyData.Speed;
        _huntingSpeed= _enemyData.HuntingSpeed;
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_speed == 0f)
        {
#if DEBUG
            Debug.LogWarning("Speed cant be [0] is now [1}.");
#endif
            _speed = 1f;
        }
    }

    /// <summary>
    /// Runs movement logic based on the current enemy state.
    /// Uses FixedUpdate because movement is applied via physics velocity.
    /// </summary>
    private void FixedUpdate()
    {
        switch (_enemyState)
        {
            case EnemyState.Patrol:
                _speed = _enemyData.Speed;
                PatrolToNextPos();
                break;

            case EnemyState.Attack:
                _speed = _huntingSpeed;
                ChaseEnemy(_enemyPos);
                break;
        }
    }

    /// <summary>
    /// Moves the enemy horizontally toward the current patrol point.
    /// When the target is reached, advances to the next point
    /// and loops back to the first point after the last one.
    /// </summary>
    private void PatrolToNextPos()
    {
        if (_patrolPos.Count == 0)
            return;

        var Target = _patrolPos[_indexPos];
        float deltaX = Target.x - transform.position.x;

        if (Mathf.Abs(deltaX) < 0.1f)
        {
            transform.position = new Vector2(Target.x, transform.position.y);
            _rb.linearVelocity = new Vector2(0f, _rb.linearVelocity.y);

            _indexPos++;

            if (_indexPos >= _patrolPos.Count)
            {
                _indexPos = 0;
            }
            return;
        }

        _dirX = Mathf.Sign(deltaX);
        _spriteRenderer.flipX = _dirX <= 0f ? false : true;
        _rb.linearVelocity = new Vector2(_dirX * _speed, _rb.linearVelocity.y);
    }

    /// <summary>
    /// Moves the enemy horizontally toward the given target position.
    /// Intended for chasing behavior in Attack state.
    /// </summary>
    /// <param name="pos">
    /// Target position to chase (usually the player position).
    /// </param>
    private void ChaseEnemy(Vector2 enemyPos)
    {
        float clampedTargetX = Mathf.Clamp(enemyPos.x, _borderLeft, _borderRight);
        var deltaX = clampedTargetX - transform.position.x;
        if (Mathf.Abs(deltaX) < 0.1f)
        {
            _rb.linearVelocity = new Vector2(0f, _rb.linearVelocity.y);
            return;
        }

        _dirX = Mathf.Sign(deltaX);
        _spriteRenderer.flipX = _dirX <= 0f ? false : true;
        _rb.linearVelocity = new Vector2(_dirX * _speed, _rb.linearVelocity.y);
    }

    /// <summary>
    /// Sets the enemy state and updates the chase target position.
    /// Used to switch between Patrol and Attack at runtime.
    /// </summary>
    /// <param name="enemyState">
    /// The new enemy state.
    /// </param>
    /// <param name="pos">
    /// Target position used in Attack state.
    /// </param>
    public void SetEnemyState(EnemyState enemyState, Vector2 pos)
    {
        _enemyPos = pos;
        _enemyState = enemyState;
    }

    /// <summary>
    /// Sets the enemy state and updates the chase target position.
    /// Used to switch between Patrol and Attack at runtime.
    /// </summary>
    /// <param name="enemyState">
    /// The new enemy state.
    /// </param>    
    public void SetEnemyState(EnemyState enemyState)
    {
        _enemyState = enemyState;
    }


#if UNITY_EDITOR
    /// <summary>
    /// Draws gizmo markers for patrol points in the Scene view.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        foreach (var pos in _patrolPos)
        {
            Gizmos.DrawCube(new Vector3(pos.x, pos.y, 0f), _cubeSize);
        }
    }
#endif
}
