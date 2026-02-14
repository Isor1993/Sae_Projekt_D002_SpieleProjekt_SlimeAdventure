
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] List<Skilldata> _allSkills = new List<Skilldata>();
    private SlimeType _type;
    private string _name;
    private int _baseDamage;
    private int _currentDamage;
    private GameObject _bulletPrefab;


    public SlimeType Type => _type;
    public string Name => _name;
    public int BaseDamage => _baseDamage;
    public int CurrentDamage => _currentDamage;
    public GameObject BulletPrefab => _bulletPrefab;

    private PlayerSkill _skills;

    private void Awake()
    {
       
    }
    private void Start()
    {
        

    }
    private void Update()
    {       
        
    }
    public void ChangeType(SlimeType type)
    {
        _type = type; 
    }

    public void AddDamage(int damage)
    {
        _currentDamage+=damage;
    }
    public void ReduceDamage(int damage)
    {
        _currentDamage -= damage;
        if(_currentDamage<0)
        {
            _currentDamage=0;
        }
    }

    public void ChangeBulletPrefab(GameObject bulletPrefab)
    {
        _bulletPrefab = bulletPrefab;
    }

}
