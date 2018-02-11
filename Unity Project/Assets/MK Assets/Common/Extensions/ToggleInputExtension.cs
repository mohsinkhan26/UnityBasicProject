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
    /// It extends Toggle component makes accesible with InputField component and animator if attached
    /// </summary>
    [AddComponentMenu("UI/Extensions/Toggle Extended"), RequireComponent(typeof(RectTransform))]
    public class ToggleInputExtension : Toggle
    {
        [Header("Extended Properties")]
        [SerializeField]
        InputField
        m_inputFieldComponent;

        [SerializeField]
        Animator
            m_animator;

        public string Text
        { // to get InputField component, you must have to assign
            get
            {
                if (m_inputFieldComponent != null)
                    return m_inputFieldComponent.text;
                else
                    throw new NullReferenceException("InputField component is not set in Inspector on " + name);
            }
            set
            {
                if (m_inputFieldComponent != null)
                    m_inputFieldComponent.text = value;
            }
        }

        public bool InputFieldInteractable
        { // to get InputField component, you must have to assign
            get
            {
                if (m_inputFieldComponent != null)
                    return m_inputFieldComponent.interactable;
                else
                    throw new NullReferenceException("InputField component is not set in Inspector on " + name);
            }
            set
            {
                if (m_inputFieldComponent != null)
                    m_inputFieldComponent.interactable = value;
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


