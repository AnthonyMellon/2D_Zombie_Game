using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VoidEvent))]
public class EventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        VoidEvent myVoidEvent = (VoidEvent)target;

        if(GUILayout.Button("Raise Event"))
        {
            myVoidEvent.Raise();
        }
    }
}
