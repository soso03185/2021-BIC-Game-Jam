using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameString : MonoBehaviour
{
    public TextPooler Eventtext;
    private RectTransform _recttransform;

    public void AddStageCount() { Stage.StageCount++; }

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

        first.textUI.DOText(first.text, 2).OnComplete(() =>
        {
            first.isTypeComplete = true;
        });

        yield return new WaitUntil(() => first.isTypeComplete == true);

        yield return new WaitForSeconds(0.5f);

        for (int i=0; i<events.Count;i++)
        {
            events[i].textUI.DOText(events[i].text, 2).OnComplete(() =>
            {
                events[i].isTypeComplete = true;
            });

            yield return new WaitUntil(() => events[i].isTypeComplete == true);
        }

        yield return new WaitForSeconds(0.5f);


        last.textUI.DOText(last.text, 2).OnComplete(() =>
        {
            last.isTypeComplete = true;
        });

        yield return new WaitUntil(() => last.isTypeComplete == true);

        yield return new WaitForSeconds(0.5f);

        Eventtext.ButtonPanel.SetParent(transform);

        LayoutRebuilder.ForceRebuildLayoutImmediate(_recttransform);
    }
}
