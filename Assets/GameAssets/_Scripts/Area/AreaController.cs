using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Text factionName;

    public static GroundArea selectedArea;
    static List<GroundArea> areaList;

    static AreaController instance;

    private void Start()
    {
        instance = this;

        factionName.gameObject.SetActive(false);
        areaList = new List<GroundArea>();
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

    public static void AddArea(GroundArea area)
    {
        areaList.Add(area);
    }

    public static void SelectArea(GroundArea area)
    {
        if (area == selectedArea)
        {
            selectedArea = null;
            instance.factionName.gameObject.SetActive(false);
        }
        else
        {
            selectedArea = area;
            instance.factionName.gameObject.SetActive(true);
            instance.factionName.text = area.props.name + " (" + area.props.faction.factionName + ")";
        }
    }
}
