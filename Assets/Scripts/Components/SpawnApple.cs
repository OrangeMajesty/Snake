using System;
using Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components
{
    [RequireComponent(typeof(RectTransform))]
    public class SpawnApple : MonoBehaviour
    {
        private GameEvents _gameEvents;
        private RectTransform _rectTransform;

        public GameObject applePrefab;
        public int maxCountApple = 5;

        private void Start()
        {
            _gameEvents = GameEvents.Instance();
            _gameEvents.OnEatenApple += OnEatenApple;

            _rectTransform = GetComponent<RectTransform>();

            for (var i = 0; i < maxCountApple; i++)
                SpawnNewApple();
        }

        private void OnEatenApple(Color color)
            => SpawnNewApple();
        private void SpawnNewApple()
        {
            Vector2 newPos = GetNewApplePosition();
            var apple = Instantiate(applePrefab, transform);

            apple.GetComponent<RectTransform>().anchoredPosition = newPos;
            apple.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        }

        private Vector2 GetNewApplePosition()
        {
            float offset = 80f;
            if (!_rectTransform)
                return Vector2.zero;
            
            var rect = _rectTransform.rect;
            var halfWidth = rect.width / 2;
            var halfHeight = rect.height / 2;
            float x = Random.Range(offset + halfWidth * -1, halfWidth - offset);
            float y = Random.Range(offset + halfHeight * -1, halfHeight - offset);

            return new Vector2(x, y);
        }

        private void OnDestroy()
        {
            GameEvents.Instance().OnEatenApple -= OnEatenApple;
        }
    }
}