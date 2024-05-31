using System.Collections.Generic;
using Game.Element;
using Game.Interface;
using UnityEngine;

namespace Game.Data
{
    public class SolveData
    {
        public SolveData(Tile pressTile, Color pieceColor, List<Tile> solveTiles, ISolver solver)
        {
            Success = true;
            PressTile = pressTile;
            SolveTiles = solveTiles;
            Solver = solver;
            PieceColor = pieceColor;
        }

        public SolveData()
        {
            Success = false;
        }

        public bool Success;
        public Tile PressTile;
        public Color PieceColor;
        public List<Tile> SolveTiles;
        public ISolver Solver;
    }
}