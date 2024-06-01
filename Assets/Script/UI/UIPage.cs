using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public abstract class UIPage : MonoBehaviour
    {
        protected UIController _controller;

        public virtual void Initialize(UIController uiController)
        {
            _controller = uiController;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}


