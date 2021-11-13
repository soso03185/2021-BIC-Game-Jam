using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextElement : MonoBehaviour
{
    public string text;
    public Text textUI;
    public bool isTypeComplete = false;

    private void Awake()
    {
        textUI = GetComponent<Text>();
    }

    public void Type()
    {
        textUI.DOText(text, 3);
    }
}
