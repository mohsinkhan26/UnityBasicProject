namespace UnityEngine.UI
{
    /// <summary>
    /// It extends Toggle component makes accesible of its Text component
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

        public Text TextComponent
        {
            get
            {
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
                    return null;
            }
            set
            {
                if (m_textComponent != null)
                    m_textComponent.text = value;
                else if (m_textExtensionComponent != null)
                    m_textExtensionComponent.text = value;
            }
        }

        [SerializeField]
        Animator
            m_Animator;

        public Animator AnimatorController
        {
            get
            {
                return m_Animator;
            }
            set
            {
                m_Animator = value;
            }
        }
    }
}


