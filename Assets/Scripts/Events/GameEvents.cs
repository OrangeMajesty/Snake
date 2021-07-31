using UnityEngine;

namespace Events
{
    public class GameEvents : ScriptableObject
    {
        
        public delegate void GameEventsHandler();
        public event GameEventsHandler OnStartGame;
        public event GameEventsHandler OnFinishGame;
        
        public delegate void GameEventsAppleHandler(Color color);
        public event GameEventsAppleHandler OnEatenApple;

        private static GameEvents _instance;
        public static GameEvents Instance()
        {
            _instance ??= CreateInstance<GameEvents>();
            return _instance;
        }
        public void OnEatenAppleInvoke(Color color)
            => OnEatenApple?.Invoke(color);
        public void OnStartGameInvoke()
            => OnStartGame?.Invoke();
        public void OnFinishGameInvoke()
            => OnFinishGame?.Invoke();
    }
}