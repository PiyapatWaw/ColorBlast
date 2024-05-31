using System.Collections.Generic;
using Game.Data;
using Game.Element;

namespace Game.Interface
{
    public interface ISolver
    {
        public List<Tile> Solve(Board board,Tile tile,PieceData piece);
    }
}