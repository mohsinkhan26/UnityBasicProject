/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(ToggleInputExtension), true)]
    [CanEditMultipleObjects]
    public class ToggleInputExtensionEditor : ToggleEditor
    {
        SerializedProperty m_InputFieldComponent;
        SerializedProperty m_Animator;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_InputFieldComponent = serializedObject.FindProperty("m_inputFieldComponent");
            m_Animator = serializedObject.FindProperty("m_animator");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_InputFieldComponent);
            EditorGUILayout.PropertyField(m_Animator);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
