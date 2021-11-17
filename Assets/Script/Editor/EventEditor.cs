using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Event))]
public class EventEditor : Editor
{
    private Event[] events = null;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Event targetevent = (Event)target;

        if (targetevent.index >= Stage.TotalEventsAmount)
        {
            EditorGUILayout.HelpBox("인덱스가 허용되는 범위를 넘어섰습니다.", MessageType.Warning);
            return;
        }

        events = GameObject.FindWithTag("Event").gameObject.GetComponentsInChildren<Event>(true);

        int count = 0;
        foreach(var item in events)
        {
            if (targetevent.index == item.index)
                count++;
        }   

        if (count > 1)
            EditorGUILayout.HelpBox("인덱스가 중복됩니다.", MessageType.Warning);

    }
}
