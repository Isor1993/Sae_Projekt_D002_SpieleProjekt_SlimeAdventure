/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : SkyController.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Controls dynamic cloud behavior in the sky background.
* Applies parallax movement based on camera movement,
* adds individual cloud drift, and recycles clouds
* when they move outside defined camera bounds.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class SkyController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Reference to the main camera used to calculate screen bounds.")]
    [SerializeField] private Camera _mainCamera;

    [Tooltip("Transform of the main camera used to track position changes.")]
    [SerializeField] private Transform _mainCameraT;

    [Tooltip("Horizontal offset used when spawning clouds on the right side.")]
    [SerializeField] private float _spawnOffset_1 = 5f;

    [Tooltip("Horizontal offset used when spawning clouds on the left side.")]
    [SerializeField] private float _spawnOffset_2 = 5f;

    [Tooltip("Horizontal offset used when despawning clouds on the left side.")]
    [SerializeField] private float _despawnOffset_1 = 7f;

    [Tooltip("Horizontal offset used when despawning clouds on the right side.")]
    [SerializeField] private float _despawnOffset_2 = 7f;

    private float _despawnPos_1;
    private float _despawnPos_2;
    private float _spawnPos_1;
    private float _spawnPos_2;

    private Clouds[] _cloudArray;
    private Vector3 _lastCamPos;
    private Vector3 _camDelta;

    /// <summary>
    /// Caches all Clouds components that are children of this GameObject.
    /// This allows centralized control over cloud movement and recycling.
    /// </summary>
    private void Awake()
    {
        _cloudArray = GetComponentsInChildren<Clouds>();
    }

    /// <summary>
    /// Stores the initial camera position.
    /// Used to calculate movement delta in Update.
    /// </summary>
    private void Start()
    {
        _lastCamPos = _mainCameraT.position;
    }

    /// <summary>
    /// Updates cloud movement every frame.
    /// Calculates camera delta movement,
    /// updates cloud spawn/despawn bounds,
    /// applies parallax movement,
    /// and handles cloud recycling.
    /// </summary>
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

    /// <summary>
    /// Applies parallax and drift movement to a single cloud.
    /// Parallax is based on camera movement,
    /// while drift adds constant horizontal motion.
    /// </summary>
    /// <param name="cloud">
    /// The cloud instance that should receive movement updates.
    /// </param>
    private void ApplyCloudMovement(Clouds cloud)
    {
        var parallaxMovement = -_camDelta * cloud.ParallaxFaktor;
        var cloudDrift = Vector3.right * cloud.CloudMovement * Time.deltaTime;

        cloud.transform.position += new Vector3(parallaxMovement.x + cloudDrift.x, 0f, 0f);
    }

    /// <summary>
    /// Calculates dynamic spawn and despawn positions
    /// based on the current camera position and size.
    /// Ensures clouds are recycled outside the visible screen area.
    /// </summary>
    /// <param name="currentCamPos">
    /// Current world position of the camera.
    /// </param>
    private void DefineCloudBounds(Vector3 currentCamPos)
    {
        var camHalfWidth = _mainCamera.orthographicSize * _mainCamera.aspect;

        _spawnPos_1 = currentCamPos.x + camHalfWidth + _spawnOffset_1;
        _despawnPos_1 = currentCamPos.x - camHalfWidth - _despawnOffset_1;

        _spawnPos_2 = currentCamPos.x - camHalfWidth - _spawnOffset_2;
        _despawnPos_2 = currentCamPos.x + camHalfWidth + _despawnOffset_2;
    }

    /// <summary>
    /// Recycles clouds when they move beyond defined bounds.
    /// Teleports them to the opposite side to create
    /// a seamless infinite sky effect.
    /// </summary>
    /// <param name="cloud">
    /// The cloud instance that should be checked and repositioned if necessary.
    /// </param>
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
#if UNITY_EDITOR
    /// <summary>
    /// Draws gizmo markers in the Scene view
    /// to visualize cloud spawn and despawn boundaries.
    /// Red = first spawn/despawn pair.
    /// Yellow = second spawn/despawn pair.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(_spawnPos_1, 5f), new Vector3(1f, 1f, 1f));
        Gizmos.DrawWireCube(new Vector3(_despawnPos_1, 5f), new Vector3(1f, 1f, 1f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(_spawnPos_2, 2f), new Vector3(1f, 1f, 1f));
        Gizmos.DrawWireCube(new Vector3(_despawnPos_2, 2f), new Vector3(1f, 1f, 1f));
    }
#endif
}
