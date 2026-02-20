
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _range = 5f;
    [SerializeField] private float _speed = 2f;


    private Rigidbody2D _rb;
    private Vector2 _startPosition;
    private int _damage;
    private float _dir;

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
            Destroy(Bullet);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet hit: " + collision.name);
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                damageable.TakeDamge(_damage);
                StopBullet();
            }

        }
        else if (collision.gameObject.GetComponent<Bullet>())
        {

        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void StopBullet()
    {
        _rb.linearVelocity = Vector2.zero;
        _rb.simulated = false;
    }


    public void Initialize(int damage, float dir)
    {
        _rb.simulated = true;
        _damage = damage;
        _dir = dir;
       
        _startPosition = transform.position;
        
        _rb.linearVelocity = new Vector2(_dir * _speed, _rb.linearVelocity.y);

    }
}
