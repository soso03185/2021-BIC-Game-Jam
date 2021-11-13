using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPooler : MonoBehaviour
{
    public TextElement[] texts;
    public RectTransform ButtonPanel;
    public TextElement FirstText;
    public TextElement LastText;

    public TextElement GetFirstText(int stageindex =0)
    {
        FirstText.text = GameStringBuilder.StageFirstString(stageindex);
        return FirstText;
    }

    public TextElement GetLastText(int stageindex = 0)
    {
        LastText.text = GameStringBuilder.StageLastString(stageindex);
        return LastText;
    }

    public TextElement GetEventText(int eventindex =0)
    {
        for(int i=0;i<texts.Length;i++)
        {
            if (!texts[i].gameObject.activeInHierarchy)
            {
                texts[i].text = GameStringBuilder.EventString(eventindex);
                return texts[i];
                
            }
        }
        return null;
    }


}
