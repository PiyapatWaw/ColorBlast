using System.Collections.Generic;
using Game;
using Game.Data;
using Game.Element;
using Game.Enum;
using Game.Interface;
using Game.Setting;

namespace Script.Solver
{
    public class DiscoSolve : ISolver
    {
        private readonly GameSetting _gameSetting;

        public DiscoSolve(GameSetting gameSetting)
        {
            _gameSetting = gameSetting;
        }

        public List<Tile> Solve(Board board, Tile tile, PieceData piece)
        {
            EPieceType target = EPieceType.Disco;

            foreach (var pieceData in _gameSetting.PieceTypes)
            {
                if (pieceData.Color == piece.Color)
                {
                    target = pieceData.Type;
                    break;
                }
            }

            List<Tile> affectedTiles = board.GetTilesSameColor(target);
            affectedTiles.Insert(0, tile);
            return affectedTiles;
        }
    }
}