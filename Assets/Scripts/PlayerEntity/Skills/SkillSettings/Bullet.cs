/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : Bullet.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Handles projectile behavior for player skills.
* Controls movement, range limitation, collision logic,
* and damage application depending on slime type.
* Supports instant-hit projectiles and fire-based
* damage-over-time behavior.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Maximum distance the projectile can travel before being destroyed.")]
    [SerializeField] private float _range = 5f;

    [Tooltip("Movement speed of the projectile.")]
    [SerializeField] private float _speed = 2f;

    [Tooltip("Time interval between damage ticks for fire-type projectiles.")]
    [SerializeField] private float _tickRate = 0.2f;

    private Rigidbody2D _rb;
    private SlimeType _type;
    private Vector2 _startPosition;

    private int _damage;
    private float _dir;
    private float _tickTimer;

    /// <summary>
    /// Caches the Rigidbody2D component required for physics-based movement.
    /// </summary>
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Called in fixed time intervals.
    /// Executes projectile range and lifetime checks.
    /// </summary>
    private void FixedUpdate()
    {
        BulletBehaviour();
    }

    /// <summary>
    /// Checks the traveled distance from the spawn position.
    /// If the projectile exceeds its defined range,
    /// it is destroyed.
    /// </summary>
    private void BulletBehaviour()
    {

        float distance = Vector2.Distance(_startPosition, transform.position);
        if (distance >= _range)
        {
            GameObject Bullet = GetComponent<GameObject>();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Handles continuous collision logic.
    /// Used for fire-type projectiles that apply
    /// damage over time while staying inside a collider.
    /// </summary>
    /// <param name="collision">
    /// The collider currently overlapping with the projectile.
    /// </param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_type == SlimeType.Fire)
        {
            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                if (!collision.gameObject.CompareTag("Player"))
                {
                    _tickTimer += Time.deltaTime;
                    if (_tickTimer > _tickRate)
                    {
                        damageable.TakeDamge(_damage);
                        _tickTimer = 0f;
                    }   
                }
            }
            else if (collision.gameObject.GetComponent<Bullet>())
            {

            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Handles initial collision logic.
    /// Applies instant damage for non-fire projectiles
    /// and destroys the projectile afterwards.
    /// </summary>
    /// <param name="collision">
    /// The collider that the projectile has entered.
    /// </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                if (_type == SlimeType.None)
                {
                    damageable.TakeDamge(_damage);
                    Destroy(gameObject);
                }
            }
        }
        else if (collision.gameObject.GetComponent<Bullet>())
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initializes the projectile after instantiation.
    /// Sets damage, direction, slime type,
    /// starting position, and initial velocity.
    /// </summary>
    /// <param name="damage">
    /// Damage value that will be applied to valid targets.
    /// </param>
    /// <param name="dir">
    /// Horizontal direction of travel (typically -1 or 1).
    /// </param>
    /// <param name="type">
    /// Slime type defining the projectile behavior.
    /// </param>
    public void Initialize(int damage, float dir, SlimeType type)
    {
        _rb.simulated = true;
        _damage = damage;
        _dir = dir;
        _type = type;
        _startPosition = transform.position;

        _rb.linearVelocity = new Vector2(_dir * _speed, _rb.linearVelocity.y);
    }
}
