using UnityEngine;

namespace CharacterScripts
{
    public class CharacterMover : MonoBehaviour
    {
        public Transform UpperTargetPoint { get; set; }
        public Transform LowerTargetPoint { get; set; }
        [SerializeField] private float speed;
        
        public int PointIndex { get; set; }
        
        public bool MoveUp { get; set; }
        public bool MoveDown { get; set; }
        public bool MoveToPoint { get; set; }

        private bool _isOnPlace;
        private bool _isUp;
        private bool _isDown = true;

        private void Update()
        {
            if ((MoveUp || MoveDown || MoveToPoint) && _isOnPlace)
                _isOnPlace = false;

            if (MoveToPoint && !_isOnPlace)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    _isDown ? LowerTargetPoint.position : UpperTargetPoint.position, speed * Time.deltaTime);
            }

            if(MoveUp && !_isOnPlace && !MoveToPoint)
                MoveCharacterUp();
            
            if(MoveDown && !_isOnPlace && !MoveToPoint)
                MoveCharacterDown();

            if (!_isOnPlace)
            {
                if (MoveUp && transform.position == UpperTargetPoint.position)
                {
                    //Debug.Log($"is up\ncharacrer: {gameObject.name}");
                    MoveUp = false;
                    
                    _isUp = true;
                    _isDown = false;
                    
                    _isOnPlace = true;
                }

                if (MoveDown && transform.position == LowerTargetPoint.position)
                {
                    //Debug.Log($"is down\ncharacrer: {gameObject.name}");
                    MoveDown = false;
                    
                    _isUp = false;
                    _isDown = true;
                    
                    _isOnPlace = true;
                }

                if (MoveToPoint)
                {
                    if (_isDown && transform.position == LowerTargetPoint.position)
                    {
                        _isOnPlace = true;
                        MoveToPoint = false;
                    }

                    if (_isUp && transform.position == UpperTargetPoint.position)
                    {
                        _isOnPlace = true;
                        MoveToPoint = false;
                    }
                }
            }
        }
        
        private void MoveCharacterUp()
        {
            transform.position = Vector3.MoveTowards(transform.position, UpperTargetPoint.position, speed * Time.deltaTime);
        }

        private void MoveCharacterDown()
        {
            transform.position = Vector3.MoveTowards(transform.position, LowerTargetPoint.position, speed * Time.deltaTime);
        }
    }
}
