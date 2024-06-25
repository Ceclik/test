using UnityEngine;

namespace CharacterScripts
{
    public class TouchHandler : MonoBehaviour
    {
        private void Update()
        {
            if (Application.isMobilePlatform)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        
                        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                        
                        if (hit.collider != null && hit.collider.gameObject == gameObject)
                        {
                            OnObjectTouched();
                        }
                    }
                }
                
            }
            else
            {
                if (Input.GetMouseButtonDown(0)) // ЛКМ
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        OnObjectTouched();
                    }
                }
            }
        }

        private void OnObjectTouched()
        {
            /*TODO ДИАЛОГ*/
            Debug.Log("Touch");
        }
    }
}
