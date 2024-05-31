using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Data;
using Game.Element;
using Game.Enum;
using Game.Interface;
using Game.Setting;
using Script.Solver;
using UnityEngine;

namespace Game
{
    public abstract class BoardFiller : IBoardFiller
    {
        protected Board _board;
        protected Pooler _pooler;
        protected GameSetting _gameSetting;
        protected List<PieceData> _allPiece;

        protected BoardFiller(Board board, Pooler pooler, GameSetting gameSetting)
        {
            _board = board;
            _pooler = pooler;
            _gameSetting = gameSetting;
            _allPiece = gameSetting.PieceTypes;
        }

        Board IBoardFiller.Board
        {
            get => _board;
            set => _board = value;
        }

        PieceObject IBoardFiller.prefab { get; set; }

        public virtual async UniTask Fill(SolveData solveData)
        {
            await HandleSpecialPieces(solveData);
        }

        protected async UniTask HandleSpecialPieces(SolveData solveData)
        {
            var fillData = new Dictionary<Vector2Int, PieceData>();

            if (solveData != null && solveData.Solver is BasicSolve)
            {
                if (solveData.SolveTiles.Count >= PieceCondition.DiscoCondition)
                {
                    var baseDisco = _allPiece.First(w => w.Type == EPieceType.Disco);
                    var disco = new PieceData
                    {
                        Type = EPieceType.Disco,
                        Icon = baseDisco.Icon,
                        Color = solveData.PieceColor,
                    };
                    fillData.Add(solveData.PressTile.Coordinator, disco);
                }
                else if (solveData.SolveTiles.Count >= PieceCondition.BombCondition)
                {
                    var bomb = _allPiece.First(w => w.Type == EPieceType.Bomb);
                    fillData.Add(solveData.PressTile.Coordinator, bomb);
                }
            }

            foreach (var fill in fillData)
            {
                var tile = _board.GetTile(fill.Key);
                if (tile != null && tile.IsEmpty)
                {
                    var poolObject = _pooler.Get();
                    if (poolObject is PieceObject piece)
                    {
                        piece.Initialize(fill.Value, _pooler, _gameSetting);
                        tile.SetPiece(piece);
                        await piece.AnimateShow();
                    }
                }
            }
        }

        protected async UniTask FillEmptyTiles()
        {
            var animateTasks = new List<UniTask>();
            foreach (var tile in _board.GameBoard)
            {
                if (tile.IsEmpty)
                {
                    var poolObject = _pooler.Get();
                    if (poolObject is PieceObject piece)
                    {
                        var normalPieces = GetNormalPieces();
                        piece.Initialize(normalPieces[Random.Range(0, normalPieces.Count)], _pooler, _gameSetting);
                        tile.SetPiece(piece);
                        animateTasks.Add(piece.AnimateShow());
                    }
                }
            }

            await UniTask.WhenAll(animateTasks);
        }

        public List<PieceData> GetNormalPieces()
        {
            var result = new List<PieceData>();
            var specialPiece = new List<EPieceType> { EPieceType.Bomb, EPieceType.Disco };
            foreach (var piece in _allPiece)
            {
                if (!specialPiece.Contains(piece.Type))
                {
                    result.Add(piece);
                }
            }

            return result;
        }
    }
}