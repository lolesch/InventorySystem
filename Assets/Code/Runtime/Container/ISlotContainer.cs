using System;

namespace Code.Runtime.Container
{
    public interface ISlotContainer
    {
        Package[] Contents { get; }
        event Action<Package[]> OnContentsChanged;
        bool TryAdd( Package arrival, int slot, out Package other );
        bool TryRemove( int slot );
        bool TryRemove( Package removal );
    }
}