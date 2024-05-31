using Cysharp.Threading.Tasks;
using Game.Data;
using Game.Setting;

namespace Game
{
    public class InstanceFill : BoardFiller
    {
        public InstanceFill(Board board, Pooler pooler, GameSetting gameSetting) : base(board, pooler, gameSetting)
        {
        }

        public override async UniTask Fill(SolveData solveData)
        {
            await base.Fill(solveData);
            await FillEmptyTiles();
        }
    }
}