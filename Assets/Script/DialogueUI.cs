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
                    ExecuteDialogue("월요일");
                    break;
                case 1:
                    ExecuteDialogue("화요일");
                    break;
                case 2:
                    ExecuteDialogue("수요일");
                    break;
                case 3:
                    ExecuteDialogue("목요일");
                    break;
                case 4:
                    ExecuteDialogue("금요일");
                    break;
                default:
                    break;
            }
        }
    }

}
