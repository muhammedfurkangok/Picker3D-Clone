using System;
using System.Collections.Generic;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public struct LevelData
    {
        public List<PoolData> Pools;

        public LevelData(List<PoolData> pools) //constructor örneği !
        {
            Pools = pools;
        }
    }
}