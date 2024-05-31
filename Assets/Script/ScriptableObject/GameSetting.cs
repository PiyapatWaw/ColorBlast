using System.Collections;
using System.Collections.Generic;
using Game.Data;
using Game.Enum;
using UnityEngine;

namespace Game.Setting
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
    public class GameSetting : ScriptableObject
    {
        public List<Vector2Int> GridSize;
        public List<PieceData> PieceTypes;
    }
}


