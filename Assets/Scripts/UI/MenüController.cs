/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : MenüController.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Controls the main menu flow and panel transitions.
* Handles start screen input, menu navigation, panel switching,
* animations, and UI focus management.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenüController : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("EventSystem responsible for UI navigation.")]
    [SerializeField] private GameObject _eventSystem;

    [Tooltip("Main UI canvas.")]
    [SerializeField] private GameObject _canvas;

    [Tooltip("Panel displaying the game title.")]
    [SerializeField] private GameObject _gameTitlePanel;

    [Tooltip("Main menu panel.")]
    [SerializeField] private GameObject _mainMenuPanel;

    [Tooltip("Start screen panel.")]
    [SerializeField] private GameObject _startScreenPanel;

    [Tooltip("In-game start screen panel.")]
    [SerializeField] private GameObject _ingameStartScreenPanel;

    [Tooltip("Options panel.")]
    [SerializeField] private GameObject _optionsPanel;

    [Tooltip("Credits panel.")]
    [SerializeField] private GameObject _creditsPanel;

    [Tooltip("Background image used for menu selection highlighting.")]
    [SerializeField] private UnityEngine.UI.Image _selectionBackground;

    [Tooltip("First selectable button in the options menu.")]
    [SerializeField] private GameObject _optionsFirstButton;

    [Tooltip("First selectable button in the main menu.")]
    [SerializeField] private GameObject _mainMenuFirstButton;

    [Tooltip("First selectable button in the credits menu.")]
    [SerializeField] private GameObject _creditsFirstButton;

    [Tooltip("ScrollRect component of the credits panel.")]
    [SerializeField] private ScrollRect _scrollRect;

    [Tooltip("Multiplier applied to menu animation speed.")]
    [SerializeField] private float _animationSpeedMultiplicator;


    //--- Fields ---
    private PanelAnimator _mainMenüPA;
    private PanelAnimator _optionsPA;
    private PanelAnimator _creditsPA;
    private bool _hasSwitched = false;
    private float _scrollRectDefaultValue=1f;


    /// <summary>
    /// Initializes menu state and caches required references.
    /// </summary>
    private void Awake()
    {
        if (_mainMenuPanel != null)
        {
            _mainMenuPanel.SetActive(false);
        }
        StartSetUp();
        MappingAnimatorPanels();
    }

    /// <summary>
    /// Sets the initial active state of all menu panels.
    /// </summary>
    private void StartSetUp()
    {
        if (_eventSystem && _canvas && _startScreenPanel
            && _ingameStartScreenPanel && _gameTitlePanel && _mainMenuPanel
            && _optionsPanel && _creditsPanel != null)
        {            
            _eventSystem.SetActive(true);
            _canvas.SetActive(true);
            _startScreenPanel.SetActive(true);
            _ingameStartScreenPanel.SetActive(false);
            _gameTitlePanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _optionsPanel.SetActive(false);
            _creditsPanel.SetActive(false);
        }
        else { Debug.LogError("One or more required menu dependencies are missing.", this); }
    }

    /// <summary>
    /// Retrieves PanelAnimator components from all menu panels.
    /// </summary>
    private void MappingAnimatorPanels()
    {
        _mainMenüPA = _mainMenuPanel.GetComponent<PanelAnimator>();
        _optionsPA = _optionsPanel.GetComponent<PanelAnimator>();
        _creditsPA = _creditsPanel.GetComponent<PanelAnimator>();
    }

    /// <summary>
    /// Listens for any input on the start screen
    /// to transition into the main menu.
    /// </summary>
    private void Update()
    {
        if (_startScreenPanel != null && !_hasSwitched)
        {
            if (Input.anyKeyDown)
            {
                FadeInMainMenü();
            }
        }
    }

    /// <summary>
    /// Transitions from the start screen to the main menu.
    /// </summary>
    private void FadeInMainMenü()
    {
       
        _hasSwitched = true;

        SwitchtoNextPanel(_startScreenPanel, _mainMenuPanel, _mainMenuFirstButton);

        if (_mainMenuPanel != null)
        {
            SlideInMenüPanel();
        }
        enabled = false;
    }

    /// <summary>
    /// Plays the slide-in animation for the main menu panel.
    /// </summary>
    private void SlideInMenüPanel()
    {
        _mainMenüPA._animationDuration *= _animationSpeedMultiplicator;
        _mainMenüPA.SlideIn();
        _mainMenüPA._animationDuration /= _animationSpeedMultiplicator;
    }

    /// <summary>
    /// Triggered by the Options button.
    /// Switches from the main menu to the options menu.
    /// </summary>
    public void ClickOptions()
    {
        SwitchtoNextPanel(_mainMenuPanel, _optionsPanel, _optionsFirstButton);
        _optionsPA.SlideIn();
        _mainMenüPA.ResetPosition();
    }

    /// <summary>
    /// Triggered by the Credits button.
    /// Switches from the main menu to the credits menu.
    /// </summary>
    public void ClickCredits()
    {
        SwitchtoNextPanel(_mainMenuPanel, _creditsPanel, _creditsFirstButton);
        _creditsPA.SlideIn();
        _mainMenüPA.ResetPosition();
    }

    /// <summary>
    /// Triggered by the Exit button.
    /// Closes the application.
    /// </summary>
    public void ClickExit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Triggered by Back buttons.
    /// Returns to the main menu from the given panel.
    /// </summary>
    /// <param name="currentPanel">
    /// The panel that should be closed.
    /// </param>
    public void ClickBack(GameObject currentPanel)
    {
        _scrollRect.verticalNormalizedPosition = _scrollRectDefaultValue;        
        SwitchtoNextPanel(currentPanel, _mainMenuPanel, _mainMenuFirstButton);       
        _mainMenüPA.SlideIn();
        currentPanel.GetComponent<PanelAnimator>().ResetPosition();
    }

    public void ClickNewGame()
    {      
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameBootstrapper.Instance.NewGame();
    }

    /// <summary>
    /// Switches visibility between two panels and updates UI selection focus.
    /// </summary>
    /// <param name="currentPanel">
    /// Panel to deactivate.
    /// </param>
    /// <param name="nextPanel">
    /// Panel to activate.
    /// </param>
    /// <param name="firstSelectedButton">
    /// UI element that should receive selection focus.
    /// </param>
    private void SwitchtoNextPanel(GameObject currenPanel, GameObject nextPanel, GameObject firstSelectedButton)
    {
        if (_ingameStartScreenPanel != null)
        {
            _ingameStartScreenPanel.SetActive(true);

        }
        currenPanel.SetActive(false);
        nextPanel.SetActive(true);
        _gameTitlePanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }
}

