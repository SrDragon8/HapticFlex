using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HapticDevice))]
public class HapticDeviceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HapticDevice script = (HapticDevice)target;
        // Use this to maintain the original inspector look
        base.OnInspectorGUI();

        EditorGUI.BeginDisabledGroup(true); // Disable editing

        // Start custom GUI for private variables
        EditorGUILayout.IntField("Device Number", script.DeviceNumber);

        // haptic device transform
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Haptic Device Transform", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        EditorGUILayout.Vector3Field("Position", script.Position);
        EditorGUILayout.Vector4Field("Rotation", script.Rotation.eulerAngles);
        EditorGUI.indentLevel--;

        // haptic device physics
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Haptic Device Physics", EditorStyles.boldLabel);
        EditorGUI.indentLevel++; 
        EditorGUILayout.FloatField("Mass", script.Mass);
        EditorGUILayout.Vector3Field("Force", script.Force);
        EditorGUILayout.Vector3Field("Torque", script.Torque);
        EditorGUI.indentLevel--;

        // button states
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Haptic Device Button States", EditorStyles.boldLabel);
        EditorGUI.indentLevel++; // Indent for a cleaner look

        // Assuming the HapticDevice has a property 'ButtonStates' which returns an array of button states
        bool[] buttons = script.ButtonStates;

        if (buttons != null) // Check if the buttons array is not null
        {
            // Loop through the array and create a toggle for each button
            int numButtons = buttons.Length; // Assuming 4 buttons, adjust if you have a different number
            for (int i = 0; i < numButtons; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Toggle($"Button {i}", buttons[i]);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.indentLevel--; // Reset indent
        }

        EditorGUI.EndDisabledGroup(); // Enable GUI editing after read-only fields

        // Apply changes to the serialized properties
        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
