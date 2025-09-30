using System;
using System.Collections.Generic;

namespace Code.Runtime.Container
{
    public interface IInfiniteContainer
    {
        List<Package> Contents { get; }
        event Action<List<Package>> OnContentsChanged;
        void Add( Package arrival );
        bool TryRemove( Package package );
    }
}