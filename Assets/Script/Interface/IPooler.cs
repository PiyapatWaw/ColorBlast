using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface
{
    public interface IPooler
    {
        PoolMember asset { get; set; }
        Transform container { get; set; }
        List<PoolMember> Members { get; set; }
        
        PoolMember Get();
        void Return(PoolMember member);
        PoolMember Create();
    }
}