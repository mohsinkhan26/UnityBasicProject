/* 
 * Author : Mohsin Khan
 * Website : http://mohsinkhan.000webhostapp.com/
*/
namespace UnityEngine.UI
{
    /// <summary>
    /// It extends Toggle component makes accesible of its Text component
    /// </summary>
    [AddComponentMenu("UI/Extensions/Text Extended"), RequireComponent(typeof(RectTransform))]
    public class TextExtension : Text
    {
        [Header("Extended Properties")]
        [SerializeField]
        string
            m_textId;

        public string textId
        {
            get
            {
                return m_textId;
            }
            set
            {
                m_textId = value;
            }
        }
    }
}


