using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueUI : MonoBehaviour
{
    public Text textui;
    public bool isYoIL = false;

    public void ExecuteDialogue(string dialogue)
    {
        textui.text = null;
        textui.DOKill();
        textui.DOFade(1, 0);
        textui.DOText(dialogue, 1.5f).OnComplete
            (() =>
               {
                   Debug.Log("Rmx");
                   textui
                    .DOFade(0, 1.5f)
                    .SetDelay(1.5f);
               }
            );
    }

    private void OnEnable()
    {
        if (isYoIL)
        {
            switch (Stage.StageCount)
            {
                case 0:
                    ExecuteDialogue("???");
                    break;
                case 1:
                    ExecuteDialogue("???");
                    break;
                case 2:
                    ExecuteDialogue("???");
                    break;
                case 3:
                    ExecuteDialogue("???");
                    break;
                case 4:
                    ExecuteDialogue("???");
                    break;
                default:
                    break;
            }
        }
    }

}
