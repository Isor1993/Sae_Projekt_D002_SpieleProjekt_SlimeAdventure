/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : Skilldata.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Defines a ScriptableObject that stores all configuration data
* required for a skill, including slime type requirement,
* display name, base damage, and projectile prefab.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
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
    [SerializeField] private int _baseDamage;
       
    [Tooltip("GameObject of bullet which the skill is shooting.")]
    [SerializeField] private GameObject _bulletPrefab;

    /// <summary>
    /// Gets the slime type required to use this skill.
    /// Determines which slime form is allowed to activate the skill.
    /// </summary>
    public SlimeType Type => _type;

    /// <summary>
    /// Gets the display name of the skill.
    /// Used for UI representation, debugging, or identification.
    /// </summary>
    public string Name => _name;

    /// <summary>
    /// Gets the base damage value of the skill.
    /// This value is typically applied when the projectile hits a valid target.
    /// </summary>
    public int BaseDamage => _baseDamage;

    /// <summary>
    /// Gets the projectile prefab associated with this skill.
    /// This prefab is instantiated when the skill is executed.
    /// </summary>
    public GameObject BulletPrefab => _bulletPrefab;
}
