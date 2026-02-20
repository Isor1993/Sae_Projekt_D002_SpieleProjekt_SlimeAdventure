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

using System.Collections.Generic;
using UnityEngine;


public class PlayerSkillSystem : MonoBehaviour
{
    [SerializeField] private PlayerEntity _playerEntity;
    [SerializeField] List<Skilldata> _allSkills = new List<Skilldata>();
    [SerializeField] List<Skilldata> _startingPlayerSkills = new List<Skilldata>();
    List<PlayerSkill> _playerSkills;
    private Vector3 _spawnPoint;
    [SerializeField] private Vector3 _spawnOffset=new Vector3(1f,0.3f,0f);

    private void Awake()
    {
        _playerSkills = new List<PlayerSkill>();
        foreach (var skillData in _startingPlayerSkills)
        {
            _playerSkills.Add(new PlayerSkill(skillData));
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_playerEntity.CanChangeFire)
        {
            _playerSkills.Add(new PlayerSkill(_allSkills[1]));
        }
    }

    public void CastNormalShoot(float dir)
    {
        //Debug.Log("shooooooooooot");
        Debug.Log($"Current Type PlayerController: {_playerEntity.Type}");
        _spawnPoint = new Vector3(transform.position.x + (_spawnOffset.x * dir), transform.position.y + _spawnOffset.y, transform.position.z);
        //Debug.Log($"poition[{transform.position.x}] + dir[{_spawnOffset.x}]={_spawnPoint.x}");
        if (_playerEntity.Type == SlimeType.None)
        {
            
            _playerSkills[0].Cast(_spawnPoint, dir);
        }
        else if (_playerEntity.Type == SlimeType.Fire&&_playerEntity.CanChangeFire)
        {
          
            _playerSkills[1].Cast(_spawnPoint, dir);
        }
    }

    public void AddSkill(Skilldata skillData)
    {
        _playerSkills.Add(new PlayerSkill(skillData));
    }

}
