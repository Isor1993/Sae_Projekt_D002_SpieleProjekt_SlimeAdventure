using TMPro;
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
    [SerializeField] private Vector2 _spawnPosition = new Vector2(0f, 0.7f);
    private SlimeType _type;
    private int _currentHP;
    [SerializeField] private int _evolutionBreakPoint = 5;
    private bool _isAlive;
    private bool _canChangeFire;
    private Animator _animator;
    [SerializeField] private bool _isFireSlime = false;

    public SlimeType Type => _type;
    public bool CanChangeFire => _canChangeFire;


    private void Awake()
    {
        _isAlive = true;
        _currentHP = _maxHP;
        _type = SlimeType.None;
        _animator = GetComponent<Animator>();
        _canChangeFire = false;

    }
  
    public void ChangeSlimeElement()
    {
        Debug.Log(_canChangeFire);
        if (CanChangeFire)
        {


            _isFireSlime = !_isFireSlime;
            _animator.SetBool("isFireSlime", _isFireSlime);
            _type = _isFireSlime ? SlimeType.Fire : SlimeType.None;
            Debug.Log($"Change Slime in PlayerEntity: {_isFireSlime}//{_type}");
        }
    }


    private void Update()
    {
        
        if (!_isAlive)
        {
            Respawn(_spawnPosition);
        }
        if(_inventory.FireElement>=_evolutionBreakPoint)
        {
            _canChangeFire=true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }

    public void TakeDamge(int damage)
    {
        _currentHP -= damage;
        Debug.Log(_currentHP);

        if (_currentHP <= 0)
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
        transform.position = spawnPosition;

    }
}
