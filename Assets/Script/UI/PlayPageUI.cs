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
        
        public override void Initialize(UIController gameManager)
        {
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(Replay);
        }
        
        private void Replay()
        {
            SceneManager.LoadScene("Game");
        }
    }
}


