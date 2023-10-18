# Rigidbody First Person Controller Tutorial.

This tutorial shows how to create an easily expandable first person rigidbody controller in Unity.

## 1. Create a new scene and set up the player.

Start by creating a new empty scene calling it whatever you like.

Create a new `Layer` and name it `Ground`, set your ground to this new layer, this will be used for ground detection later.

Create a new empty game object and name it `Player`.

To this game object, add a rigidbody and set the `Collision Detection` to `Continuous`, set `Interpolate` to `Interpolate`,  and freeze the `X`, `Y` and `Z` rotation under `Constraints`.

Under the `Player` game object, create a 3D capsule to act as the player object and name this to `PlayerObject`.

Under the `Player` game object, create a empty game object to act as the player orientation and name this to `Orientation`.

Under the `Player` game object, create a empty game object to act as the camera position and name this to `CameraPosition` and move this to where you would like the camera to be positioned on the player.

Now create a new empty game object seperate from the `Player` and name this `CameraHolder`, drag the `Main Camera` under this new game object and reset the transform via the inspector.

## 2. Create scripts to control the camera.

### Script to move camera position with the position of the player.

In the `Project` window of the editor, create a folder and call it `Scripts`.

Under this new folder create a new script and call it `MoveCamera`.

In this script we are making sure that the `CameraHolder` is set to the `CameraPosition` game object under the `Player`:
```.cs
public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
```

Now go back into Unity and drag the `MoveCamera` script under the `CameraHolder` game object, now under `Camera Position` in the script, drag the `CameraPosition` game object from under the `Player` into this field.

### Script to control the movement of the camera.

In the scripts folder create a new script and call it `PlayerCamera`.

This script contols the movement of the `Main Camera` with your mouse:
```.cs
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
```

Now go back into Unity and drag the `PlayerCamera` script onto the `Main Camera` object, assign the `Orientation` object to the `Camera Orientaion` field int the script. You can now adjust the `Camera Clamp Angle` to your liking, this stops the camera from going past the given threshold when moving the `Main Camera` verticaly.

## 3. Create scripts to control the player.

### Script to control player movement.

In the scripts folder create a new script and call it `PlayerController`.

This script contols the movement of the `Player` with inputs from your keyboard:




