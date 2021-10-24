using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    Transform tr;
    public float x;
    public float z;

    private float rotX = 0.0f;
    private float rotY = 0.0f;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotX += mouseX;
        rotY += mouseY;

        tr.localRotation = Quaternion.Euler(rotY, rotX, 0.0f);
        Vector3 move = tr.forward * z + tr.right * x;

        tr.position += move;
    }

}
