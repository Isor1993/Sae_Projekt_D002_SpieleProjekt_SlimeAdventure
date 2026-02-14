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
using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Animation_Floating_Items : MonoBehaviour
{
    [Header("References")]
    [Tooltip("List of the animation steps")]
    [SerializeField] private List<AnimationPara> _pos;
    [Tooltip("Enable/Disable loop for the animation")]
    [SerializeField] private bool _isLoop = false;
    [Header("Debug Parameter")]
    [Tooltip("Change the size of stePositions on gizmo")]
    [Range(0f, 0.5f)]
    [SerializeField] float _gizmosSphereSize = 1f;

    private AnimationPara _AnimationPos;
    private Vector2 _targetPos;
    private Vector2 _currenPosition;
    private float _duration;
    private int _currentAnimationPosIndex;
    private float _t;
    private float _elapsedTime;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        if (_pos == null || _pos.Count < 1)
        {
            Debug.LogError("Missing List: create AnimationPos in the list.", this);
            enabled = false;
            return;
        }
        _currentAnimationPosIndex = 0;
        PrepareStep();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        if (_duration <= 0f)
        {
            Debug.LogError("Your Duration need to be bigger than [0].", this);
            return;
        }

        _elapsedTime += Time.deltaTime;
        _t = _elapsedTime / _duration;

        Vector2 newPos = Vector2.Lerp(_currenPosition, _targetPos, _t);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

        Debug.Log(_currentAnimationPosIndex);
        if (_t >= 1)
        {
            AdvanceStep();
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var index = 0;
        foreach (var pos in _pos)
        {

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pos.Position, _gizmosSphereSize);
            Handles.Label(pos.Position + new Vector2(0.1f, 0.1f), $"{index}");
            index++;

        }
    }
#endif//UNITY_EDITOR

    /// <summary>
    /// 
    /// </summary>
    private void PrepareStep()
    {
        _elapsedTime = 0f;
        _currenPosition = transform.position;
        _AnimationPos = _pos[_currentAnimationPosIndex];
        _duration = _AnimationPos.Duration;
        _targetPos = _AnimationPos.Position;
    }

    /// <summary>
    /// 
    /// </summary>
    private void AdvanceStep()
    {
        _currentAnimationPosIndex++;

        if (_currentAnimationPosIndex >= _pos.Count)
        {
            if (_isLoop)
            {
                _currentAnimationPosIndex = 0;
            }
            else
            {
                enabled = false;
                return;
            }
        }
        PrepareStep();
    }
}
