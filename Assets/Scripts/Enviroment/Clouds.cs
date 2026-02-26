/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : Clouds.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Defines cloud behavior parameters used by the SkyController.
* Stores parallax strength and individual horizontal drift speed
* for background cloud movement.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Defines how strongly the cloud reacts to camera movement (parallax effect).")]
    [Range(0f, 1f)]
    [SerializeField] private float _parallaxFaktor;

    [Tooltip("Defines the constant horizontal drift speed of the cloud.")]
    [Range(-2f,2f)]
    [SerializeField] private float _cloudMovement;

    /// <summary>
    /// Gets the parallax factor of the cloud.
    /// Determines how much the cloud moves relative to camera movement.
    /// </summary>
    public float ParallaxFaktor => _parallaxFaktor;

    /// <summary>
    /// Gets the horizontal drift speed of the cloud.
    /// Controls constant movement independent of camera motion.
    /// </summary>
    public float CloudMovement => _cloudMovement;   
}
