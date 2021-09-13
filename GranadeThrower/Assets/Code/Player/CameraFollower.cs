using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Vector3 _cameraOffset;

    [Range(0.1f, 1.0f)]
    [SerializeField] private float _smoothFactor = 0.5f;

    [SerializeField] private Transform _playerTransform;

    private void Start()
    {
        _cameraOffset = transform.position - _playerTransform.position;
    }

    private void Update()
    {
        Vector3 newPos = _playerTransform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, _smoothFactor);
    }
}
