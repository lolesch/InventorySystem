using System;
using Code.Runtime.Container.Items;
using UnityEngine;

namespace Code.Runtime.Container
{
    [Serializable]
    public abstract class SlotContainer : ISlotContainer
    {
        [field: SerializeField] public Package[] Contents { get; private set; }

        public event Action<Package[]> OnContentsChanged;

        protected SlotContainer( int capacity ) => Contents = new Package[capacity];
        
        public bool TryAdd( ref Package package )
        {
            if( !package.hasValidItem )
                return false;

            return TryMerge( ref package ) || TryAddToEmpty( package );
        }
        
        public bool TryAddAt( int slot, ref Package arrival )
        {
            if( !arrival.hasValidItem || !IsValidSlot( slot ) )
                return false;

            if( TryCombineAt( slot, ref arrival ) )
                return true;

            SwapAt (slot, ref arrival );
            return true;
        }

        public bool TryRemove( int slot, out Package removed )
        {
            if( IsEmpty( slot ) )
            {
                removed = new Package();
                return false;
            }

            removed = Contents[slot];
            Contents[slot] = new Package();
            
            OnContentsChanged?.Invoke( Contents );
            return true;
        }

        public bool TryRemove( Package removal )
        {
            if( !removal.hasValidItem )
                return false;

            // TODO: TrySplitAt() -> see TryCombineAt() as reference
            // and iterate over all slots to remove from multiple stacks if necessary
            
            var slot = Array.FindIndex( Contents, p => p.Item.Equals( removal.Item ) );
            if( slot < 0 )
                return false;

            if( removal.Amount > Contents[slot].Amount )
                return false;
            
            _ = Contents[slot].Remove( removal.Amount );
            OnContentsChanged?.Invoke( Contents );
            return true;
        }

        //public abstract void UseItemAt( int slot );

        private void SwapAt( int slot, ref Package arrival )
        {
            var previous  = Contents[slot];
            Contents[slot] = arrival;
            arrival = previous;
                
            OnContentsChanged?.Invoke( Contents );
        }
        
        private bool TryMerge( ref Package arrival )
        {
            if( arrival.Item.stackLimit <= StackLimitType.Single)
                return false;
            
            for( var slot = 0; slot < Contents.Length; slot++ )
            {
                if( !TryCombineAt( slot, ref arrival ) )
                    continue;

                if( 0 < arrival.Amount ) 
                    continue;
                
                arrival = new Package();
                OnContentsChanged?.Invoke( Contents );
                return true;
            }
            return false;
        }
        
        private bool TryCombineAt( int slot, ref Package arrival )
        {
            if( IsEmpty( slot ) || !Contents[slot].Item.Equals( arrival.Item ) || !Contents[slot].hasSpace )
                return false;
            
            var added = Contents[slot].Add( arrival.Amount );
            _ = arrival.Remove( added );
            
            OnContentsChanged?.Invoke( Contents );
            return true;
        }
        
        private bool TryAddToEmpty( Package arrival )
        {
            for( var slot = 0; slot < Contents.Length; slot++ )
            {
                if( !IsEmpty( slot ) ) 
                    continue;
         
                if( TryAddAt( slot, ref arrival ) )
                    return true;
            }
            return false;
        }
        
        private bool IsEmpty( int slot ) => IsValidSlot( slot) && !Contents[slot].hasValidItem;
        private bool IsValidSlot( int slot ) => 0 <= slot && slot < Contents.Length;}
}