using System.Collections;
using System.Collections.Generic;
using Game.Enum;
using UnityEngine;

namespace Game.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private UIPage MainPageUI,PlayUI;
        private UIPage currentPage;
        private GameManager gameManager;
        public GameManager GameManager {get => gameManager;}

        public void Initialize(GameManager gameManager)
        {
            this.gameManager = gameManager;
            MainPageUI.Initialize(this);
            PlayUI.Initialize(this);
        }

        public void ChangePage(EPage page)
        {
            UIPage newPage = null;
            switch (page)
            {
                case EPage.Main :
                    newPage = MainPageUI;
                    break;
                case EPage.Play :
                    newPage = PlayUI;
                    break;
            }

            if (newPage != null)
            {
                if (currentPage != null)
                {
                    currentPage.Hide();
                }
                newPage.Show();
                currentPage = newPage;
            }
        }
    }
}


