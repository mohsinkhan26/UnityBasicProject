/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/unbounded-eagle/ 
*/
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(ToggleExtension), true)]
    [CanEditMultipleObjects]
    public class ToggleExtensionEditor : ToggleEditor
    {
        SerializedProperty m_Text;
        SerializedProperty m_TextExtension;
        SerializedProperty m_Animator;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_Text = serializedObject.FindProperty("m_textComponent");
            m_TextExtension = serializedObject.FindProperty("m_textExtensionComponent");
            m_Animator = serializedObject.FindProperty("m_animator");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_Text);
            EditorGUILayout.PropertyField(m_TextExtension);
            EditorGUILayout.PropertyField(m_Animator);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
