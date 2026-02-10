using UnityEngine;

public class Item_Coin : MonoBehaviour, ICollectable
{
    
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private Vector2 _stepPos_1;   
    [SerializeField] private Vector2 _endPosition;
    [SerializeField] private float _duration_1;
    [SerializeField] private float _duration_2;
    [SerializeField] private float _duration_3;
    [SerializeField] private float _duration_4;
    private float _t;


    public void Collect(Player_Inventory inventory)
    {
        inventory.AddCoin(1);
        this.gameObject.SetActive(false);
    }

    private void Awake()
    {
        _startPosition = transform.position;
       
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _t = Time.time*_duration_1;
        
        transform.position = MoveCoin(_startPosition, _endPosition, _t);
       


    }

    private Vector2 MoveCoin(Vector2 start,Vector2 end,float t)
    {
        var nextPosition=Vector2.Lerp(start, end, t);      
        return nextPosition;
        
    }
}
