using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _damage;

    public string Name => _name;
    public int MaxHP => _maxHP;
    public int Damage => _damage;
}
