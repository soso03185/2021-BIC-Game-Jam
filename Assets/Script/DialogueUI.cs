using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueUI : MonoBehaviour
{
    public Text textui;

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

}
