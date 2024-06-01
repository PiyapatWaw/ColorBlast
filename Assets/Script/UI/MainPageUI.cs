using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.UI
{
    public class MainPageUI : UIPage
    {
        [SerializeField] private TMP_Dropdown sizeSelector;
        [SerializeField] private Button playButton;
        [SerializeField] private List<TMP_Dropdown.OptionData> options;

        public override void Initialize(UIController uiController)
        {
            base.Initialize(uiController);
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(()=>uiController.GameManager.StartGame(sizeSelector.value));

            foreach (var size in uiController.GameManager.gameSetting.GridSize)
            {
                var text = $"{size.x} x {size.y}";
                sizeSelector.options.Add(new TMP_Dropdown.OptionData(text,null));
            }
            
            sizeSelector.RefreshShownValue();
        }
    }
}

