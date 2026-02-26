/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : Player_Inventory.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Manages the player's inventory values.
* Stores and modifies resources such as coins
* and fire evolution elements during gameplay.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{    
     private int _coins;
     private int _evoltuionElementFire;

    /// <summary>
    /// Gets the current amount of coins the player owns.
    /// </summary>
    public int Coins=>_coins;

    /// <summary>
    /// Gets the current amount of fire evolution elements
    /// stored in the inventory.
    /// </summary>
    public int FireElement => _evoltuionElementFire;

    /// <summary>
    /// Initializes inventory values at the start of the game.
    /// Ensures all resource counters begin at zero.
    /// </summary>
    private void Awake()
    {
        _coins = 0;
        _evoltuionElementFire = 0;
    }

    /// <summary>
    /// Adds a specified amount of coins to the inventory.
    /// </summary>
    /// <param name="coin">
    /// The amount of coins to add.
    /// </param>
    public void AddCoin(int coin)
    {
        _coins += coin;
    }

    /// <summary>
    /// Removes a specified amount of coins from the inventory.
    /// Removal only occurs if the current coin amount is not zero.
    /// </summary>
    /// <param name="coin">
    /// The amount of coins to remove.
    /// </param>
    public void RemoveCoin(int coin)
    {
        if (_coins != 0)
        {
            _coins -= coin;
        }
    }

    /// <summary>
    /// Adds a specified amount of fire evolution elements
    /// to the inventory.
    /// </summary>
    /// <param name="element">
    /// The amount of fire elements to add.
    /// </param>
    public void AddEvelutuonElementFire(int element)
    {
        _evoltuionElementFire += element;
    }
    /// <summary>
    /// Removes a specified amount of fire evolution elements
    /// from the inventory.
    /// Removal only occurs if the current element count is not zero.
    /// </summary>
    /// <param name="element">
    /// The amount of fire elements to remove.
    /// </param>
    public void RemoveEvelutuonElementFire(int element)
    {
        if (_evoltuionElementFire != 0)
        {
            _evoltuionElementFire -= element;
        }
    }
}
