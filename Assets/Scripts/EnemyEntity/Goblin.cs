using Unity.Mathematics;
using UnityEngine;

public class Goblin : MonoBehaviour, IDamageable
{
    //--- Dependencies ---
    [Header("Settings")]
    [SerializeField] EnemyData _baseData;
    private PatrolRoute _patrol;

    //--- Fields ----
    private string _name;
    private int _maxHP;
    private int _currentHP;
    private int _damage;
    private Vector2 _enemyPos; 
    private EnemyState _currentState;
    


    //--- Detection ---
    [Header("Detection Settings")]
    [SerializeField] private float _detectionRange;
    [SerializeField] private LayerMask _attackableTargets;
    [SerializeField] private Vector2 _boxCastSize;
    [SerializeField] private Color _color;

    private float _dirX;

    public void TakeDamge(int damage)
    {
        _currentHP -= damage;

        if (_currentHP <= 0)
        {
            Die();
        }
    }

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerEntity player = collision.gameObject.GetComponentInChildren<PlayerEntity>();
        if (player == null)
        {
            return;
        }
        player.TakeDamge(_damage);
       
        
    }

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

    public void Die()
    {
#if DEBUG
        Debug.Log($"{_name} got killed.");
#endif
        Destroy(gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
#if UNITY_EDITOR
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
