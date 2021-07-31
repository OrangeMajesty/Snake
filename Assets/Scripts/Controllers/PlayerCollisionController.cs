using Events;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerCollisionController : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Apple"))
            {
                var color = other.transform.GetComponent<SpriteRenderer>().color;
                GameEvents.Instance().OnEatenAppleInvoke(color);
                Destroy(other.transform.gameObject);
            }

            if (other.transform.CompareTag("Border"))
                GameEvents.Instance().OnFinishGameInvoke();
        }
    }
}