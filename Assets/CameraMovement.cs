using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform tr;
    public float x;
    public float y;
    public float z;
    // Start is called before the first frame update
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

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 25.0f, 1 << 9);
            GameObject entity = hit.collider.gameObject;
            VegetationController veg = entity.GetComponent<VegetationController>();
        }

    }
}
