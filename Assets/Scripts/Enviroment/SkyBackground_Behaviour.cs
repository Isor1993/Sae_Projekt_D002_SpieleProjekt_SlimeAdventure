/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : SkyBackground_Behaviour.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Keeps the sky background aligned with the camera on the X-axis
* while preserving its original Y and Z positions.
* Creates the effect of an infinite horizontal sky.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class SkyBackground_Behaviour : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Reference to the main camera transform used for horizontal tracking.")]
    [SerializeField] private Transform _mainCamera;

    private float _startY;
    private float _startZ;

    /// <summary>
    /// Caches the initial Y and Z position of the background.
    /// These values remain constant during runtime.
    /// </summary>
    private void Awake()
    {
        _startY=transform.position.y;
        _startZ=transform.position.z;
    }

    /// <summary>
    /// Updates the background position every frame.
    /// Follows the camera only on the X-axis
    /// while maintaining its original Y and Z coordinates.
    /// </summary>
    private void Update()
    {
        transform.position = new Vector3(_mainCamera.position.x, _startY, _startZ);
    }
}
