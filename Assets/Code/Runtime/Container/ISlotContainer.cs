using System;

namespace Code.Runtime.Container
{
    public interface ISlotContainer
    {
        public Package[] Contents { get; }
        public event Action<Package[]> OnContentsChanged;
        bool TryAdd( ref Package package );
        bool TryAddAt( int slot, ref Package arrival );

        bool TryRemove( int slot );

        bool TryRemove( Package removal );
    }
}