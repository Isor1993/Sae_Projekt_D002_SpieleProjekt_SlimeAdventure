using UnityEngine;

public class PlayerCollectSystem : MonoBehaviour
{
    [SerializeField] Player_Inventory inventory;
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        var collectableItem = collision.gameObject.GetComponent<ICollectable>();
        
        if (collectableItem != null)
        {
            collectableItem.Collect(inventory);
        }
    }   
}
