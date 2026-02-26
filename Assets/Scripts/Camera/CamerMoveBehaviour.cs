/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : CamerMoveBehaviour.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Controls basic camera follow behavior.
* The camera tracks the player's X and Y position
* with a configurable vertical offset,
* while keeping its original Z position.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class CamerMoveBehaviour : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Reference to the player transform that the camera follows.")]
    [SerializeField] private Transform _player;

    [Tooltip("Vertical offset applied to the camera relative to the player.")]
    [SerializeField] private float _offsetY=-1.6f;

    private float _startZ;

    /// <summary>
    /// Caches the initial Z position of the camera.
    /// Ensures depth remains constant during movement.
    /// </summary>
    private void Awake()
    {
        _startZ = transform.position.z;       
    }

    /// <summary>
    /// Updates the camera position every frame.
    /// Follows the player on the X-axis
    /// and applies a vertical offset on the Y-axis.
    /// </summary>
    void Update()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y - _offsetY, _startZ);
    }
}
