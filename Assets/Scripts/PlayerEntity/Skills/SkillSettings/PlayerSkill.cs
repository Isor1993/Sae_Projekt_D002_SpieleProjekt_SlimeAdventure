/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : 
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* *
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
    private Vector3 _dir;

    public SlimeType Type => _type;
    public string Name => _name;
    public int Damage => _damage;
    public GameObject BulletPrefab => _bulletPrefab;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
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
    /// 
    /// </summary>
    /// <param name="projectilePrefab"></param>
    /// <param name="spawnPoint"></param>
    public void Cast( Vector3 spawnPoint,Vector3 dir)
    {
        _dir=dir;
        GameObject projectile = GameObject.Instantiate(_bulletPrefab, spawnPoint, Quaternion.identity);
        Bullet bullet=projectile.GetComponentInChildren<Bullet>();
        if (bullet == null)
            return;
        bullet.Initialize(_damage,dir);
       
#if DEBUG
        Debug.Log($"Cast {_name} with Projectile {projectile.name} with [{_damage}]");
#endif
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void ResetSkill()
    {
        _type = _baseData.Type;
        _name = _baseData.Name;
        _damage = _baseData.BaseDamage;
        _bulletPrefab = _baseData.BulletPrefab;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    public void SetType(SlimeType type)
    {
        _type = type;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public void ChangeName(string name)
    {
        _name = name;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(int damage)
    {
        _damage += damage;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    public void ReduceDamage(int damage)
    {
        _damage -= damage;
        if (damage < 0)
        {
            _damage = 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bulletPrefab"></param>
    public void SetBulletPrefab(GameObject bulletPrefab)
    {
        _bulletPrefab = bulletPrefab;
    }
}
