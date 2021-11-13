using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingLine : MonoBehaviour
{
    private UnityEngine.UI.Extensions.UILineRenderer line;
    public GameObject lineRendererPrefab;
    private UnityEngine.UI.Extensions.UILineRenderer lineRenderer;
    public List<UnityEngine.UI.Extensions.UILineRenderer> lineList = new List<UnityEngine.UI.Extensions.UILineRenderer>();
    public Transform linePool;

    public bool use;
    public bool startLine;

    private Color color;

    [SerializeField] private int screenX;
    [SerializeField] private int screenY;


    private void Awake()
    {
        line = GetComponent<UnityEngine.UI.Extensions.UILineRenderer>();        
        
    }

    private void Start()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {        
        float randTime = 0;
        int count = 1;
     
        bool r = true;        

        while (count < line.Points.Length)
        {
            randTime = Random.Range(0.01f, 0.1f);

            int posX = Random.Range(-400, -300);
            int posY = Random.Range(400, 300);

            float time = 0;

            while (time < 0.001f)
            {
                for (int i = count; i < line.Points.Length; i++)
                {
                    line.Points[i] = Vector2.Lerp(line.Points[i], new Vector2(posX, posY), Time.deltaTime * 100);
                }
                line.SetAllDirty();
                time += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }

            count++;
            yield return new WaitForSecondsRealtime(randTime);
        }        
    }

}
