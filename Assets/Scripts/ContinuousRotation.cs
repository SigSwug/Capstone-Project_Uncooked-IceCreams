using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    // Public variables to control rotation speed
    public float rotationSpeedX = 10f;
    public float rotationSpeedY = 10f;
    public float rotationSpeedZ = 10f;

    // Update is called once per frame
    void Update()
    {
        // Calculate rotation for each axis
        float rotationX = rotationSpeedX * Time.deltaTime;
        float rotationY = rotationSpeedY * Time.deltaTime;
        float rotationZ = rotationSpeedZ * Time.deltaTime;

        // Apply rotation to the object
        transform.Rotate(rotationX, rotationY, rotationZ);
    }
}
