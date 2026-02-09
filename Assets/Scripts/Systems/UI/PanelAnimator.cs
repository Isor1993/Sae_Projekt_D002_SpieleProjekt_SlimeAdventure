/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : PanelAnimator.cs
* Date    : 22.01.2026
* Author  : Eric Rosenberg
*
* Description :
* Handles fade-in animation for UI panels using a CanvasGroup.
* Alpha values can be configured in a 0–255 range for better
* readability and designer-friendly tweaking.
*
* History :
* 22.01.2026 ER Created
******************************************************************************/
using System.Collections;
using UnityEngine;

/// <summary>
/// Handles fade-in animation for UI elements by interpolating
/// the alpha value of a CanvasGroup over time.
/// </summary>
public class PanelAnimator : MonoBehaviour
{
    [Header("Settings")]

    [Header("Fade Canvas IN")]
    [Tooltip("Duration of the fade animation in seconds.")]
    [SerializeField] private float _fadeDuration = 0.5f;

    [Tooltip("Start alpha value (0–255).")]
    [Range(0, 255)]
    [SerializeField] private int _startAlpha255 = 0;

    [Tooltip("End alpha value (0–255).")]
    [Range(0, 255)]
    [SerializeField] private int _endAlpha255 = 255;

    [Header("Slide Panel")]
    [Tooltip("Use a predefined start position instead of the current position.")]
    [SerializeField] private bool _isStartPositionAktive = false;

    [Tooltip("Start position used when start position override is enabled.")]
    [SerializeField] private Vector2 _startPosition;

    [Tooltip("Target Y position of the panel.")]
    [SerializeField] private float _targetPositionY;

    [Tooltip("Duration of the slide animation in seconds.")]
    public float _animationDuration;

    //--- Fields ---
    private CanvasGroup _canvasGroup;
    private RectTransform _transform;
    private Vector2 _currentStartPosition;

    private void Awake()
    {
        if (!TryGetComponent(out _canvasGroup))
        {
            Debug.LogError("PanelAnimator requires a CanvasGroup.", this);
            enabled = false;
            return;
        }
        if (!TryGetComponent(out _transform))
        {
            Debug.LogError("PanelAnimator requires a RectTransform.", this);
            enabled = false;
            return;
        }
    }

    /// <summary>
    /// Starts a fade-in animation from the configured
    /// start alpha value to the end alpha value.
    /// </summary>
    public void FadeIn()
    {
        _canvasGroup.alpha = Alpha255To01(_startAlpha255);
        StartCoroutine(FadeCanvasGroup(Alpha255To01(_startAlpha255), Alpha255To01(_endAlpha255), _fadeDuration));
    }

    public void SlideIn()
    {        
        StartCoroutine(SlidePanel(_targetPositionY, _animationDuration));
    }

    /// <summary>
    /// Resets the panel to its original start position.
    /// The position depends on whether a custom start position is enabled.
    /// </summary>
    public void ResetPosition()
    {
        if (!_isStartPositionAktive)
        {
            _transform.anchoredPosition = _currentStartPosition;

        }
        else
        {
            _transform.anchoredPosition = _currentStartPosition;
        }
    }

    /// <summary>
    /// Coroutine that slides the panel vertically over time.
    /// </summary>
    /// <param name="targetPositionY">
    /// Target Y position for the slide animation.
    /// </param>
    /// <param name="animationDuration">
    /// Duration of the slide animation in seconds.
    /// </param>
    /// <returns>
    /// IEnumerator required for coroutine execution.
    /// </returns>
    private IEnumerator SlidePanel(float targetPositionY, float animationDuration)
    {        
        if (animationDuration <= 0)
            yield break;

        if (!_isStartPositionAktive)
        {
            _currentStartPosition = _transform.anchoredPosition;

        }
        else
        {
            _currentStartPosition = _startPosition;
        }

        float currentTime = 0f;

        while (currentTime < animationDuration)
        {
            currentTime += Time.deltaTime;

            float t = currentTime / animationDuration;

            float newPosition = Mathf.Lerp(_currentStartPosition.y, targetPositionY, t);

            _transform.anchoredPosition = new Vector2(_transform.anchoredPosition.x, newPosition);

            yield return null;
        }
        _transform.anchoredPosition = new Vector2(_transform.anchoredPosition.x, targetPositionY);
    }

    /// <summary>
    /// Coroutine that smoothly interpolates the CanvasGroup alpha
    /// from a start value to an end value over a specified duration.
    /// </summary>
    /// <param name="startAlpha">
    /// Starting alpha value in normalized range (0–1).
    /// </param>
    /// <param name="endAlpha">
    /// Target alpha value in normalized range (0–1).
    /// </param>
    /// <param name="fadeDuration">
    /// Duration of the fade animation in seconds.
    /// </param>
    /// <returns>
    /// IEnumerator required for coroutine execution.
    /// </returns>
    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha, float fadeDuration)
    {
        if (fadeDuration <= 0)
        {
            Debug.LogError("FadeDuration is 0 or below", this);
            _canvasGroup.alpha = endAlpha;
            yield break;

        }
        _canvasGroup.alpha = startAlpha;
        float currentTime = 0f;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, currentTime / fadeDuration);
            yield return null;
        }
        _canvasGroup.alpha = endAlpha;
    }

    /// <summary>
    /// Converts an alpha value from a 0–255 range
    /// to Unity's normalized 0–1 range.
    /// </summary>
    /// <param name="alpha255">
    /// Alpha value in the range 0–255.
    /// </param>
    /// <returns>
    /// Normalized alpha value in the range 0–1.
    /// </returns>
    private float Alpha255To01(int alpha255)
    {
        return Mathf.Clamp01(alpha255 / 255f);
    }
}
