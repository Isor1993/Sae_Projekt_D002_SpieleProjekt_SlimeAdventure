using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    [SerializeField] private int _coins;
    [SerializeField] private int _evoltuionElementFire;

    public int Coins=>_coins;
    public int FireElement => _evoltuionElementFire;


    private void Awake()
    {
        _coins = 0;
        _evoltuionElementFire = 0;
    }
    public void AddCoin(int coin)
    {
        _coins += coin;
    }

    public void RemoveCoin(int coin)
    {
        if (_coins != 0)
        {
            _coins -= coin;
        }
    }

    public void AddEvelutuonElementFire(int element)
    {
        _evoltuionElementFire += element;
    }
    public void RemoveEvelutuonElementFire(int element)
    {
        if (_evoltuionElementFire != 0)
        {
            _evoltuionElementFire -= element;
        }
    }

}
