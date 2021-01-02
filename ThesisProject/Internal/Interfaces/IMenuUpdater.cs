using System;

namespace ThesisProject.Internal.Interfaces
{
    internal interface IMenuUpdater
    {
        event EventHandler Update;
        void Start();
        void Stop();
    }
}
