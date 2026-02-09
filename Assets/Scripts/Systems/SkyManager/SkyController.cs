using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _mainCameraT;

    [SerializeField] private float _spawnOffset_1 = 5f;

    [SerializeField] private float _spawnOffset_2 = 5f;

    [SerializeField] private float _despawnOffset_1 = 7f;

    [SerializeField] private float _despawnOffset_2 = 7f;



    private float _despawnPos_1;
    private float _despawnPos_2;
    private float _spawnPos_1;
    private float _spawnPos_2;




    private Clouds[] _cloudArray;
    private Vector3 _lastCamPos;
    private Vector3 _camDelta;

    private void Awake()
    {

        _cloudArray = GetComponentsInChildren<Clouds>();



    }
    private void Start()
    {
        _lastCamPos = _mainCameraT.position;
    }

    private void Update()
    {

        var currentCamPos = _mainCameraT.position;
        _camDelta = currentCamPos - _lastCamPos;

        DefineCloudBounds(currentCamPos);

        foreach (var cloud in _cloudArray)
        {
            ApplyCloudMovement(cloud);
            HandleCloudRecycling(cloud);
        }
        _lastCamPos = currentCamPos;
    }
    
    private void ApplyCloudMovement(Clouds cloud)
    {
        var parallaxMovement = -_camDelta * cloud.ParallaxFaktor;
        var cloudDrift = Vector3.right * cloud.CloudMovement * Time.deltaTime;

        cloud.transform.position += new Vector3(parallaxMovement.x + cloudDrift.x, 0f, 0f);
    }

    private void DefineCloudBounds(Vector3 currentCamPos)
    {
        var camHalfWidth = _mainCamera.orthographicSize * _mainCamera.aspect;

        _spawnPos_1 = currentCamPos.x + camHalfWidth + _spawnOffset_1;
        _despawnPos_1 = currentCamPos.x - camHalfWidth - _despawnOffset_1;

        _spawnPos_2 = currentCamPos.x - camHalfWidth - _spawnOffset_2;
        _despawnPos_2 = currentCamPos.x + camHalfWidth + _despawnOffset_2;
    }

    private void HandleCloudRecycling(Clouds cloud)
    {
        if (cloud.transform.position.x < _despawnPos_1)
        {
            cloud.transform.position = new Vector3(_spawnPos_1, cloud.transform.position.y, cloud.transform.position.z);
        }
        else if (cloud.transform.position.x > _despawnPos_2)
        {
            cloud.transform.position = new Vector3(_spawnPos_2, cloud.transform.position.y, cloud.transform.position.z);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(_spawnPos_1, 5f), new Vector3(1f, 1f, 1f));
        Gizmos.DrawWireCube(new Vector3(_despawnPos_1, 5f), new Vector3(1f, 1f, 1f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(_spawnPos_2, 2f), new Vector3(1f, 1f, 1f));
        Gizmos.DrawWireCube(new Vector3(_despawnPos_2, 2f), new Vector3(1f, 1f, 1f));
    }
}
