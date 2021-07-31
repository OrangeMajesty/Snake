using System;
using Events;
using UnityEngine;

namespace Controllers
{
    public class HeadSnakeMoveController : MonoBehaviour
    {
        public float speed = 2f;
        public RectTransform canvasRectTransform;
        private RectTransform _rectTransform;
        
        private float _currentSpeed;
        private Vector3 _vectorMove;
        private Camera _camera;
        

        private void Start()
        {
            _vectorMove = Vector3.right;
            GameEvents.Instance().OnStartGame += OnStartGame;

            _rectTransform = GetComponent<RectTransform>();
            _camera = GetComponentInParent<Canvas>().worldCamera;
        }
        private void OnStartGame()
            => _currentSpeed = speed;
        void Update()
        {
            // Mouse control
            if (Input.GetMouseButton(0))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, _camera, out var localpoint);
                localpoint = Rect.PointToNormalized(canvasRectTransform.rect, localpoint - _rectTransform.anchoredPosition);
                
                _vectorMove = new Vector3(localpoint.x >= 0.5f ? 1: -1, localpoint.y >= 0.5f ? 1: -1, 1f);
            }

            // Keyboard Control
            if (Input.GetKeyDown(KeyCode.DownArrow) && _vectorMove != Vector3.up)
                _vectorMove = Vector3.down;
            if (Input.GetKeyDown(KeyCode.UpArrow) && _vectorMove != Vector3.down)
                _vectorMove = Vector3.up;
            if (Input.GetKeyDown(KeyCode.LeftArrow) && _vectorMove != Vector3.right)
                _vectorMove = Vector3.left;
            if (Input.GetKeyDown(KeyCode.RightArrow) && _vectorMove != Vector3.left)
                _vectorMove = Vector3.right;
            
            var oldPos = transform.position;
            transform.position = oldPos + _vectorMove * (_currentSpeed * Time.deltaTime);
        }

        private void OnDestroy()
        {
            GameEvents.Instance().OnStartGame -= OnStartGame;
        }
    }
}