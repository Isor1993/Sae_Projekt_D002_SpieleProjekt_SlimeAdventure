/*****************************************************************************
* Project : 2D Jump'n'Run Steuerung (K2, S2)
* File    : WallCheck.cs
* Date    : 25.12.2025
* Author  : Eric Rosenberg
*
* Description :
* Component used to detect whether the player is touching a wall by performing
* multiple configurable OverlapBox checks. Includes optional Gizmos visualization
* for debugging purposes.
*
* History :
* 25.12.2025 ER Created
******************************************************************************/

using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [Header("Options")]
    [Tooltip("Activates the drawing on Gizmos of the Wallckecks")]
    [SerializeField] private bool DebugModus = false;

    /// <summary>
    /// Serializable data container defining a single wall check configuration.
    /// </summary>
    [System.Serializable]
    public struct WallCheckData
    {
        [Header("Settings OverLapBox")]
        public Vector2 _boxSize;

        [Tooltip("Offset from GameObject to place the Wallcheck")]
        public Vector2 _boxOffset;

        [Tooltip("Target LayerMask which will be ckecked")]
        public LayerMask layerMask;
    }

    //--- Fields ---
    [SerializeField] private WallCheckData[] _wallChecks;

    private bool _isTouchingWall;

    /// <summary>
    /// Executes all configured wall checks at fixed time intervals and updates
    /// the current wall contact state.
    /// </summary>
    private void FixedUpdate()
    {
        _isTouchingWall = false;
        for (int i = 0; i < _wallChecks.Length; i++)
        {
            if (CheckOverlap(_wallChecks[i]))
            {
                _isTouchingWall = true;
                break;
            }
        }
    }

    /// <summary>
    /// Indicates whether the player is currently touching a wall.
    /// </summary>
    /// <returns>
    /// True if at least one wall check detects a collision; otherwise, false.
    /// </returns>
    public bool CheckWall()
    {
        return _isTouchingWall;
    }

    /// <summary>
    /// Performs a single OverlapBox check using the provided wall check data.
    /// </summary>
    /// <param name="data">
    /// Configuration data containing size, offset and target LayerMask.
    /// </param>
    /// <returns>
    /// True if a collider is detected within the overlap box; otherwise, false.
    /// </returns>
    private bool CheckOverlap(WallCheckData data)
    {
        Vector2 boxPosition = (Vector2)transform.position + data._boxOffset;
        Collider2D hit = Physics2D.OverlapBox(boxPosition, data._boxSize, 0, data.layerMask);

        return hit != null;
    }

    /// <summary>
    /// Draws Gizmos in the editor to visualize all wall check overlap boxes.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!DebugModus || _wallChecks == null)
            return;

        Gizmos.color = Application.isPlaying && _isTouchingWall ? Color.green : Color.red;
        for (int i = 0; i < _wallChecks.Length; i++)
        {
            Vector3 position = transform.position;
            position.y += _wallChecks[i]._boxOffset.y;
            position.x += _wallChecks[i]._boxOffset.x;

            Gizmos.DrawWireCube(position, _wallChecks[i]._boxSize);
        }
    }
}