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
    [AddComponentMenu("UI/Extensions/Slider Extended"), RequireComponent(typeof(RectTransform))]
    public class SliderExtension : Slider
    {
        [Header("Extended Properties")]
        [SerializeField]
        Text
            m_labelComponent;
        [SerializeField]
        Text
            m_valueComponent;

        [SerializeField]
        Animator
            m_animator;

        public Text LabelComponent
        {
            get
            {
                if (m_labelComponent == null)
                    throw new NullReferenceException("Label Text component is not set in Inspector on " + name);
                else
                    return m_labelComponent;
            }
            set
            {
                m_labelComponent = value;
            }
        }

        public string Label
        {
            get
            {
                if (m_labelComponent == null)
                    throw new NullReferenceException("Label Text component is not set in Inspector on " + name);
                else
                    return m_labelComponent.text;
            }
            set
            {
                if (m_labelComponent != null)
                    m_labelComponent.text = value;
            }
        }

        // TODO: register this function from inspector on this slider, as OnValueChanged
#if UNITY_EDITOR
        public
#endif
        void SetValueComponent()
        {
            m_valueComponent.text = m_Value.ToString("##0.##");
        }


        public string ValueInString
        {
            get
            {
                if (m_valueComponent == null)
                    throw new NullReferenceException("Value Text component is not set in Inspector on " + name);
                else
                    return m_Value.ToString("##0.##");
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


