/*****************************************************************************
* Project : 2D Jump'n'Run Steuerung (K2, S2)
* File    : GroundCheck.cs
* Date    : 25.12.2025
* Author  : Eric Rosenberg
*
* Description :
* Component responsible for detecting whether the player is grounded using
* an OverlapBox check. Provides optional Gizmos visualization for debugging.
*
* History :
* 25.12.2025 ER Created
******************************************************************************/

using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("Settings OverLapBox")]
    [Tooltip("LayerMask defining which layers are considered ground.")]
    [SerializeField] private LayerMask _layerMask;

    [Tooltip("Size of the overlap box used for ground detection.")]
    [SerializeField] private Vector2 _boxSize = new Vector2(1.14f, 0.04f);

    [Tooltip("Local offset from the GameObject position where the ground check is performed.")]
    [SerializeField] private Vector2 _boxOffset = new Vector2(0f, 0.98f);

    [Header("Debug")]
    [Tooltip("Enables Gizmos visualization for the ground check.")]
    [SerializeField] private bool DebugModus = false;

    private bool _isGrounded;

    /// <summary>
    /// Updates the grounded state at fixed time intervals.
    /// </summary>
    private void FixedUpdate()
    {
        _isGrounded = CheckOverlap();
    }

    /// <summary>
    /// Indicates whether the player is currently grounded.
    /// </summary>
    /// <returns>
    /// True if ground is detected below the object; otherwise, false.
    /// </returns>
    public bool CheckGround()
    {
        return _isGrounded;
    }

    /// <summary>
    /// Performs an OverlapBox check to detect ground contact.
    /// </summary>
    /// <returns>
    /// True if a collider on the ground layer is detected; otherwise, false.
    /// </returns>
    private bool CheckOverlap()
    {
        Vector2 boxPosition = (Vector2)transform.position + _boxOffset;
        Collider2D hit = Physics2D.OverlapBox(boxPosition, _boxSize, 0, _layerMask);

        return hit != null;
    }

    /// <summary>
    /// Draws Gizmos in the editor to visualize the ground check overlap box.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (DebugModus)
        {
            Vector3 position = transform.position;
            position.y += _boxOffset.y;
            position.x += _boxOffset.x;

            Gizmos.color = Application.isPlaying && _isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireCube(position, _boxSize);
        }
    }
}