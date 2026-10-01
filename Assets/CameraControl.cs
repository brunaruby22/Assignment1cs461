using UnityEngine;
using UnityEngine.InputSystem;

//first requested script, that once attached to the camera and ran, should allow for the camera view to move as controlled through the specific keys by user
public class CameraControl : MonoBehaviour
{
    //speed variables that may be changed in the inspector view, one for movement speed and the other for the rotation speed.
    public float speed = 30f;
    public float turnSpeed = 60f;

    void Update() //makes input be listened to
    {
        if (Keyboard.current == null) return; //ensures the keyboard is connected and working

        //movement
        float moveX = 0f;
        float moveZ = 0f;

        //updates direction based on key pressed
        if (Keyboard.current.wKey.isPressed) moveZ += 1f; //forward
        if (Keyboard.current.sKey.isPressed) moveZ -= 1f; //backward
        if (Keyboard.current.dKey.isPressed) moveX += 1f; //right
        if (Keyboard.current.aKey.isPressed) moveX -= 1f; //left

        //get the direction the camera facing and frame rate, actual movement
        Vector3 moveInput = (transform.forward * moveZ + transform.right * moveX).normalized; //this .normalized is used to prevent faster movement when pressing two directions at the same time.
        transform.position += moveInput * speed * Time.deltaTime; //updates position visuals related to time and frame, for smooth moving

        //rotation
        float turnInput = 0f;

        //for whether 'Q' or 'E' is read, camera rotates
        if (Keyboard.current.eKey.isPressed) turnInput += 1f; //turn Right
        if (Keyboard.current.qKey.isPressed) turnInput -= 1f; //turn Left

        //rotate camera around the global Y-axis over time
        transform.Rotate(Vector3.up, turnInput * turnSpeed * Time.deltaTime, Space.World);
    }
}