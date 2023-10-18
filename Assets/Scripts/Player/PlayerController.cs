using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float positiveMaxSpeed;
    [SerializeField] private float negativeMaxSpeed;
    [SerializeField] private float speedIncreasingRate;
    [SerializeField] private float speedDecreasingRate;
    
    [Header("Stop moving")]
    [SerializeField] private float dampingSpeedFactor=0.9f;
    [SerializeField] private float minimalMovingSpeed;
   
    [Header("Physical stats")] 
    [SerializeField] private float forceMultiplayer;
    
    [Header("Rotating")] 
    [SerializeField] private Vector3 rotationSpeed;
    
    [Header("Debug")]
    [SerializeField] private float currentSpeed;
    
    private InputHandler _inputHandler = new InputHandler();
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = _inputHandler.MoveInput;
        ChangeSpeed(moveInput.y);
        Vector2 mouseInput = _inputHandler.MouseInput;
        _rigidbody.AddForce(transform.forward * currentSpeed * forceMultiplayer  * Time.fixedDeltaTime, ForceMode.VelocityChange);
        _rigidbody.AddRelativeTorque(Vector3.Scale(new Vector3(-mouseInput.y, mouseInput.x, -moveInput.x), rotationSpeed*Time.fixedDeltaTime), ForceMode.VelocityChange);
        
    }

    private void ChangeSpeed(float direction)
    {
        switch (direction)
        {
            case >0: currentSpeed += direction * speedIncreasingRate;
                break;
            case <0: currentSpeed += direction * speedDecreasingRate;
                break;
            default: currentSpeed *= dampingSpeedFactor;
                break;
        }
        currentSpeed = Math.Clamp(currentSpeed, -negativeMaxSpeed, positiveMaxSpeed);

        if (Math.Abs(currentSpeed) < minimalMovingSpeed)
        {
            currentSpeed = 0;
        }
        
    }
    
}
