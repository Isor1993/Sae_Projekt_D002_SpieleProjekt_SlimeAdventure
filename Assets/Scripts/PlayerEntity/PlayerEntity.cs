using UnityEngine;

public enum SlimeType
{
    None,
    Fire,
}
public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private Player_Inventory _inventory;
    private SlimeType _type;

    private void Awake()
    {
        _type = SlimeType.None;
    }

  

}
