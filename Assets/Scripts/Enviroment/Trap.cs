using UnityEngine;

public class Trap : MonoBehaviour
{ 
    [SerializeField] private int _damage = 1;


    private void OnTriggerStay2D(Collider2D collision)
    {
        var player=collision.gameObject.GetComponent<PlayerEntity>();
        player.TakeDamge(_damage);
    }



}
