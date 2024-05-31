namespace Game.Interface
{
    public interface IPoolMember
    {
        public IPooler Pooler { get; set; }
        public bool IsInPool { get; set; }
    }
}