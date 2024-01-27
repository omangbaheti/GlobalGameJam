using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DogController : MonoBehaviour
{
    //Properties
    public float MoveSpeed = 2.0f;
    public float SprintSpeed = 5.335f;
    public float RotationSmoothTime = 2.0f;
    public float AccelerationTime = 2f;
    
    //Variables for Keeping Track of  Speed
    private float _speed;
    private float _timeSinceStartedRunning;
    private float _targetRotation;
    private float _rotationVelocity;
    
    //Variables for GameObjects
    private Camera _mainCamera;
    private Animator _animationController;
    private CharacterController _characterController;
    private InputSystem _input;
    private float _verticalVelocity = 0f;

    private const float offset = 0.1f;
    void Start()
    {
        _input = GetComponent<InputSystem>();
        //_animationController = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
    }

    private void OnValidate()
    {
        _input = GetComponent<InputSystem>();
        //_animationController = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;
        
        float currentVelocity = new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;
        if (currentVelocity < targetSpeed - offset || currentVelocity > targetSpeed + offset)
        {
            _timeSinceStartedRunning += Time.deltaTime;
            _speed = Mathf.Lerp(currentVelocity, targetSpeed, Mathf.Sin(_timeSinceStartedRunning/AccelerationTime * Mathf.PI/2));
            
        }
        else
        {
            _speed = targetSpeed;
            _timeSinceStartedRunning = 0f;
        }

        if (_input.move.Equals(Vector2.zero))
        {
            _timeSinceStartedRunning += Time.deltaTime;
            _speed = Mathf.Lerp(currentVelocity, 0f,
                Mathf.Sin(_timeSinceStartedRunning / AccelerationTime * Mathf.PI / 2));
            if (currentVelocity.Equals(Vector3.zero))
            {
                
            }
        }
       
        
        if (_input.move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
        
        Vector3 targetDirection = (Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward).normalized;

        _characterController.Move(targetDirection * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    
}
