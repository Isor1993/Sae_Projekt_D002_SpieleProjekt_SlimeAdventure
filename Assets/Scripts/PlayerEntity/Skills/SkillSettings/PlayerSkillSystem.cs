/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : PlayerSkillSystem.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Manages all player-related skills.
* Initializes starting skills, handles skill unlocking,
* and executes skill casting logic depending on
* the current slime type and player state.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/

using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSystem : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Reference to the PlayerEntity used for checking slime type and state.")]
    [SerializeField] private PlayerEntity _playerEntity;

    [Tooltip("List containing all available skill data assets.")]
    [SerializeField] List<Skilldata> _allSkills = new List<Skilldata>();

    [Tooltip("List of skills the player starts with.")]
    [SerializeField] List<Skilldata> _startingPlayerSkills = new List<Skilldata>();

    [Tooltip("Offset from the player position where projectiles are spawned.")]
    [SerializeField] private Vector3 _spawnOffset=new Vector3(1f,0.3f,0f);

    List<PlayerSkill> _playerSkills;
    private Vector3 _spawnPoint;

    /// <summary>
    /// Initializes the player's skill list.
    /// Converts all starting Skilldata entries
    /// into runtime PlayerSkill instances.
    /// </summary>
    private void Awake()
    {
        _playerSkills = new List<PlayerSkill>();

        foreach (var skillData in _startingPlayerSkills)
        {
            _playerSkills.Add(new PlayerSkill(skillData));
        }
    }

    /// <summary>
    /// Checks runtime conditions for unlocking additional skills.
    /// If the player is allowed to change to Fire type,
    /// a corresponding skill is added.
    /// </summary>
    void Update()
    {
        if(_playerEntity.CanChangeFire)
        {
            _playerSkills.Add(new PlayerSkill(_allSkills[1]));
        }
    }

    /// <summary>
    /// Casts the appropriate shooting skill depending on
    /// the player's current slime type.
    /// Calculates the projectile spawn position
    /// based on direction and offset.
    /// </summary>
    /// <param name="dir">
    /// Direction of the shot.
    /// Typically -1 for left and 1 for right.
    /// </param>
    public void CastNormalShoot(float dir)
    {   
        _spawnPoint = new Vector3(transform.position.x + (_spawnOffset.x * dir), transform.position.y + _spawnOffset.y, transform.position.z);        
        if (_playerEntity.Type == SlimeType.None)
        {            
            _playerSkills[0].Cast(_spawnPoint, dir);
        }
        else if (_playerEntity.Type == SlimeType.Fire&&_playerEntity.CanChangeFire)
        {          
            _playerSkills[1].Cast(_spawnPoint, dir);
        }
    }

    /// <summary>
    /// Adds a new skill to the player's skill list at runtime.
    /// Wraps the provided Skilldata into a PlayerSkill instance.
    /// </summary>
    /// <param name="skillData">
    /// The Skilldata asset describing the skill to add.
    /// </param>
    public void AddSkill(Skilldata skillData)
    {
        _playerSkills.Add(new PlayerSkill(skillData));
    }
}
