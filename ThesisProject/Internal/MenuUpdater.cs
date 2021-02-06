using System;
using ThesisProject.Internal.Interfaces;
using Microsoft.Extensions.Options;
using System.Windows.Threading;
using ThesisProject.Internal.Options;

namespace ThesisProject.Internal
{
    internal class MenuUpdater : IMenuUpdater
    {
        private readonly DispatcherTimer _timer;

        public MenuUpdater(IOptions<MenuUpdaterOptions> options)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(options.Value.RefreshPeriod);
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void AddUpdater(EventHandler updater)
        {
            _timer.Tick += updater;
        }
    }
}
