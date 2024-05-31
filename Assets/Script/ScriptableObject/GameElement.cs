using System.Collections;
using System.Collections.Generic;
using Game.Element;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Setting
{
    [CreateAssetMenu(fileName = "GameElement", menuName = "ScriptableObjects/GameElement", order = 2)]
    public class GameElement : ScriptableObject
    {
        public PieceObject pieceObjectPrefab;
        public Tile tilePrefab;
    }
}
