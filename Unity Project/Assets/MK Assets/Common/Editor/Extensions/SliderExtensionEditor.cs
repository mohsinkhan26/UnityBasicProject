/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(SliderExtension), true)]
    [CanEditMultipleObjects]
    public class SliderExtensionEditor : SliderEditor
    {
        SerializedProperty m_Label;
        SerializedProperty m_Value;
        SerializedProperty m_Animator;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_Label = serializedObject.FindProperty("m_labelComponent");
            m_Value = serializedObject.FindProperty("m_valueComponent");
            m_Animator = serializedObject.FindProperty("m_animator");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_Label);
            EditorGUILayout.PropertyField(m_Value);
            EditorGUILayout.PropertyField(m_Animator);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
