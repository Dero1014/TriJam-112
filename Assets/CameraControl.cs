using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float Sensitivity;

    Vector3 _tempCameraRot;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        float inputX = -(Input.GetAxisRaw("Mouse Y")) * Sensitivity * Time.deltaTime;
;
        float inputY = (Input.GetAxisRaw("Mouse X")) * Sensitivity * Time.deltaTime;

        float cX = transform.localEulerAngles.x;
        float cY = transform.localEulerAngles.y;
        float cZ = transform.localEulerAngles.z;

        float rotUpDown = cX + inputX;
        float rotLeftRight = cY + inputY;  

        transform.rotation = Quaternion.Euler(rotUpDown, rotLeftRight, cZ);

    }
}
