using UnityEngine;

public class EvolutionElement : MonoBehaviour, ICollectable
{
    public void Collect(Player_Inventory inverntory)
    {
        inverntory.AddEvelutuonElementFire(1);
        this.gameObject.SetActive(false);
    }
}
