namespace SilverlightWalkthrough.Async
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    public static class DispatcherHelper
    {
        public static Dispatcher UIDispatcher { get; private set; }

        public static void CheckBeginInvokeOnUI(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (UIDispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                UIDispatcher.BeginInvoke(action);
            }
        }

        public static void Initialize()
        {
            if (UIDispatcher != null)
            {
                return;
            }

            UIDispatcher = Deployment.Current.Dispatcher;
        }
    }
}