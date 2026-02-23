
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _range = 5f;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _tickRate = 0.2f;
    private float _tickTimer;


    private Rigidbody2D _rb;
    private Vector2 _startPosition;
    private int _damage;
    private float _dir;
    private SlimeType _type;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }
    private void FixedUpdate()
    {
        BulletBehaviour();
    }

    private void BulletBehaviour()
    {

        float distance = Vector2.Distance(_startPosition, transform.position);
        if (distance >= _range)
        {
            GameObject Bullet = GetComponent<GameObject>();
            Destroy(gameObject);
        }
    }

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

    public void StopBullet()
    {
        _rb.linearVelocity = Vector2.zero;
        
    }


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
