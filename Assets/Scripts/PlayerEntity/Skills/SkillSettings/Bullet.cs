using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _range = 5f;
    [SerializeField] private float _speed = 2f;
    private Skilldata _bulletData;

    private Rigidbody2D _rb;
    private Vector2 _startPosition;
    private bool _isMoving = true;
    private int _damage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
       
       
    }

    private void Start()
    {
        

        _rb.linearVelocity=transform.right*_speed;
    }
    private void FixedUpdate()
    {        
        
        float distance=Vector2.Distance(_startPosition,transform.position);
        if (distance >= _range)
        {
            StopBullet();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet hit: " + collision.name);
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            Debug.Log(_damage);
            damageable.TakeDamge(_damage);
            
        }
        StopBullet();
    }   

    public void StopBullet()
    {
        _isMoving = false;
        _rb.linearVelocity = Vector2.zero;
        _rb.simulated = false;
    }

    public void Initialize(int damage)
    {
        _damage=damage;
        Debug.Log($"{damage} bullet hit");
    }
}
