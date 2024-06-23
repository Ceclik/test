using UnityEngine;
using UnityEngine.EventSystems;

namespace CharacterScripts
{
    public class TouchHandler : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.LogError("On character clicked!");
        }
    }
}
