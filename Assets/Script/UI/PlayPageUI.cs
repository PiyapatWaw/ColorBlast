using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayPageUI : UIPage
    {
        [SerializeField] private Button backButton;
        public override void Initialize(UIController uiController)
        {
            base.Initialize(uiController);
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(Replay);
        }
        
        private void Replay()
        {
            _controller.GameManager.StopGame();
            SceneManager.LoadScene("Game");
        }
    }
}


