using System;

namespace OrlovMikhail.LJ.Grabber.Client.ViewModel
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T s)
        {
            Value = s;
        }

        public T Value { get; }
    }
}