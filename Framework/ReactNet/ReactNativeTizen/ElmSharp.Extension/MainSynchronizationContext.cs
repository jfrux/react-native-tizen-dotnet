using System;
using System.Threading;

namespace ReactNativeTizen.ElmSharp.Extension
{
    public static class MainSynchronizationContext
    {
        private static SynchronizationContext _context;
        private static Action _quitAction;

        public static void Initialize(SynchronizationContext synchronizationContext, Action quitAction = null)
        {
            _context = synchronizationContext;
            _quitAction = quitAction;
        }

        public static void Send(Action action)
        {
            MustBeInitialized();
            _context.Send(_ => { action(); }, null);
        }

        public static void Post(Action action)
        {
            MustBeInitialized();
            _context.Post(_ => { action(); }, null);
        }

        public static void Quit()
        {
            _quitAction?.Invoke();
        }

        public static bool IsMainThread => _context != null && SynchronizationContext.Current == _context;

        private static void MustBeInitialized()
        {
            if (_context == null)
                throw new InvalidOperationException("MainSynchronizationContext is not initialized");
        }
    }
}