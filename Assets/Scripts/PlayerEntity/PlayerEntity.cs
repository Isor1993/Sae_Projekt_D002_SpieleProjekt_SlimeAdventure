using UnityEngine;

public enum SlimeType
{
    None,
    Fire,
}
public class PlayerEntity : MonoBehaviour, IDamageable
{
    [SerializeField] private Player_Inventory _inventory;
    [SerializeField] private int _maxHP = 1;
    [SerializeField] private Vector2 _spawnPosition=new Vector2(0f,0.7f);
    private SlimeType _type;
    private int _currentHP;
    private bool _isAlive;


    private void Awake()
    {
        _isAlive = true;
        _currentHP = _maxHP;
        _type = SlimeType.None;
    }

   

    private void Update()
    {
        if(!_isAlive)
        {
            Respawn(_spawnPosition);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
      
    }

    public void TakeDamge(int damage)
    {
        _currentHP -= damage;
        Debug.Log(_currentHP);

        if(_currentHP<=0)
        {
            Die();
        }
    }
    public void Die()
    {
        _isAlive = false;
        Debug.Log("Player Died");
    }

    public void Respawn(Vector2 spawnPosition)
    {
        _isAlive = true;
        transform.position= spawnPosition;

    }
}
