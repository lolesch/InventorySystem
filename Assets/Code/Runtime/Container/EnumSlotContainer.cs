using System;

namespace Code.Runtime.Container
{
    [Serializable]
    public sealed class EnumSlotContainer<T> : SlotContainer where T : Enum
    {
        public EnumSlotContainer() : base( Enum.GetValues( typeof( T ) ).Length ) { }
        
        public bool TryAdd( Package arrival, T slot, out Package previous ) => TryAdd( arrival, ToInt( slot ), out previous );
        public bool TryRemove( T slot ) => TryRemove( ToInt( slot ) );

        private int ToInt( T slot ) => Array.IndexOf( Enum.GetValues( typeof( T ) ), slot );
    }
}