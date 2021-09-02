namespace Zongband.View.VActions
{
    public abstract class ContextVAction : VAction
    {
        protected readonly Context ctx;

        public ContextVAction(Context ctx)
        {
            this.ctx = ctx;
        }
    }
}
