using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class outline : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;
    [Range(0, 1000)]
    public float radius = 5;
    LineRenderer line;

   // public Material mat;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = true;

        
     //   mat.SetColor("_Color", new Color(1f, 1f, 1f, 0f));
        CreatePoints();
    }

    private void Update()
    {
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;
        float z;

        float angle = 0f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x + transform.position.x, 0 + transform.position.y, y + transform.position.z));

            angle += (360f / segments);
        }
    }
}