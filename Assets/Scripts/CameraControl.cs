using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{

    public float sensitivity = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2)) //middle mouse button
        {
            float viewYaw = Input.GetAxis("Mouse X");
            float viewPitch = Input.GetAxis("Mouse Y");

            transform.RotateAround(transform.position, Vector3.up, viewYaw * sensitivity);

            transform.RotateAround(transform.position, -transform.right, viewPitch * sensitivity);

            if (transform.localEulerAngles.x > 30 && transform.localEulerAngles.x < 180)
            {
                transform.localEulerAngles = new Vector3(30, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            if (transform.localEulerAngles.x < 5 || transform.localEulerAngles.x > 180)
            {
                transform.localEulerAngles = new Vector3(5, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
        }

    }
}
