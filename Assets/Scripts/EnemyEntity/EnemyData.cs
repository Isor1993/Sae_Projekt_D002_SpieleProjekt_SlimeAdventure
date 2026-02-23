using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _damage;
    [SerializeField] private int _speed;
    [SerializeField] private float _huntingSpeed;
    [SerializeField] private float _detectionRange;
    [SerializeField] private LayerMask _targetAttackable;
   

    public string Name => _name;
    public int MaxHP => _maxHP;
    public int Damage => _damage;
    public int Speed => _speed;

    public float HuntingSpeed => _huntingSpeed;
    public float DetectionRange => _detectionRange;
    public LayerMask TargetAttackable => _targetAttackable;

 
}
