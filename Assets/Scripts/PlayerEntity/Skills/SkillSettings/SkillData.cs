
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class Skilldata : ScriptableObject
{
    [Header("Settings")]
    [Tooltip("Type of the slime which is needed for the skill.")]
    [SerializeField] private SlimeType _type;

    [Tooltip("Name of the Skill.")]
    [SerializeField] private string _name;

    [Tooltip("Damage value of skill.")]
    [SerializeField] private int _damage;
       
    [Tooltip("GameObject of bullet which the skill is shooting.")]
    [SerializeField] private GameObject _bulletPrefab;

    /// <summary>
    /// 
    /// </summary>
    public SlimeType Type => _type;

    /// <summary>
    /// 
    /// </summary>
    public string Name => _name;

    /// <summary>
    /// 
    /// </summary>
    public int Damage => _damage;
       
    /// <summary>
    /// 
    /// </summary>
    public GameObject BulletPrefab => _bulletPrefab;
}
