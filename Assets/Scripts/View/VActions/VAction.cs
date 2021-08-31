namespace Zongband.View.VActions
{
    public abstract class VAction
    {
        protected Context ctx;

        protected VAction(Context ctx)
        {
            this.ctx = ctx;
        }

        public bool IsCompleted { get; private set; } = false;

        public void Process()
        {
            if (IsCompleted) return;
            IsCompleted = ProcessAndCheck();
        }

        protected abstract bool ProcessAndCheck();
    }
}
