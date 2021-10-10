using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    Transform tr;
    public float x;
    public float y;
    public float z;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Mouse ScrollWheel") * -10.0f;
        z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, y, z);

        tr.position += move;
    }

}
