using System;

namespace ThesisProject.Internal.Interfaces
{
    internal interface IMenuUpdater
    {
        void AddUpdater(EventHandler updater);
        void Start();
        void Stop();
    }
}
