using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _transform;
    [SerializeField] private GrenadeThrower _grenadeThrower;
    private float _moveDirection;
    private float _rotateDirection;
    private bool _isAiming;

    private void OnEnable()
    {
        _grenadeThrower = GetComponent<GrenadeThrower>();
        _grenadeThrower.OnAimStay += StopByAiming;
        _grenadeThrower.OnGrenadeThrowed += CancelAiming;
    }

    private void OnDisable()
    {
        _grenadeThrower.OnAimStay -= StopByAiming;
        _grenadeThrower.OnGrenadeThrowed -= CancelAiming;
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_isAiming == false)
        {
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        _moveDirection = Input.GetAxis("Vertical");
        _transform.Translate(0,0,_moveDirection*_speed*Time.deltaTime);
    }

    private void Rotate() 
    {
        _rotateDirection = Input.GetAxis("Horizontal");
        _transform.Rotate(0, _rotateDirection*2, 0);
    }

    private void StopByAiming()
    {
        _isAiming = true;
    }

    private void CancelAiming() 
    {
        _isAiming = false;
    }
}
