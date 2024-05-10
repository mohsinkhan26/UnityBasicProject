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
    /// It extends Toggle component makes accesible with InputField component and animator if attached
    /// </summary>
    [AddComponentMenu("UI/Extensions/Toggle Extended"), RequireComponent(typeof(RectTransform))]
    public sealed class ToggleInputExtension : Toggle
    {
        [Header("Extended Properties")] [SerializeField]
        InputField
            m_inputFieldComponent;

        [SerializeField] Animator
            m_animator;

        public string Text
        {
            // to get InputField component, you must have to assign
            get
            {
                if (m_inputFieldComponent == null)
                    throw new NullReferenceException("InputField component is not set in Inspector on " + name);
                return m_inputFieldComponent.text;
            }
            set
            {
                if (m_inputFieldComponent == null)
                    throw new NullReferenceException("InputField component is not set in Inspector on " + name);
                m_inputFieldComponent.text = value;
            }
        }

        public bool InputFieldInteractable
        {
            // to get InputField component, you must have to assign
            get
            {
                if (m_inputFieldComponent == null)
                    throw new NullReferenceException("InputField component is not set in Inspector on " + name);
                return m_inputFieldComponent.interactable;
            }
            set
            {
                if (m_inputFieldComponent == null)
                    throw new NullReferenceException("InputField component is not set in Inspector on " + name);
                m_inputFieldComponent.interactable = value;
            }
        }

        public Animator AnimatorController
        {
            get
            {
                if (m_animator == null)
                    throw new NullReferenceException("Animator component is not set in Inspector on " + name);
                return m_animator;
            }
            set
            {
                if (m_animator == null)
                    throw new NullReferenceException("Animator component is not set in Inspector on " + name);
                m_animator = value;
            }
        }
    }
}