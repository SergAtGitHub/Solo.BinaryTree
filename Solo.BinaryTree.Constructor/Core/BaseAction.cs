namespace Solo.BinaryTree.Constructor.Core
{
    public abstract class BaseAction<TArgs> : IAction<TArgs>
    {
        public void Process(TArgs arguments)
        {
            if (!CanExecute(arguments))
            {
                return;
            }

            Execute(arguments);
        }

        public abstract void Execute(TArgs args);

        public virtual bool CanExecute(TArgs args)
        {
            return true;
        }
    }
}