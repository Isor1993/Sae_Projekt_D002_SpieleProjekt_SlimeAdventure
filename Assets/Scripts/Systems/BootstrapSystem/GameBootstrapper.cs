/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : 
* Date    : 22.01.2026
* Author  : Eric Rosenberg
*
* Description :
* *
* History :
* 22.01.2026 ER Created
******************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBootstrapper : MonoBehaviour
{
    [Tooltip("Write the UI Scene name here.")]
    [SerializeField] private string uiScene = "UI_MainRoot";
    [Tooltip("Write the Word Lvl 1 Scene name here.")]
    [SerializeField] private string worldScene = "World_Level_01";
        
    void Start()
    {
        LoadifNotLoaded(uiScene);
        LoadifNotLoaded(worldScene);        
    }

    /// <summary>
    /// Checks if the scene already exist if not it will be loaded.
    /// </summary>
    /// <param name="SceneName"></param>
    private void LoadifNotLoaded(string SceneName)
    {
        if(!SceneManager.GetSceneByName(SceneName).isLoaded)
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }
    }
}


