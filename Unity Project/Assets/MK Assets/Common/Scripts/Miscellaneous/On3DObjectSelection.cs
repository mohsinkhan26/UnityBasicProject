using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MK.Common.Miscellaneous
{ // TODO: don't forget to add PhysicsRaycaster component on Camera and Collider component on the object
    [RequireComponent(typeof(BoxCollider))]
    public class On3DObjectSelection : MonoBehaviour, IPointerClickHandler
    { // handy script in nested complex structures
        [SerializeField]
        [Tooltip("Register actions to perform on this GameObject")]
        UnityEvent OnSelected;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickCount > 0)
            {
                ObjectSelected();
                OnSelected.Invoke();
            }
        }

        protected virtual void ObjectSelected()
        {
        }
    }
}
