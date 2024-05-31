using System.Collections.Generic;
using System.Linq;
using Game;
using Game.Data;
using Game.Element;
using Game.Enum;
using Game.Interface;
using UnityEngine;

namespace Script.Solver
{
    public class BasicSolve : ISolver
    {
        private Dictionary<Vector2Int, Tile> affectedTiles;
        public List<Tile> Solve(Board board, Tile tile, PieceData piece)
        {
            affectedTiles = new Dictionary<Vector2Int, Tile>();

            GetSameTypeFromAdjacent(board,tile,piece.Type);
            
            if(affectedTiles.Count < PieceCondition.SolveCondition)
                affectedTiles.Clear();
            
            return affectedTiles.Values.ToList();
        }

        private void GetSameTypeFromAdjacent(Board board, Tile startTile, EPieceType type)
        {
            Stack<Tile> stack = new Stack<Tile>();
            stack.Push(startTile);

            while (stack.Count > 0)
            {
                Tile currentTile = stack.Pop();

                if (affectedTiles.TryAdd(currentTile.Coordinator, currentTile))
                {
                    var adjacentTiles = board.GetAdjacentOfTile(currentTile);
                    foreach (var adjacentTile in adjacentTiles)
                    {
                        if (!adjacentTile.IsEmpty && adjacentTile.PieceType == type &&
                            !affectedTiles.ContainsKey(adjacentTile.Coordinator))
                        {
                            stack.Push(adjacentTile);
                        }
                    }
                }
            }
        }
    }
}