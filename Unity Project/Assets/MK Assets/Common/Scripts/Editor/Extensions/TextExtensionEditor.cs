/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(TextExtension), true)]
    [CanEditMultipleObjects]
    public class TextExtensionEditor : TextEditor
    {
        SerializedProperty m_TextId;
        SerializedProperty m_Animator;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_TextId = serializedObject.FindProperty("m_textId");
            m_Animator = serializedObject.FindProperty("m_animator");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_TextId);
            EditorGUILayout.PropertyField(m_Animator);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
