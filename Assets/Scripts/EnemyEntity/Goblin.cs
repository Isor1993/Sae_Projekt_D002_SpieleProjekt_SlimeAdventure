using UnityEngine;

public class Goblin : MonoBehaviour,IDamageable
{
    [SerializeField] EnemyData _baseData;
    private string _name;
    private int _damage;
    private int _currentHP;
    private bool _isAlive;

    public void TakeDamge(int damage)
    {
        Debug.Log(_currentHP);
        _currentHP-=damage;
        Debug.Log(_currentHP);
        if (_currentHP<=0 )
        {
            Die();
        }
    }

    private void Awake()
    {
        _isAlive = true;
        _name=_baseData.Name;
        _damage = _baseData.Damage;
        _currentHP = _baseData.MaxHP;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerEntity player=collision.gameObject.GetComponentInChildren<PlayerEntity>();
        if (player == null)
        {
            return;
        }
        
            player.TakeDamge(_damage);
        
    }


    public void Die()
    {
        _isAlive = false;
        Debug.Log($"{_name} got killed.");
        this.gameObject.SetActive( false );
    }

}
