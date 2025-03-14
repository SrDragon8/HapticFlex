using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(ExtendedButton), editorForChildClasses: true)]

public class ExtendedButtonEditor : ButtonEditor
{
    private SerializedProperty HoverSoundProperty;
    private SerializedProperty clickSoundProperty;
    private SerializedProperty audioSourceProperty;
    private SerializedProperty onHoverEnterProperty;
    private SerializedProperty onHoverExistProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        HoverSoundProperty = serializedObject.FindProperty("HoverSound");
        clickSoundProperty = serializedObject.FindProperty("ClickSound");
        audioSourceProperty = serializedObject.FindProperty("audioSource");
        onHoverEnterProperty = serializedObject.FindProperty("onHoverEnter");
        onHoverExistProperty = serializedObject.FindProperty("onHoverExit");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Extended Button Properties", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(HoverSoundProperty);
        EditorGUILayout.PropertyField(clickSoundProperty);
        EditorGUILayout.PropertyField(audioSourceProperty);

        //Add a helpful note about audio source
        EditorGUILayout.HelpBox("If audio source is not assigned, one will be added automatically", MessageType.Info);

        // Hover Events Section

        EditorGUILayout.PropertyField(onHoverEnterProperty);
        EditorGUILayout.PropertyField(onHoverExistProperty);


        serializedObject.ApplyModifiedProperties();
    }
}
