/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : PlayerSkill.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Represents a runtime instance of a player skill.
* Wraps Skilldata into a mutable object that can be modified
* during gameplay (e.g., damage upgrades, type changes).
* Handles projectile instantiation and skill casting logic.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class PlayerSkill
{
    private Skilldata _baseData;
    private SlimeType _type;
    private string _name;
    private int _damage;
    private GameObject _bulletPrefab;
    private float _dir;

    /// <summary>
    /// Gets the slime type associated with this skill.
    /// Determines which slime form the skill belongs to.
    /// </summary>
    public SlimeType Type => _type;

    /// <summary>
    /// Gets the current name of the skill.
    /// Can be modified at runtime.
    /// </summary>
    public string Name => _name;

    /// <summary>
    /// Gets the current damage value of the skill.
    /// </summary>
    public int Damage => _damage;

    /// <summary>
    /// Gets the projectile prefab used when casting this skill.
    /// </summary>
    public GameObject BulletPrefab => _bulletPrefab;

    /// <summary>
    /// Creates a new runtime PlayerSkill instance
    /// based on the provided Skilldata.
    /// </summary>
    /// <param name="data">
    /// The base Skilldata asset used to initialize this skill.
    /// </param>
    public PlayerSkill(Skilldata data)
    {
        _baseData = data;
        if (data == null)
        {
            Debug.Log($"{nameof(PlayerSkill)} : Skilldata not avaible.");
            return;
        }
        ResetSkill();
    }

    /// <summary>
    /// Casts the skill by instantiating its projectile prefab
    /// at the given spawn position and initializing it
    /// with damage, direction, and slime type.
    /// </summary>
    /// <param name="spawnPoint">
    /// The world position where the projectile should be spawned.
    /// </param>
    /// <param name="dir">
    /// The horizontal direction of the projectile
    /// (typically -1 for left or 1 for right).
    /// </param>
    public void Cast( Vector3 spawnPoint,float dir)
    {
        _dir=dir;
        GameObject projectile = GameObject.Instantiate(_bulletPrefab, spawnPoint, Quaternion.identity);
        Bullet bullet=projectile.GetComponentInChildren<Bullet>();
        if (bullet == null)
            return;
        bullet.Initialize(_damage,dir,_type);
       
#if DEBUG
        Debug.Log($"Cast {_name} with Projectile {projectile.name} with [{_damage}]");
#endif
    }

    /// <summary>
    /// Resets the skill values to their original
    /// configuration defined in the base Skilldata.
    /// </summary>
    public void ResetSkill()
    {
        _type = _baseData.Type;
        _name = _baseData.Name;
        _damage = _baseData.BaseDamage;
        _bulletPrefab = _baseData.BulletPrefab;
    }

    /// <summary>
    /// Changes the slime type of this skill at runtime.
    /// </summary>
    /// <param name="type">
    /// The new slime type to assign.
    /// </param>
    public void SetType(SlimeType type)
    {
        _type = type;
    }

    /// <summary>
    /// Changes the name of the skill at runtime.
    /// </summary>
    /// <param name="name">
    /// The new name to assign to the skill.
    /// </param>
    public void ChangeName(string name)
    {
        _name = name;
    }

    /// <summary>
    /// Increases the skill's damage value.
    /// </summary>
    /// <param name="damage">
    /// The amount of damage to add.
    /// </param>
    public void AddDamage(int damage)
    {
        _damage += damage;
    }

    /// <summary>
    /// Reduces the skill's damage value.
    /// Ensures that the damage does not drop below zero.
    /// </summary>
    /// <param name="damage">
    /// The amount of damage to subtract.
    /// </param>
    public void ReduceDamage(int damage)
    {
        _damage -= damage;
        if (damage < 0)
        {
            _damage = 0;
        }
    }

    /// <summary>
    /// Replaces the projectile prefab used by this skill.
    /// </summary>
    /// <param name="bulletPrefab">
    /// The new projectile prefab to assign.
    /// </param>
    public void SetBulletPrefab(GameObject bulletPrefab)
    {
        _bulletPrefab = bulletPrefab;
    }    
}
