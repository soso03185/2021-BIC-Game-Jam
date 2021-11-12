using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPooler : MonoBehaviour
{
    public Text[] texts;
    public RectTransform ButtonPanel;
    public Text FirstText;
    public Text LastText;

    public Transform GetFirstText(int stageindex =0)
    {
        FirstText.text = GameStringBuilder.StageFirstString(stageindex);
        return FirstText.transform;
    }

    public Transform GetLastText(int stageindex = 0)
    {
        LastText.text = GameStringBuilder.StageLastString(stageindex);
        return LastText.transform;
    }

    public Transform GetEventText(int eventindex =0)
    {
        for(int i=0;i<texts.Length;i++)
        {
            if (!texts[i].gameObject.activeInHierarchy)
            {
                texts[i].text = GameStringBuilder.EventString(eventindex);
                texts[i].gameObject.SetActive(true);
                return texts[i].transform;
                
            }
        }
        return null;
    }


}
