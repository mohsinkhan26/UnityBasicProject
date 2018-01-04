/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(ButtonExtension), true)]
    [CanEditMultipleObjects]
    public class ButtonExtensionEditor : ButtonEditor
    {
        SerializedProperty m_Text;
        SerializedProperty m_Animator;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_Text = serializedObject.FindProperty("m_textComponent");
            m_Animator = serializedObject.FindProperty("m_animator");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_Text);
            EditorGUILayout.PropertyField(m_Animator);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
