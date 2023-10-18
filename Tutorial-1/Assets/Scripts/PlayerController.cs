using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Properties")] 
    public float movementSpeed = 70f;
    public float groundDrag = 5f;
    public float jumpForce = 12f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.4f;
    private bool _readyToJump;

    [Header("Ground Check")] 
    public float playerHeight = 2f;
    public LayerMask groundLayer;
    private bool _isGrounded;

    [Header("Keybindings")] 
    public KeyCode jumpKey = KeyCode.Space;

    [Header("References")]
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
        //Set the initial jump to be ready
        _readyToJump = true;
    }

    private void Update()
    {
        GroundCheck();
        GetInput();
        SpeedControl();
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
        //Check for jump input
        if (Input.GetKey(jumpKey) && _readyToJump && _isGrounded)
        {
            _readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void PlayerControl()
    {
        //Get direction from orientation component
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        
        //Apply a movement force to the player
        if(_isGrounded)
            _playerRigidbody.AddForce(_moveDirection.normalized * movementSpeed, ForceMode.Force);
        else if(!_isGrounded)
            _playerRigidbody.AddForce(_moveDirection.normalized * movementSpeed * airMultiplier, ForceMode.Force);
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

    private void SpeedControl()
    {
        //Calculate the flat velocity of the player and if it exceds the movement speed then calculate the force to apply to 
        //limit the speed of the player
        Vector3 flatVelocity = new Vector3(_playerRigidbody.velocity.x, 0f, _playerRigidbody.velocity.z);
        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            _playerRigidbody.velocity = new Vector3(limitedVelocity.x, _playerRigidbody.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        //Handle jump mechanics
        //Reset y velocity to ensure the player jumps the same height each time
        _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, 0f, _playerRigidbody.velocity.z);
        _playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        //This function resets the bool readyToJump
        _readyToJump = true;
    }
}
