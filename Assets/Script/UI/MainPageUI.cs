using System.Collections.Generic;
using Game.Enum;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.UI
{
    public class MainPageUI : UIPage
    {
        [SerializeField] private TMP_Dropdown sizeSelector;
        [SerializeField] private TMP_Dropdown fillSelector;
        [SerializeField] private Button playButton;

        public override void Initialize(UIController uiController)
        {
            base.Initialize(uiController);
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(()=>uiController.GameManager.StartGame(sizeSelector.value,fillSelector.value));

            foreach (var size in uiController.GameManager.gameSetting.GridSize)
            {
                var text = $"{size.x} x {size.y}";
                sizeSelector.options.Add(new TMP_Dropdown.OptionData(text,null));
            }
            
            foreach (EFillType fillType in System.Enum.GetValues(typeof(EFillType)))
            {
                fillSelector.options.Add(new TMP_Dropdown.OptionData(fillType.ToString(), null));
            }

            sizeSelector.RefreshShownValue();
            fillSelector.RefreshShownValue();
        }
    }
}

