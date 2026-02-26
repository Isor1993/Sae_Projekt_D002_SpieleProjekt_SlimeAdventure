/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : PlayerCollectSystem.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Handles item collection for the player.
* Detects trigger collisions with objects that implement
* the ICollectable interface and forwards the player inventory
* reference to allow the item to add itself.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class PlayerCollectSystem : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Reference to the player's inventory where collected items are stored.")]
    [SerializeField] Player_Inventory inventory;

    /// <summary>
    /// Called automatically by Unity when another Collider2D
    /// enters this object's trigger collider.
    /// Checks whether the collided object implements ICollectable
    /// and, if so, executes its Collect logic.
    /// </summary>
    /// <param name="collision">
    /// The Collider2D that entered the trigger area.
    /// Used to determine if the object can be collected.
    /// </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        var collectableItem = collision.gameObject.GetComponent<ICollectable>();
        
        if (collectableItem != null)
        {
            collectableItem.Collect(inventory);
        }
    }   
}
