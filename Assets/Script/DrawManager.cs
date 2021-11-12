using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{    
    public GameObject lineRendererPrefab;
    private LineRenderer lineRenderer;
    public List<LineRenderer> lineList = new List<LineRenderer>();
    public Transform linePool;

    public bool use;
    public bool startLine;

    public Transform pivot;

    private Color color;


    private void Awake()
    {
        color = Color.white;
    }

    private void Update()
    {
        if (use)
        { 
            if (startLine)
            {
                DrawLineContinue();
            }
        }
    }

    public void MakeLineRenderer()
    {
        GameObject lineObj = Instantiate(lineRendererPrefab);
        lineObj.transform.SetParent(linePool);
        lineObj.transform.position = Vector3.zero;
        lineObj.transform.localScale = new Vector3(1, 1, 1);

        lineRenderer = lineObj.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, pivot.position);

        startLine = true;
        lineList.Add(lineRenderer);
        lineRenderer.SetColors(color, color);
    }

    public void DrawLineContinue()
    {
        lineRenderer.positionCount = lineRenderer.positionCount + 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pivot.position);
    }

    public void StartDrawLine()
    {        
        use = true;
        if (!startLine) MakeLineRenderer();
    }

    public void StopDrawLine()
    {
        use = false;
        startLine = false;
        lineRenderer = null;
    }

    public void EraseAll()
    {
        for (int i = 0; i < linePool.childCount; i++)
        {
            Destroy(linePool.GetChild(i).gameObject);
        }
        lineList.Clear();
    }

    public void LineThikness(float value)
    {
        lineRenderer.SetWidth(value, value);
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }
}
