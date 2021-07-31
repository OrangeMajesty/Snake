using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class SceneController : MonoBehaviour
    {
        private void Start()
        {
            GameEvents.Instance().OnFinishGame += RestartScene;
        }

        private void RestartScene()
        {
            var index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }

        private void OnDestroy()
            => GameEvents.Instance().OnFinishGame -= RestartScene;
    }
}