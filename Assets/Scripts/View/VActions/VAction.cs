namespace Zongband.View.VActions
{
    public abstract class VAction
    {
        public bool HasStarted { get; private set; } = false;
        public bool IsCompleted { get; private set; } = false;

        public void Process()
        {
            HasStarted = true;
            if (IsCompleted) return;
            IsCompleted = ProcessAndCheck();
        }

        protected abstract bool ProcessAndCheck();
    }
}
