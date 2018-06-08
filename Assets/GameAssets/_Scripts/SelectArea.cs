using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectArea : MonoBehaviour
{
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(transform.position, ray.direction * 40, Color.red, .1f);

        Physics.Raycast(ray, out hit);
        
        if (hit.collider)
        {
            GroundArea area = hit.collider.GetComponent<GroundArea>();
            if (area)
            {
                area.hit = true;
            }
        }
    }
}
