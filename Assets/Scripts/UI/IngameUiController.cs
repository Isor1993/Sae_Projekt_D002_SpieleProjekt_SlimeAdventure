/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : IngameUiController.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Controls the in-game UI elements.
* Updates player-related values such as HP, coins, and elements,
* and manages the pause (break) menu including input handling,
* time scaling, and scene transition back to the main menu.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class IngameUiController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Text element displaying the player's current HP.")]
    [SerializeField] private TextMeshProUGUI _hpText;

    [Tooltip("Text element displaying the current coin amount.")]
    [SerializeField] private TextMeshProUGUI _coinText;

    [Tooltip("Text element displaying the amount of fire elements.")]
    [SerializeField] private TextMeshProUGUI _elementText;

    [Tooltip("Reference to the PlayerEntity for accessing runtime data such as HP.")]
    [SerializeField] private PlayerEntity _player;

    [Tooltip("Reference to the Player_Inventory for accessing collected resources.")]
    [SerializeField] private Player_Inventory _inventory;

    [Tooltip("UI panel that represents the break (pause) menu.")]
    [SerializeField] private GameObject _BreakMenuPannel;
    [Tooltip("Continue buttom from paused Menu.")]
    [SerializeField] private GameObject _continueBottom;

    private PlayerInputActions _inputActions;
    private InputAction _paused;
    private bool _isPaused = false;

    /// <summary>
    /// Initializes the input action system and caches
    /// the pause input action for runtime use.
    /// </summary>
    private void Awake()
    {
        _inputActions=new PlayerInputActions();
        _paused = _inputActions.Slime.Paused;
    }

    /// <summary>
    /// Updates UI text elements every frame
    /// and checks for pause input.
    /// If the pause action is triggered,
    /// the pause state is toggled.
    /// </summary>
    void Update()
    {
        _hpText.text = "x" + _player.CurrentHP;
        _coinText.text = "x" + _inventory.Coins;
        _elementText.text = "x" + _inventory.FireElement;
    
        if(_paused.WasPressedThisFrame())
        {
            EventSystem.current.SetSelectedGameObject(_continueBottom);
            TogglePause();
        }
    }
        
    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    /// <summary>
    /// Called when the Continue button is pressed.
    /// Closes the break menu, resumes game time,
    /// and resets the pause state.
    /// </summary>
    public void PressContinueButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible=false;
        _BreakMenuPannel.SetActive(false);
      
        Time.timeScale = 1f;
        _isPaused = false;
    }

    /// <summary>
    /// Called when the Exit button is pressed.
    /// Resumes game time, resets pause state,
    /// and transitions back to the main menu.
    /// </summary>
    public void PressExitButton()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        GameBootstrapper.Instance.GoMenu();
        
    }

    /// <summary>
    /// Toggles the pause state of the game.
    /// Activates or deactivates the break menu panel
    /// and adjusts the global time scale accordingly.
    /// </summary>
    private void TogglePause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _isPaused = !_isPaused;

        _BreakMenuPannel.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0f : 1f;
    }
}
