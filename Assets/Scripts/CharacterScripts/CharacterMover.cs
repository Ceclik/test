using UnityEngine;

namespace CharacterScripts
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] private Transform upperTargetPoint;
        [SerializeField] private Transform lowerTargetPoint;
        [SerializeField] private float speed;
        
        public bool MoveUp { get; set; }
        public bool MoveDown { get; set; }

        private bool _isOnPlace;

        private void Update()
        {
            if ((MoveUp || MoveDown) && _isOnPlace)
                _isOnPlace = false;
            
            if(MoveUp && !_isOnPlace)
                MoveCharacterUp();
            
            if(MoveDown && !_isOnPlace)
                MoveCharacterDown();

            if (!_isOnPlace)
            {
                if (MoveUp && transform.position == upperTargetPoint.position)
                {
                    MoveUp = false;
                    _isOnPlace = true;
                }

                if (MoveDown && transform.position == lowerTargetPoint.position)
                {
                    MoveDown = false;
                    _isOnPlace = true;
                }
            }
        }

        private void MoveCharacterUp()
        {
            transform.position = Vector3.MoveTowards(transform.position, upperTargetPoint.position, speed * Time.deltaTime);
        }

        public void MoveCharacterDown()
        {
            transform.position = Vector3.MoveTowards(transform.position, lowerTargetPoint.position, speed * Time.deltaTime);
        }
    }
}
