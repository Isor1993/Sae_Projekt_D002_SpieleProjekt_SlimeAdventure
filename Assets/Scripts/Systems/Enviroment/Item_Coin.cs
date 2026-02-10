using UnityEngine;

public class Item_Coin : MonoBehaviour, ICollectable
{

    
    public void Collect(Player_Inventory inventory)
    {
        inventory.AddCoin(1);
        this.gameObject.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
