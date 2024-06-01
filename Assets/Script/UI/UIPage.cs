using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public abstract class UIPage : MonoBehaviour
    {
        public abstract void Initialize(UIController uiController);

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


