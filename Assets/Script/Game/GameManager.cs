using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Setting;
using Game.Utility;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameSetting gameSetting;
        [SerializeField] private GameElement gameElement;
        private Board board;
        private Pooler pooler;
        private bool isPlay;

        private void Start()
        {
            pooler = new Pooler(gameElement.pieceObjectPrefab, new GameObject("Pooler").transform);
            board = BoardGenerator.GenerateBoard(gameSetting.GridSize[0], gameElement.tilePrefab,gameSetting);
            board.InitializeBoard(new FallFill(board, pooler, gameSetting));
            Play().Forget();
        }

        private async UniTask Play()
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
    }
}