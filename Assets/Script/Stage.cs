using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public const int TotalEventsAmount = 10;
    public static int StageCount = 0;
    public static bool[] eventtriggers = new bool[TotalEventsAmount];
    public static List<int> currentActivatedTriggers = null;

    [SerializeField] private Event[] events = null;


    private void Awake()
    {
        //Connect Eventtriger datas to current event components;
        if(StageCount < 4)
        InitializeEvents();
        currentActivatedTriggers = new List<int>();
    }

    //씬이 시작될때 이벤트트리거의 데이터를 씬 내 이벤트에 업데이트.
    private void InitializeEvents()
    {
        //Find scene event components
        events = GameObject.FindWithTag("Event").gameObject.GetComponentsInChildren<Event>(true);

        //Connect Eventtriger datas to current event components;
        if (events != null)
        {
            foreach (Event item in events)
            {
                item.isActivated = eventtriggers[item.index];
            }

            int randomamount = Random.Range(1, 2);
            for(int i=0; i<randomamount; i++)
            {
                int random;
                do { random = Random.Range(0, events.Length); }
                while (events[random].isActivated == true);

                events[random].gameObject.SetActive(true);
            }
        }
        
    }

    //게임중 이벤트가 실행된 여부를 이벤트트리거에 업데이트함.
    public void UpdateEventTriggers()
    {
        if (events != null && eventtriggers != null)
        {
            for(int i=0; i<TotalEventsAmount; i++)
            {
                foreach (var item in events)
                {
                    if (i == item.index)
                        eventtriggers[i] = item.isActivated;
                }
            }
        }
        
    }


}
