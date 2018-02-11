/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/
using System;


namespace UnityEngine.UI
{
    /// <summary>
    /// It extends Button component makes accesible of its Text component and animator if attached
    /// </summary>
    [AddComponentMenu("UI/Extensions/Button Extended"), RequireComponent(typeof(RectTransform))]
    public class ButtonExtension : Button
    {
        [Header("Extended Properties")]
        [SerializeField]
        Text
            m_textComponent;

        [SerializeField]
        Animator
            m_animator;

        public Text TextComponent
        {
            get
            {
                if (m_textComponent == null)
                    throw new NullReferenceException("Text component is not set in Inspector on " + name);
                else
                    return m_textComponent;
            }
            set
            {
                m_textComponent = value;
            }
        }

        public string Text
        { // to get Text or TextExtension component, you must have to assign one at a time
            get
            {
                if (m_textComponent == null)
                    throw new NullReferenceException("Text component is not set in Inspector on " + name);
                else
                    return m_textComponent.text;
            }
            set
            {
                if (m_textComponent != null)
                    m_textComponent.text = value;
            }
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
                m_animator = value;
            }
        }
    }
}


