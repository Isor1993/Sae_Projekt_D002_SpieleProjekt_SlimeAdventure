/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : PortalGate.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Represents a level transition portal.
* Allows the player to proceed to the next level
* only if the boss has been defeated and
* the player is currently in fire slime form.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class PortalGate : MonoBehaviour
{
    private bool _bossIsDead = false;

    /// <summary>
    /// Called when another collider enters this object's collider.
    /// Checks whether the colliding object is the player
    /// and verifies if the required conditions are fulfilled
    /// to trigger the level transition.
    /// </summary>
    /// <param name="collision">
    /// The Collision2D data of the object that collided with the portal.
    /// </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var isplayer = collision.gameObject.CompareTag("Player");
        if (isplayer)
        {
            var player = collision.gameObject.GetComponent<PlayerEntity>();
            var SlimeElement = player.Type;
            if (SlimeElement == SlimeType.Fire && _bossIsDead)
            {
                NextLvl();
            }
        }
    }

    /// <summary>
    /// Triggers the transition to the next level.
    /// Currently redirects back to the main menu
    /// using the GameBootstrapper.
    /// </summary>
    public void NextLvl()
    {       
        GameBootstrapper.Instance.GoMenu();
    }

    /// <summary>
    /// Marks the boss as defeated.
    /// Enables the portal to be activated by the player.
    /// </summary>
    public void BossDied()
    {
        _bossIsDead = true;
    }
}
