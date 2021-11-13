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

        first.textUI.DOText(first.text, first.text.Length * typingspeed).
            OnComplete(() =>
            {
                first.isTypeComplete = true;
            });

        yield return new WaitUntil(() => first.isTypeComplete == true);

        LayoutRebuilder.ForceRebuildLayoutImmediate(first.textUI.rectTransform);

        yield return new WaitForSeconds(0.5f);

        for (int i=0; i<events.Count;i++)
        {
            audiosource.Play();
            events[i].textUI.DOText(events[i].text, events[i].text.Length * typingspeed).OnComplete(() =>
            {
                events[i].isTypeComplete = true;
            });

            yield return new WaitUntil(() => events[i].isTypeComplete == true);

            LayoutRebuilder.ForceRebuildLayoutImmediate(events[i].textUI.rectTransform);
        }

        yield return new WaitForSeconds(0.5f);

        audiosource.Play();
        last.textUI.DOText(last.text, last.text.Length * typingspeed).OnComplete(() =>
        {
            last.isTypeComplete = true;
        });

        yield return new WaitUntil(() => last.isTypeComplete == true);

        LayoutRebuilder.ForceRebuildLayoutImmediate(last.textUI.rectTransform);

        yield return new WaitForSeconds(0.5f);

        audiosource.Stop();
        Eventtext.ButtonPanel.SetParent(transform);

        LayoutRebuilder.ForceRebuildLayoutImmediate(_recttransform);
    }
}
