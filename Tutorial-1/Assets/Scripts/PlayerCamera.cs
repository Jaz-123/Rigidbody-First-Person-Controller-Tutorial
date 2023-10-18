using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera Properties")]
    public float xSensitivity = 400f;
    public float ySensitivity = 400f;
    [Range(40f, 90f)]public float cameraClampAngle = 80f;

    public Transform cameraOrientation;

    private float _xRotation;
    private float _yRotation;
    private float _mouseX;
    private float _mouseY;

    private void Start()
    {
        //Hide cursor and lock it to the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Get input from mouse
        _mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * xSensitivity;
        _mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * ySensitivity;
        _yRotation += _mouseX;
        _xRotation -= _mouseY;
        //Clamp camera angle
        _xRotation = Mathf.Clamp(_xRotation, -cameraClampAngle, cameraClampAngle);
        //Apply rotations to camera and orientation
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        cameraOrientation.rotation = Quaternion.Euler(0f, _yRotation, 0f);
    }
}
