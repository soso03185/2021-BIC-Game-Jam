using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameString : MonoBehaviour
{
    public TextPooler Eventtext;
    public float typingspeed = 0.05f;
    private RectTransform _recttransform;
    private AudioSource audiosource;
    public int eventStringSize = 50, LastStringSize = 52;


    public void AddStageCount() { Stage.StageCount++; }

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _recttransform = GetComponent<RectTransform>();

        TextElement firsttext = Eventtext.GetFirstText(Stage.StageCount);
        firsttext.transform.SetParent(transform);

        List<TextElement> eventtexts = new List<TextElement>();
        if (Stage.currentActivatedTriggers != null)
        {
            for (int i = 0; i < Stage.currentActivatedTriggers.Count; i++)
            {
                eventtexts.Add(new TextElement());
                eventtexts[i] = Eventtext.GetEventText(Stage.currentActivatedTriggers[i]);
                eventtexts[i].transform.SetParent(transform);
                eventtexts[i].gameObject.SetActive(true);
            }
        }

        TextElement lasttext = Eventtext.GetLastText(Stage.StageCount);
        lasttext.transform.SetParent(transform);
        StartCoroutine(TypeCoroutine(firsttext, eventtexts, lasttext));
        
    }

    private IEnumerator TypeCoroutine(TextElement first, List<TextElement> events, TextElement last)
    {
        audiosource.Play();

        for (int i = 0; i < events.Count; i++)
        {
            first.text = string.Format("{0}\n<size={1}>{2}</size>", first.text, eventStringSize, events[i].text);
        }

        first.text = string.Format("{0}\n<size={1}>{2}</size>", first.text, LastStringSize, last.text);

        first.textUI.DOText(first.text, first.text.Length * typingspeed).
            OnComplete(() =>
            {
                first.isTypeComplete = true;
            });

        yield return new WaitUntil(() => first.isTypeComplete == true);

        LayoutRebuilder.ForceRebuildLayoutImmediate(first.textUI.rectTransform);

        audiosource.Stop();
        Eventtext.ButtonPanel.gameObject.SetActive(true);

        LayoutRebuilder.ForceRebuildLayoutImmediate(_recttransform);


    }
}
