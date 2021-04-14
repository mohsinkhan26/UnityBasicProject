/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System;

namespace UnityEngine.UI
{
    /// <summary>
    /// It extends Toggle component makes accesible of its Text component and animator if attached
    /// </summary>
    [AddComponentMenu("UI/Extensions/Text Extended"), RequireComponent(typeof(RectTransform))]
    public class TextExtension : Text
    {
        [Header("Extended Properties")] [SerializeField]
        string
            m_textId;

        [SerializeField] Animator
            m_animator;

        public string TextId
        {
            get { return m_textId; }
            set { m_textId = value; }
        }

        public Animator AnimatorController
        {
            get
            {
                if (m_animator == null)
                    throw new NullReferenceException("Animator component is not set in Inspector on " + name);
                else
                    return m_animator;
            }
            set
            {
                if (m_animator == null)
                    throw new NullReferenceException("Animator component is not set in Inspector on " + name);
                else
                    m_animator = value;
            }
        }
    }
}
