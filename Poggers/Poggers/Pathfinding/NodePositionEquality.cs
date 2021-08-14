using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Poggers.Pathfinding
{
    public class NodePositionEquality : IEqualityComparer<Node>
    {
        public bool Equals([AllowNull] Node x, [AllowNull] Node y)
        {
            return x?.Position.Equals(y?.Position) ?? false;
        }

        public int GetHashCode([DisallowNull] Node obj)
        {
            return obj.Position.GetHashCode();
        }
    }
}
