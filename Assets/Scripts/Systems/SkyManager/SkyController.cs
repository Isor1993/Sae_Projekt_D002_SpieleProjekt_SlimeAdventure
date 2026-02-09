using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _mainCameraT;

    [SerializeField] private float _spawnOffset = 5f;
    [SerializeField] private float _despawnOffset = 5f;
    
    private float _camLeftBoarder;
    private float _camRightBoarder;
    private float _despawnPos_1;
    private float _despawnPos_2;
    private float _spawnPos_1;
    private float _spawnPos_2;




    private Clouds[] _cloudArray;
    private float _camMovement;
    private Vector3 _currentCamPos;
    private Vector3 _lastCameraPos;

    private void Awake()
    {

        _cloudArray = GetComponentsInChildren<Clouds>();



    }
    private void Start()
    {
        _lastCameraPos = _mainCameraT.position;
    }

    private void Update()
    {
        float camX=_mainCameraT.position.x;
        var camHalfWidth = _mainCamera.orthographicSize * _mainCamera.aspect;
        
        _spawnPos_1 = camX + camHalfWidth + _spawnOffset;   // Spawn rechts
        _despawnPos_1 = camX - camHalfWidth - _despawnOffset; // Despawn links

        _spawnPos_2 = camX - camHalfWidth - _spawnOffset;  // Spawn links
        _despawnPos_2 = camX + camHalfWidth + _despawnOffset; // Despawn rechts

        Debug.LogWarning(_camLeftBoarder);
        Debug.LogWarning(_camRightBoarder);



        GetCamerMovement();
        foreach (var cloud in _cloudArray)
        {
            
            var cloudMovement = (_camMovement * cloud.ParallaxFaktor) + (cloud.CloudMovement * Time.deltaTime);
            cloud.transform.position += new Vector3(cloudMovement, 0, 0);

            if (cloud.transform.position.x < _despawnPos_1)
            {
                cloud.transform.position = new Vector3(_spawnPos_1, cloud.transform.position.y, cloud.transform.position.z);
            }
            else if (cloud.transform.position.x > _despawnPos_2)
            {
                cloud.transform.position = new Vector3(_spawnPos_2, cloud.transform.position.y, cloud.transform.position.z);

            }
        }

    }

    /// <summary>S
    /// 
    /// </summary>
    private void GetCamerMovement()
    {
        _currentCamPos = _mainCameraT.position;
        var camDiff = _currentCamPos.x - _lastCameraPos.x;
        _camMovement = camDiff;
        _lastCameraPos = _currentCamPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(_spawnPos_1, 5f), new Vector3(1f, 1f, 1f));
        Gizmos.DrawWireCube(new Vector3(_despawnPos_1, 5f), new Vector3(1f, 1f, 1f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(_spawnPos_2, 5f), new Vector3(1f, 1f, 1f));
        Gizmos.DrawWireCube(new Vector3(_despawnPos_2, 5f), new Vector3(1f, 1f, 1f));
    }
}
