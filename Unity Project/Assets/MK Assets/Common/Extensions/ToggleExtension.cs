﻿/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/unbounded-eagle/ 
*/
using System;

namespace UnityEngine.UI
{
    /// <summary>
    /// It extends Toggle component makes accesible of its Text, TextExtension component and animator if attached
    /// </summary>
    [AddComponentMenu("UI/Extensions/Toggle Extended"), RequireComponent(typeof(RectTransform))]
    public class ToggleExtension : Toggle
    {
        [Header("Extended Properties")]
        [SerializeField]
        Text
            m_textComponent;
        [SerializeField]
        TextExtension
            m_textExtensionComponent;

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

        public TextExtension TextExtensionComponent
        {
            get
            {
                if (m_textExtensionComponent == null)
                    throw new NullReferenceException("TextExtension component is not set in Inspector on " + name);
                else
                    return m_textExtensionComponent;
            }
            set
            {
                m_textExtensionComponent = value;
            }
        }

        public string text
        { // to get Text or TextExtension component, you must have to assign one at a time
            get
            {
                if (m_textComponent != null)
                    return m_textComponent.text;
                else if (m_textExtensionComponent != null)
                    return m_textExtensionComponent.text;
                else
                    throw new NullReferenceException("TextExtension or Text component is not set in Inspector on " + name);
            }
            set
            {
                if (m_textComponent != null)
                    m_textComponent.text = value;
                else if (m_textExtensionComponent != null)
                    m_textExtensionComponent.text = value;
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


