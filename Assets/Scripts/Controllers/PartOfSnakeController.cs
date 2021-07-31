using UnityEngine;

namespace Controllers
{
    public class PartOfSnakeMoveController : MonoBehaviour
    {
        private Transform _prevPart;
        private float _speed = 0.18f;
    
        public void SetPrevPart(Transform prevPart)
            => _prevPart = prevPart;
    
        private void FixedUpdate()
        {
            if (_prevPart)
                transform.position = Vector3.Lerp(transform.position, _prevPart.position, _speed);
        }
    }
}
