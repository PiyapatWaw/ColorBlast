using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Enum;
using Game.Setting;
using Game.UI;
using Game.Utility;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [field:SerializeField] public GameSetting gameSetting { get;private set; }
        [field:SerializeField] public GameElement gameElement { get;private set; }
        [SerializeField] private UIController uiController;
        private Board board;
        private Pooler pooler;
        private bool isPlay;

        private void Start()
        {
            pooler = new Pooler(gameElement.pieceObjectPrefab, new GameObject("Pooler").transform);
            uiController.Initialize(this);
            uiController.ChangePage(EPage.Main);
        }

        public void StartGame(int sizeIndex,int fillType)
        {
            uiController.ChangePage(EPage.Play);
            BoardFiller filler = null;
            board = BoardGenerator.GenerateBoard(gameSetting.GridSize[sizeIndex], gameElement.tilePrefab,gameSetting);
            switch (fillType)
            {
                case (int)EFillType.Instance :
                    filler = new InstanceFill(board, pooler, gameSetting);
                    break;
                case (int)EFillType.Fall :
                    filler =  new FallFill(board, pooler, gameSetting);
                    break;
            }
            board.InitializeBoard(filler);
            GameLoop().Forget();
        }

        private async UniTask GameLoop()
        {
            isPlay = true;
            while (isPlay)
            {
                var inputPosition = await ListenForInput();
                var pressTile = board.HandleInput(inputPosition);
                if (pressTile != null)
                {
                    var solveData = board.Solve(pressTile);
                    if (solveData.Success)
                    {
                        var removeTasks = new List<UniTask>();
                        foreach (var item in solveData.SolveTiles)
                        {
                            removeTasks.Add(item.RemovePiece());
                        }

                        await UniTask.WhenAll(removeTasks);
                    }

                    await UniTask.WaitForSeconds(0.15f);
                    await board.Fill(solveData);
                }
            }
        }

        private async UniTask<Vector2> ListenForInput()
        {
            while (isPlay)
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                return mousePosition;
            }

            return Vector2.zero;
        }

        public void StopGame()
        {
            isPlay = false;
        }
    }
}