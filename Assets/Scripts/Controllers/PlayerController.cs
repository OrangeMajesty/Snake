using System;
using Events;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject partSnakePrefab;
        public RectTransform canvasRectTransform;
        public int startSize = 3;
        private Transform _lastPart;
        private void Start()
        {
            if (!partSnakePrefab) return;
        
            for (var i = 0; i < startSize; i++)
                CreatePartOfBody(Color.black);

            GameEvents.Instance().OnEatenApple += CreatePartOfBody;
        }

        private void CreatePartOfBody(Color color)
        {
            var part = Instantiate(partSnakePrefab, transform);

            if (color != Color.black)
                part.GetComponent<SpriteRenderer>().color = color;

            if (_lastPart is null)
                PrepareHead(ref part);
            else
                PrepareBody(ref part, _lastPart);
            
            _lastPart = part.transform;
        }

        private void PrepareHead(ref GameObject part)
        {
            part.AddComponent<HeadSnakeMoveController>().canvasRectTransform = canvasRectTransform;
            part.AddComponent<PlayerCollisionController>();
        }

        private void PrepareBody(ref GameObject part, Transform prevPartBody)
        {
            var partOfSnakeController = part.AddComponent<PartOfSnakeMoveController>();
            part.transform.position = prevPartBody.position;
            partOfSnakeController.SetPrevPart(_lastPart);
            partOfSnakeController.enabled = true;
        }

        private void OnDestroy()
        {
            GameEvents.Instance().OnEatenApple -= CreatePartOfBody;
        }
    }
}
