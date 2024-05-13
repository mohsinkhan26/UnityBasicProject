/*
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 */

using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(ButtonTMPExtension), true)]
    [CanEditMultipleObjects]
    public class ButtonTMPExtensionEditor : ButtonEditor
    {
        SerializedProperty m_textTMP;
        SerializedProperty m_Animator;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_textTMP = serializedObject.FindProperty("m_textTMP");
            m_Animator = serializedObject.FindProperty("m_animator");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_textTMP);
            EditorGUILayout.PropertyField(m_Animator);

            serializedObject.ApplyModifiedProperties();
        }
    }
}