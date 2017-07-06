/* 
 * Author : Mohsin Khan
 * Website : http://mohsinkhan.000webhostapp.com/
*/
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(TextExtension), true)]
    [CanEditMultipleObjects]
    public class TextExtensionEditor : TextEditor
    {
        SerializedProperty m_TextId;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_TextId = serializedObject.FindProperty("m_textId");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_TextId);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
