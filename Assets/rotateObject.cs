using UnityEngine;

//created and given by professor
public class RotateObject : MonoBehaviour
{
    // Rotation speed in degrees per second
    public float rotationSpeed = 30f;

    void Update()
    {
        // Rotate around the Y axis at rotationSpeed degrees per second
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}