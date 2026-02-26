/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : AnimationPara.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Defines a serializable data structure used for animation parameters.
* Stores a target position and a duration value,
* typically used for timed movement or UI transitions.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;
[System.Serializable]
public struct AnimationPara
{
    /// <summary>
    /// Target position used for the animation.
    /// Defines where the object should move.
    /// </summary>
    public Vector2 Position;

    /// <summary>
    /// Duration of the animation in seconds.
    /// Defines how long the movement should take.
    /// </summary>
    public float Duration;
}
