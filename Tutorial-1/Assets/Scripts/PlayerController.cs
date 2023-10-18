using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Properties")] 
    public float movementSpeed = 70f;
    public float groundDrag = 5f;

    [Header("Ground Check")] 
    public float playerHeight = 2f;
    public LayerMask groundLayer;
    private bool _isGrounded;

    public Transform orientation;

    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _moveDirection;
    private Rigidbody _playerRigidbody;

    private void Start()
    {
        //Find rigidbody on player and assign it to playerRigidbody value and lock the rotation
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        GroundCheck();
        GetInput();
        HandleDrag();
    }

    private void FixedUpdate()
    {
        PlayerControl();
    }

    private void GetInput()
    {
        //Read values from keyboard and assign to horizontal and vertical values
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void PlayerControl()
    {
        //Get direction from orientation component
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        
        //Apply a movement force to the player
        _playerRigidbody.AddForce(_moveDirection.normalized * movementSpeed, ForceMode.Force);
    }
    
    private void GroundCheck()
    {
        //Shoot a raycast down from the player to check if the player is grounded
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
    }

    private void HandleDrag()
    {
        //If the player is grounded, set the drag levels to the groundDrag value otherwise set to 0
        _playerRigidbody.drag = _isGrounded ? groundDrag : 0f;
    }
}
