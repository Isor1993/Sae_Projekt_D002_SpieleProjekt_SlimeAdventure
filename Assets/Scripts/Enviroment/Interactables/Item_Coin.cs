using UnityEngine;

public class Item_Coin : MonoBehaviour, ICollectable
{
    public void Collect(Player_Inventory inventory)
    {
        inventory.AddCoin(1);
        this.gameObject.SetActive(false);
    } 
}
