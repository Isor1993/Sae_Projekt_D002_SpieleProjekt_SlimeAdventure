/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : ICollectable.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Defines a contract for objects that can be collected by the player.
* Any GameObject implementing this interface must provide
* logic describing what happens when it is collected,
* typically adding resources to the player's inventory.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/

public interface ICollectable
{
    /// <summary>
    /// Executes the collection logic for this object.
    /// Called when the player interacts with or enters
    /// the trigger collider of the collectable item.
    /// </summary>
    /// <param name="inverntory">
    /// Reference to the player's inventory.
    /// Used to add resources, elements, coins, or other values
    /// when the item is collected.
    /// </param>
    public void Collect(Player_Inventory inverntory);
}
