using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundArea : MonoBehaviour
{
    [SerializeField] Color normalColor;
    [SerializeField] Color hoverColor;
    [SerializeField] Color selectedColor;
    public bool hit = false;

    MeshRenderer rend;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (hit)
        {
            rend.material.color = hoverColor;
            hit = false;

            AreaController.Up(0, this);
        }
        else
        {
            rend.material.color = normalColor;
        }

        if (AreaController.selectedArea == this)
        {
            rend.material.color = selectedColor;
        }
    }

    private void OnDrawGizmos()
    {
        CityDistrictsGenerator cdGen = GetComponent<CityDistrictsGenerator>();
        Renderer renderer = GetComponent<Renderer>();

        drawString(cdGen.siteName, renderer.bounds.center);
    }

    static void drawString(string text, Vector3 worldPos, Color? colour = null)
    {
        UnityEditor.Handles.BeginGUI();
        if (colour.HasValue) GUI.color = colour.Value;
        var view = UnityEditor.SceneView.currentDrawingSceneView;
        Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
        Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
        GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
        UnityEditor.Handles.EndGUI();
    }
}
