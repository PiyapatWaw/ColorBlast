using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Data;
using Game.Element;
using UnityEngine;

namespace Game.Interface
{
    public interface IBoardFiller
    {
        protected Board Board { get; set; }
        protected PieceObject prefab { get; set; }
        public UniTask Fill(SolveData solveData);
        public List<PieceData> GetNormalPieces();
    }
}


