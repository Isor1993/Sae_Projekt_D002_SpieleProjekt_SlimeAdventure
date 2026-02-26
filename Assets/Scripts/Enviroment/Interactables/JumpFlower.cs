/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : JumpFlower.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Represents a jump-enhancing object in the world.
* Provides a configurable bounce force value
* that can be used by external systems
* (e.g., PlayerController) to apply upward momentum.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class JumpFlower : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Amount of upward force applied when the player interacts with this object.")]
    [SerializeField] private float bounceForce = 15f;

    /// <summary>
    /// Gets the bounce force value.
    /// Used by other components to apply vertical impulse.
    /// </summary>
    public float BounceForce => bounceForce;
       
}
