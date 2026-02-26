/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : GameBootstrapper.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Central bootstrap and scene management controller.
* Ensures persistent lifetime across scene changes,
* loads required base scenes additively,
* and handles transitions between menu and gameplay.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBootstrapper : MonoBehaviour
{
    [Tooltip("Write the UI Scene name here.")]
    [SerializeField] private string uiScene = "UI_MainRoot";

    [Tooltip("Write the Word Lvl 1 Scene name here.")]
    [SerializeField] private string worldScene = "World_Level_01";

    public static GameBootstrapper Instance;

    /// <summary>
    /// Implements a basic Singleton pattern.
    /// Ensures only one GameBootstrapper instance exists
    /// and persists across scene transitions.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Called on startup.
    /// Ensures the UI scene is loaded additively
    /// if it is not already present.
    /// </summary>
    void Start()
    {
        LoadifNotLoaded(uiScene);             
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

    /// <summary>
    /// Starts a new game session.
    /// Loads the world scene and unloads the UI scene.
    /// </summary>
    public void NewGame()
    {
        LoadifNotLoaded(worldScene);
        SceneManager.UnloadSceneAsync(uiScene);
    }

    /// <summary>
    /// Returns to the main menu.
    /// Loads the UI scene and unloads the world scene.
    /// </summary>
    public void GoMenu()
    {
        LoadifNotLoaded(uiScene);
        SceneManager.UnloadSceneAsync(worldScene);
    }
}


