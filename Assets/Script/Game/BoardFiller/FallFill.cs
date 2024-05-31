using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Data;
using Game.Element;
using Game.Setting;
using UnityEngine;

namespace Game
{
    public class FallFill : BoardFiller
    {
        public FallFill(Board board, Pooler pooler, GameSetting gameSetting) : base(board, pooler, gameSetting)
        {
        }

        public override async UniTask Fill(SolveData solveData)
        {
            await base.Fill(solveData);
            await FallToEmpty();
            await FillEmptyTiles();
        }

        private async UniTask FallToEmpty()
        {
            for (int i = 0; i < _board.Size.y; i++)
            {
                for (int j = 0; j < _board.Size.x; j++)
                {
                    var currentCoordinator = new Vector2Int(j, i);
                    var tile = _board.GetTile(currentCoordinator);
                    var topTile = FindTop(currentCoordinator);

                    if (topTile != null && tile != null && tile.IsEmpty && !topTile.IsEmpty)
                    {
                        var piece = topTile.Piece;
                        piece.transform.SetParent(tile.transform);
                        topTile.Piece = null;
                        tile.Piece = piece;
                    }
                }
            }

            var animateTasks = new List<UniTask>();
            foreach (var tile in _board.GameBoard)
            {
                if (!tile.IsEmpty && tile.Piece.transform.localPosition != Vector3.zero)
                {
                    animateTasks.Add(tile.Piece.AnimateToTile());
                }
            }

            await UniTask.WhenAll(animateTasks);
        }

        private Tile FindTop(Vector2Int current)
        {
            if (current.y >= _board.Size.y)
                return null;

            var upperCoordinator = new Vector2Int(current.x, current.y + 1);
            var tile = _board.GetTile(upperCoordinator);
            if (tile != null && !tile.IsEmpty)
            {
                return tile;
            }
            else
            {
                return FindTop(upperCoordinator);
            }
        }
    }
}
