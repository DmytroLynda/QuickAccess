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

        public event EventHandler Update;

        public MenuUpdater(IOptions<MenuUpdaterOptions> options)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(options.Value.RefreshPeriod);
            _timer.Tick += Update;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
