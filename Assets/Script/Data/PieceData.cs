using System.Collections.Generic;
using Game.Element;
using Game.Enum;
using Game.Interface;
using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    public struct PieceData
    {
        public EPieceType Type;
        public Color Color;
        public Sprite Icon;
        
    }
}


