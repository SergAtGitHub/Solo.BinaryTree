using System.Collections.Generic;

namespace Solo.BinaryTree.Constructor.Core
{
    public class QueryArguments<T>
    {
        public List<string> Messages { get; } = new List<string>();
        public T Result { get; set; }
    }
}