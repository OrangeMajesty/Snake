using System;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    [RequireComponent(typeof(Text))]
    [RequireComponent(typeof(Animator))]
    public class CountDown : MonoBehaviour
    {
        public int num = 3;
        private Text _text;

        private void Start()
            =>  _text = GetComponent<Text>();

        public void Decrement()
        {
            if (num > 0)
            {
                _text.text = num--.ToString();
            }
            else
            {
                GameEvents.Instance().OnStartGameInvoke();
                GetComponent<Animator>().enabled = false;
                _text.enabled = false;
            }

        }
    }
}