using System;
using UnityEngine;

namespace Code.Runtime.Container
{
    [Serializable]
    public abstract class SlotContainer : ISlotContainer
    {
        [SerializeField] private Package[] contents;
        public Package[] Contents => contents;

        public event Action<Package[]> OnContentsChanged;

        public SlotContainer( int size ) => contents = new Package[size];

        public bool TryAdd( Package arrival, int slot, out Package other )
        {
            other = arrival;
            
            if( !arrival.IsValid )
                return false;

            other = Contents[slot];

            if( Contents[slot].IsValid && TryMerge( ref arrival, slot ) )
                other = arrival;
            else
                Contents[slot] = arrival;
            
            OnContentsChanged?.Invoke( Contents );
            return true;
        }

        public bool TryRemove( int slot )
        {
            if( !Contents[slot].IsValid )
                return false;

            Contents[slot] = new Package();
            OnContentsChanged?.Invoke( Contents );
            return true;
        }

        public bool TryRemove( Package removal )
        {
            var slot = Array.FindIndex( Contents, p => p.Item.Equals( removal.Item ) );
            if( slot < 0 )
                return false;

            if( removal.Amount > Contents[slot].Amount )
                return false;
            
            _ = Contents[slot].Reduce( removal.Amount );
            OnContentsChanged?.Invoke( Contents );
            return true;
        }

        private bool TryMerge( ref Package arrival, int slot )
        {
            if( !Contents[slot].Item.Equals( arrival.Item ) ) 
                return false;
            
            var added = Contents[slot].Increase( arrival.Amount );
            _ = arrival.Reduce( added );
            return true;
        }
    }
}