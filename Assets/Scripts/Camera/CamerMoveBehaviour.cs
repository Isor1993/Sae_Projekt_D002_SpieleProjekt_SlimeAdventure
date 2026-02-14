using UnityEngine;

public class CamerMoveBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private float _startZ;
    [SerializeField] private float _offsetY=-1.6f;

    private void Awake()
    {
        _startZ = transform.position.z;
       
    }

    void Update()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y - _offsetY, _startZ);
    }
}
