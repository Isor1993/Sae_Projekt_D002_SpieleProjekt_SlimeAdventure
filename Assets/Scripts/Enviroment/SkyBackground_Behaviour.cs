/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : 
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* *
* History :
* 20.02.2026 ER Created
******************************************************************************/
using UnityEngine;

public class SkyBackground_Behaviour : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;

    private float _startY;
    private float _startZ;  

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        _startY=transform.position.y;
        _startZ=transform.position.x;
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        transform.position = new Vector3(_mainCamera.position.x, _startY, _startZ);
    }
}
