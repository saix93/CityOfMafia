using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    [SerializeField] Camera cam;

    public static GroundArea selectedArea;

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

    public static bool Up(int mouseButton, GroundArea area)
    {
        // Al levantar el botón del ratón
        if (Input.GetMouseButtonUp(mouseButton))
        {
            selectedArea = area == selectedArea ? null : area;

            return true;
        }

        return false;
    }
}
