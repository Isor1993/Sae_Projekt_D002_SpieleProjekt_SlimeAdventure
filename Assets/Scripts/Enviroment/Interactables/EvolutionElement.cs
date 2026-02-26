/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : EvolutionElement.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Represents a collectible evolution element.
* Implements ICollectable and increases the player's
* fire evolution element count when collected.
* The object is disabled after collection.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class EvolutionElement : MonoBehaviour, ICollectable
{
    /// <summary>
    /// Executes the evolution element collection logic.
    /// Adds one fire evolution element to the player's inventory
    /// and disables the GameObject.
    /// </summary>
    /// <param name="inverntory">
    /// Reference to the player's inventory
    /// where the evolution element will be added.
    /// </param>
    public void Collect(Player_Inventory inverntory)
    {
        inverntory.AddEvelutuonElementFire(1);
        this.gameObject.SetActive(false);
    }
}
