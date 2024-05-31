using System.Collections.Generic;
using Game;
using Game.Data;
using Game.Element;
using Game.Interface;
using UnityEngine;

namespace Script.Solver
{
    public class BombSolve: ISolver
    {
        public List<Tile> Solve(Board board, Tile tile, PieceData piece)
        {
            List<Tile> affectedTiles =  board.GetAdjacentOfTile(tile);
            affectedTiles.Insert(0,tile);

            Vector2Int[] additionArea = new[]
                { new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1) };

            foreach (var area in additionArea)
            {
                var newCoordinator = tile.Coordinator + area;
                var additionTile = board.GetTile(newCoordinator);
                if (additionTile != null)
                {
                    affectedTiles.Add(additionTile);
                }
            }
            
            return affectedTiles;
        }
    }
}