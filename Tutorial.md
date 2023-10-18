# Rigidbody First Person Controller Tutorial.

This tutorial shows how to create an easily expandable first person rigidbody controller in Unity.

## 1. Create a new scene and set up the player.

Start by creating a new empty scene calling it whatever you like.

Create a new empty game object and name it `Player`.

To this game object, add a rigidbody and set the `Collision Detection` to `Continuous`, set `Interpolate` to `Interpolate`,  and freeze the `X`, `Y` and `Z` rotation under `Constraints`.

Under the `Player` game object, create a 3D capsule to act as the player object and name this to `PlayerObject`.

Under the `Player` game object, create a empty game object to act as the player orientation and name this to `Orientation`.

Under the `Player` game object, create a empty game object to act as the camera position and name this to `CameraPosition` and move this to where you would like the camera to be positioned on the player.

Now create a new empty game object seperate from the `Player` and name this `CameraHolder`, drag the `Main Camera` under this new game object and reset the transform via the inspector.

## 2. Create scripts for the player and camera controllers.

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

Now go back into Unity and drag the `MoveCamera` script under the `CameraHolder` game object, now under `Camera Position` in the script drag the `CameraPosition` game object from under the `Player` into this field.









