using System;
using Code.Runtime.Container.Items;

namespace Code.Runtime.Container
{
    [Serializable]
    public sealed class EnumSlotContainer<T> : SlotContainer where T : Enum
    {
        public EnumSlotContainer() : base( Enum.GetValues( typeof( T ) ).Length ) { }
        
        public bool TryAdd( ref Package arrival )
        {
            if( !arrival.hasValidItem || arrival.Item is not ISlotTypeItem<T> item )
                return false;
            return TryAddAt( ToInt( item.SlotType ), ref arrival );
        }
        public bool TryAdd( T slot, ref Package arrival ) => TryAdd( ToInt( slot ), ref arrival );
        public bool TryAdd( int slot, ref Package arrival )
        {
            if( !arrival.hasValidItem || arrival.Item is not ISlotTypeItem<T> item || ToInt( item.SlotType ) != slot )
                return false;
            return TryAddAt( slot, ref arrival );
        }

        public bool TryRemove( T slot ) => TryRemove( ToInt( slot ) );

        private int ToInt( T slot ) => Array.IndexOf( Enum.GetValues( typeof( T ) ), slot );
    }
}
