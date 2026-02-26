/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : Trap.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Represents a damaging trap in the world.
* Applies damage to the player while they remain
* inside the trap's trigger collider.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Amount of damage applied to the player while inside the trap.")]
    [SerializeField] private int _damage = 1;

    /// <summary>
    /// Called every frame while another collider
    /// stays inside this trigger collider.
    /// If the collider belongs to a PlayerEntity,
    /// damage is applied continuously.
    /// </summary>
    /// <param name="collision">
    /// The Collider2D currently overlapping with the trap.
    /// </param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        var player=collision.gameObject.GetComponent<PlayerEntity>();
        player.TakeDamge(_damage);
    }
}
