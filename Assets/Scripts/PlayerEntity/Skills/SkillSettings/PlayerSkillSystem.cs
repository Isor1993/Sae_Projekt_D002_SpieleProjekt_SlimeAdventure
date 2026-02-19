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
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillSystem : MonoBehaviour
{
    [SerializeField] List<Skilldata> _allSkills = new List<Skilldata>();
    [SerializeField] List<Skilldata> _startingPlayerSkills = new List<Skilldata>();
    List<PlayerSkill> _playerSkills;
    private Vector3 _shootPoint;

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
        
    }

    public void CastNormalShoot(Vector3 dir)
    {
        var bullet = _playerSkills[0].BulletPrefab;
        _shootPoint = transform.position + dir;
        Debug.Log($"poition[{transform.position}] + dir[{dir}]={_shootPoint}");
        _playerSkills[0].Cast( _shootPoint,dir);
    }

    public void AddSkill(Skilldata skillData)
    {
        _playerSkills.Add(new PlayerSkill(skillData));
    }

}
