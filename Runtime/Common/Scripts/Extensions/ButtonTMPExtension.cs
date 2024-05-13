/*
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 */

using System;
using TMPro;

namespace UnityEngine.UI
{
    /// <summary>
    /// It extends Button component makes accesible of its Text component and animator if attached
    /// </summary>
    [AddComponentMenu("UI/Extensions/Button TMP Extended"), RequireComponent(typeof(RectTransform))]
    public sealed class ButtonTMPExtension : Button
    {
        [Header("Extended Properties")] [SerializeField]
        TMP_Text m_textTMP;

        [SerializeField] Animator m_animator;

        public TMP_Text TextTMPComponent
        {
            get
            {
                if (m_textTMP == null)
                    throw new NullReferenceException("TextTMP component is not set in Inspector on " + name);
                return m_textTMP;
            }
            set
            {
                if (m_textTMP == null)
                    throw new NullReferenceException("TextTMP component is not set in Inspector on " + name);
                m_textTMP = value;
            }
        }

        public string Text
        {
            // to get TextTMP component, you must have to assign one at a time
            get
            {
                if (m_textTMP != null)
                    return m_textTMP.text;
                throw new NullReferenceException("TextTMP component is not set in Inspector on " + name);
            }
            set
            {
                if (m_textTMP != null)
                    m_textTMP.text = value;
                else
                    throw new NullReferenceException("TextTMP component is not set in Inspector on " + name);
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