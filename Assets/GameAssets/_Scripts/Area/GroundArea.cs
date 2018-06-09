using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundArea : MonoBehaviour
{
    // Colores al pasar por encima / seleccionar un area
    // TODO: Cambiar por un cambio en el material, que refleje el cambio de área sin alterar el color
    [SerializeField] Color hoverColor;
    [SerializeField] Color selectedColor;

    public bool hit = false;

    // Area properties
    [System.Serializable]
    public class Properties
    {
        public string name = "Ground";
        public Faction faction;
    }
    public Properties props;

    MeshRenderer rend;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();

        AreaController.AddArea(this);
    }

    private void Update()
    {
        if (hit)
        {
            rend.material.color = hoverColor;
            hit = false;

            if (Input.GetMouseButtonUp(0))
            {
                AreaController.SelectArea(this);
            }
        }
        else
        {
            rend.material.color = props.faction.color;
        }

        if (AreaController.selectedArea == this)
        {
            rend.material.color = selectedColor;
        }
    }

    private void OnDrawGizmos()
    {
        Renderer renderer = GetComponent<Renderer>();

        drawString(props.name, renderer.bounds.center);
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
