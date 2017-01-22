using UnityEngine;
using System.Collections;

public class CircleExpander : MonoBehaviour
{
    public int segments;
    public float radius;
    public float GrowthSpeed = 1;
    public float FadeSpeed = 1;

    public Vector3 StartSize = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 FinishSize = new Vector3(20, 20, 20);

    private LineRenderer line;
    private float alpha;
       

    void Start()
    {
        transform.localScale = StartSize;

        line = gameObject.GetComponent<LineRenderer>();

        line.numPositions = segments + 1;
        line.useWorldSpace = false;
        CreatePoints();
        
        alpha = 0.5f;
        line.material = new Material(Shader.Find("Particles/Additive (Soft)"));

        if (GrowthSpeed < 0)
            GrowthSpeed = 0.1f;
    }

    void FixedUpdate()
    {

        transform.localScale = Vector3.MoveTowards(transform.localScale, FinishSize, GrowthSpeed * Time.deltaTime);

        alpha -= Time.deltaTime * FadeSpeed;
        Color start = Color.white;
        start.a = alpha;
        Color end = Color.black;
        end.a = alpha;

        line.startColor = start;
        line.endColor = start;
    }

    private void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;

        float angle = 0f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle);
            y = Mathf.Cos(Mathf.Deg2Rad * angle);
            line.SetPosition(i, new Vector3(x, y, z) * radius);
            angle += (360f / segments);
        }
    }
}
