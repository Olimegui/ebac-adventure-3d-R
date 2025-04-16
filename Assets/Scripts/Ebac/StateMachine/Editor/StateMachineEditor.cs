using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FSMExample))]
public class StateMachineEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FSMExample fsm = (FSMExample)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if(fsm.stateMachine == null) return;

        if(fsm.stateMachine.CurrentState != null)
            EditorGUILayout.LabelField("Current State", fsm.stateMachine.CurrentState.ToString());

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Avaiable States");

        if(showFoldout)
        {
            if(fsm.stateMachine.dictionaryState != null)
            {
                var Keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
                var Vals = fsm.stateMachine.dictionaryState.Values.ToArray();

                for(int i = 0; i < Keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", Keys[i], Vals[i]));
                }
            }
        }
    }
}
