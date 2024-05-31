using System.Collections;
using System.Collections.Generic;
using Game.Interface;
using UnityEngine;

namespace Game
{
    public abstract class PoolMember : MonoBehaviour , IPoolMember
    {
        public IPooler Pooler { get; set; }
        public abstract bool IsInPool { get; set; }
    }
}