namespace Solo.BinaryTree.Constructor.Core
{
    public interface IAction<in TArgs>
    {
        void Process(TArgs arguments);
    }
}