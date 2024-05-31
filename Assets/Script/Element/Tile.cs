using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Data;
using Game.Enum;
using Unity.Collections;
using UnityEngine;

namespace Game.Element
{
    public class Tile : MonoBehaviour
    {
        public Vector2Int Coordinator { private set; get; }
        public List<Vector2Int> adjacentTiles;
        private PieceObject piece;

        public bool IsEmpty => piece == null;
        public EPieceType PieceType => piece.Type;
        public PieceObject Piece
        {
            get => piece;
            set => piece = value;
        }

        public List<Vector2Int> AdjacentCoordinator => adjacentTiles;

        public void Initialize(Vector2Int coordinator,List<Vector2Int> adjacentTiles)
        {
            gameObject.name = "Tile " + coordinator.x + " , " + coordinator.y;
            Coordinator = coordinator;
            this.adjacentTiles = adjacentTiles;
            transform.position = new Vector3(coordinator.x, coordinator.y);
        }

        public void SetPiece(PieceObject piece)
        {
            this.piece = piece;
            piece.transform.SetParent(this.transform);
            piece.transform.localPosition = Vector3.zero;
        }

        public SolveData ActivePiece(Board board)
        {
            return piece.Active(board, this);
        }

        public async UniTask RemovePiece()
        {
            await piece.AnimateScaleDown();
            piece = null;
        }
    }
}


