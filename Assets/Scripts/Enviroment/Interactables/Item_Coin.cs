/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : Item_Coin.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Represents a collectible coin item.
* Implements ICollectable and adds one coin
* to the player's inventory when collected.
* The object is disabled after collection.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class Item_Coin : MonoBehaviour, ICollectable
{
    /// <summary>
    /// Executes the coin collection logic.
    /// Adds one coin to the player's inventory
    /// and disables the coin GameObject.
    /// </summary>
    /// <param name="inventory">
    /// Reference to the player's inventory
    /// where the coin value will be added.
    /// </param>
    public void Collect(Player_Inventory inventory)
    {
        inventory.AddCoin(1);
        this.gameObject.SetActive(false);
    } 
}
