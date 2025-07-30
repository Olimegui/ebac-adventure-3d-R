using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gm = (GameManager)target;

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("Estado Atual do Jogo:", gm.CurrentState.ToString());

        EditorGUILayout.Space(10);


        if (GUILayout.Button("Jump"))
            gm.SwitchState(GameManager.GameStates.JUMP);

        if (GUILayout.Button("Land"))
            gm.SwitchState(GameManager.GameStates.LAND);

        if (GUILayout.Button("Death"))
            gm.SwitchState(GameManager.GameStates.DEATH);
    }
}