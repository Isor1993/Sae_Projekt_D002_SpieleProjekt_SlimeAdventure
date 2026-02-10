using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    [SerializeField] private int _coins;


    private void Awake()
    {
        _coins = 0;
    }
    public void AddCoin(int coin)
    {
        _coins += coin;
    }

    public void RemoveCoin(int coin)
    {
        _coins -= coin;
    }

}
