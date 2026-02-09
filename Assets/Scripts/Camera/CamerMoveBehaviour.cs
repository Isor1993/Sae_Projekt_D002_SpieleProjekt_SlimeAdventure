using UnityEngine;

public class CamerMoveBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private float _startZ;
    [SerializeField] private float _offsetY;

    private void Awake()
    {
        _startZ = transform.position.z;
        _offsetY = -3.59f;
    }

    void Update()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y - _offsetY, _startZ);
    }
}
