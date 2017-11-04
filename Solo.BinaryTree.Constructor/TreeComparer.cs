using System;
using System.Collections.Generic;

namespace Solo.BinaryTree.Constructor
{
    public class TreeComparer : IEqualityComparer<Tree>
    {
        public static readonly TreeComparer Instance = new TreeComparer();

        public bool Equals(Tree x, Tree y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Data == y.Data && Equals(x.Left, y.Left) && Equals(x.Right, y.Right);
        }

        public int GetHashCode(Tree obj)
        {
            throw new NotImplementedException();
        }
    }
}