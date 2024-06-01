using System.Collections;
using System.Collections.Generic;
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
    public class Board
    {
        public Vector2Int Size => new Vector2Int(GameBoard.GetLength(0), GameBoard.GetLength(1));
        public Tile[,] GameBoard;
        public Transform Container;
        private IBoardFiller boardFiller;
        private Dictionary<Vector2Int, Tile> _tiles;
        private GameSetting gameSetting;

        public Tile GetTile(Vector2Int coordinate)
        {
            if (_tiles.TryGetValue(coordinate, out var tile))
                return tile;
            return null;
        }
        
        public Board(Tile[,] gameBoard,Transform container, GameSetting gameSetting)
        {
            this.gameSetting = gameSetting;
            GameBoard = gameBoard;
            Container = container;
            _tiles = new Dictionary<Vector2Int, Tile>();
            foreach (var tile in GameBoard)
            {
                _tiles.Add(tile.Coordinator,tile);
            }
        }

        public void InitializeBoard(IBoardFiller boardFiller)
        {
            this.boardFiller = boardFiller;
            Fill();
        }

        public void Fill()
        {
            boardFiller.Fill(null);
        }
        
        public async UniTask Fill(SolveData solveData)
        {
            await boardFiller.Fill(solveData);
        }
        
        public Tile HandleInput(Vector2 worldPosition)
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null && hit.collider.TryGetComponent<Tile>(out var result))
            {
                return result;
            }

            return null;
        }
        
        public SolveData Solve(Tile tile)
        {
            var solveData = new SolveData();
            if (!tile.IsEmpty)
            {
                solveData = tile.ActivePiece(this);
                if(solveData.Success)
                    TriggerAdditionalPiece(solveData);
            }

            return solveData;
        }
        
        private void TriggerAdditionalPiece(SolveData solveData, List<Tile> processedTiles = null)
        {
            if (processedTiles == null)
                processedTiles = new List<Tile>();

            var specialTiles = new List<Tile>();

            foreach (var tile in solveData.SolveTiles)
            {
                if (!tile.IsEmpty && tile.Piece.Solver is not BasicSolve && !processedTiles.Contains(tile))
                {
                    specialTiles.Add(tile);
                    processedTiles.Add(tile);
                }
            }

            foreach (var tile in specialTiles)
            {
                var additionalSolved =  tile.ActivePiece(this);
                foreach (var t in additionalSolved.SolveTiles)
                {
                    if (!solveData.SolveTiles.Contains(t))
                    {
                        solveData.SolveTiles.Add(t);
                    }
                }
            }
            
            if (specialTiles.Count > 0)
            {
                TriggerAdditionalPiece(solveData, processedTiles);
            }
        }

        public List<Tile> GetAdjacentOfTile(Tile tile)
        {
            List<Tile> result = new List<Tile>();
            foreach (var coordinator in tile.AdjacentCoordinator)
            {
                if(_tiles.TryGetValue(coordinator,out var adjacent))
                    result.Add(adjacent);
            }
            return result;
        }

        public List<Tile> GetTilesSameColor(EPieceType type)
        {
            List<Tile> result = new List<Tile>();
            foreach (var _tile in GameBoard)
            {
                if(!_tile.IsEmpty && type == _tile.PieceType)
                    result.Add(_tile);
            }

            return result;
        }
    }
}



