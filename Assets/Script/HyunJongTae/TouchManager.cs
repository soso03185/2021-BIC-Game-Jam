using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    public DrawManager draw;

    [SerializeField] private Transform drawPen;
    [SerializeField] private GameObject drawPoint;

    [SerializeField] private GameObject loading;

    public Text[] console;    

    public bool drawingMode;
    private bool press;


    public void OnPointerDown()
    {
        press = true;
    }

    public void OnPointerUp()
    {
        press = false;
        draw.StopDrawLine();
    }

    public void DrawingMode()
    {
        if (drawingMode)
        {
            OnPointerUp();
            drawingMode = false;            
        }
        else
        {
            drawingMode = true;           
        }
    }

    private void Update()
    {

        Ray rayF;
        RaycastHit hitObjf;

        rayF = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Input.GetMouseButtonDown(0))
        {
            OnPointerDown();
        }
        if (Input.GetMouseButton(0))
        {
            draw.pivot.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,2);
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnPointerUp();
        }

        //if (Input.touchCount == 0) return;

        //Touch touch = Input.GetTouch(0);

        //Ray rayC;
        //RaycastHit hitObj;

        //rayC = Camera.main.ScreenPointToRay(touch.position);

        //if (Physics.Raycast(rayC, out hitObj, 20000, 1 << LayerMask.NameToLayer("Touch")))
        //{            
        //    ITouchObject entity = hitObj.collider.GetComponent<ITouchObject>();
        //    //console[1].text = "[console/Touch] : search touch";
        //    if (entity != null)
        //    {
        //        entity.Touch();
        //        //console[1].text = "[console/Touch] : Send Message";
        //        loading.SetActive(true);
        //    }
        //    console[0].text = hitObj.collider.name;
        //}

        //if (!drawingMode) return;

        //if (Input.touchCount > 0)
        //{

        //    if (touch.phase == TouchPhase.Began) OnPointerDown();
        //    else if (touch.phase == TouchPhase.Ended) OnPointerUp();

        //    var ray = Camera.main.ScreenPointToRay(touch.position);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        drawPen.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        //    }
        //}

        if (press)
        {
            draw.StartDrawLine();
        }
    }

}
