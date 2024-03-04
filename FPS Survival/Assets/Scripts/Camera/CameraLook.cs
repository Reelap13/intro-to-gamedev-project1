using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{

    public InputManager inputManager;
    public float mouseSesetivity;
    public Transform body;

    private float xRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        verticalRotation();
        horizontalRotation();
        
    }

    void verticalRotation()
    {
        float mouseY = inputManager.inputMaster.CameraLook.MouseY.ReadValue<float>() * mouseSesetivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    void horizontalRotation()
    {
        float mouseX = inputManager.inputMaster.CameraLook.MouseX.ReadValue<float>() * mouseSesetivity * Time.deltaTime;
        body.Rotate(Vector3.up * mouseX);
    }
}
