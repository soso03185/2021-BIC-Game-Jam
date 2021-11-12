using UnityEngine;
using UnityEngine.UI;

public class GameString : MonoBehaviour
{
    public TextPooler Eventtext;
    private RectTransform _recttransform;

    public void AddStageCount() { Stage.StageCount++; }

    private void OnEnable()
    {
        _recttransform = GetComponent<RectTransform>();

        Eventtext.GetFirstText(Stage.StageCount).SetParent(transform);
        if(Stage.currentActivatedTriggers != null)
        {
            for(int i =0;i< Stage.currentActivatedTriggers.Count;i++)
            {
                Eventtext.GetEventText(Stage.currentActivatedTriggers[i]).SetParent(transform);
            }
        }
        
        Eventtext.GetLastText(Stage.StageCount).SetParent(transform);
        Eventtext.ButtonPanel.SetParent(transform);

        LayoutRebuilder.ForceRebuildLayoutImmediate(_recttransform);
    }
}
