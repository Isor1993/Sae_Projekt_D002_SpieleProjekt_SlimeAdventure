using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;


public  class PlayerSkills : MonoBehaviour
{
    [SerializeField] List<ScriptableObject> _allsSkills=new List<ScriptableObject>();
    private List<PlayerSkills> _playerSkills;
    [SerializeField]private List<PlayerSkill> _skilData;
    



   



    private void Awake()
    {
       
    }
    private void Start()
    {
       
    }
    private void Update()
    {
        foreach(var skill in _allsSkills )
        {
            
        }
    }

    public void CastSpell()
    {
        
    }


}
