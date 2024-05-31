using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Data;
using Game.Enum;
using Game.Interface;
using Game.Setting;
using Script.Solver;
using UnityEngine;

namespace Game.Element
{
    public class PieceObject : PoolMember
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private PieceData data;
        private ISolver solver;

        public EPieceType Type => data.Type;
        public ISolver Solver => solver;

        public void Initialize(PieceData data, IPooler pool, GameSetting gameSetting)
        {
            this.data = data;
            _spriteRenderer.sprite = data.Icon;
            _spriteRenderer.color = data.Color;
            Pooler = pool;
            switch (data.Type)
            {
                case EPieceType.Bomb:
                    solver = new BombSolve();
                    break;
                case EPieceType.Disco:
                    solver = new DiscoSolve(gameSetting);
                    break;
                default:
                    solver = new BasicSolve();
                    break;
            }
        }

        public SolveData Active(Board board, Tile tile)
        {
            SolveData result;

            var solveTiles = solver.Solve(board, tile, data);

            if (solveTiles.Count > 0)
            {
                result = new SolveData(tile, data.Color, solveTiles, solver);
            }
            else
            {
                result = new SolveData();
            }

            return result;
        }

        public async UniTask AnimateScaleDown(float duration = 0.25f)
        {
            await _spriteRenderer.transform.DOScale(Vector3.zero, duration).ToUniTask();
            Pooler.Return(this);
        }

        public async UniTask AnimateShow(float duration = 0.25f)
        {
            await _spriteRenderer.transform.DOScale(Vector3.one, duration).ToUniTask();
        }

        public async UniTask AnimateToTile(float durationPerUnit = 0.15f)
        {
            float distance = Vector3.Distance(transform.position, transform.parent.position);
            float duration = distance * durationPerUnit;

            await transform.DOLocalMove(Vector3.zero, duration).ToUniTask();
        }

        public override bool IsInPool { get; set; }
    }
}
