using UnityEngine;

namespace CharacterScripts
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] private Transform targetPoint;
        [SerializeField] private float speed;

        [SerializeField] private bool move;

        private bool _isOnPlace;

        private void Update()
        {
            if(move && !_isOnPlace)
                MoveCharacterUp();

            if (!_isOnPlace && transform.position == targetPoint.position)
                _isOnPlace = true;
        }

        private void MoveCharacterUp()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        }

        public void MoveCharacterDown()
        {
            
        }
    }
}
