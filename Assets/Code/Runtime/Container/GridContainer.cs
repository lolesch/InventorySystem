using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Container.Items;
using UnityEngine;

namespace Code.Runtime.Container
{
    [Serializable]
    public class GridContainer : IGridContainer
    {
        [field: SerializeField] public Package[] Contents { get; private set; }

        public event Action<Package[]> OnContentsChanged;
        public GridContainer( Vector2Int dimensions )
        {
            Dimensions = dimensions;
            Contents = new Package[dimensions.x * dimensions.y];
        }

        public readonly Vector2Int Dimensions;

        private Dictionary<Vector2Int, int> _gridPointer = new();
        
        public bool TryAdd( ref Package package )
        {
            if( !package.hasValidItem )
                return false;

            return TryMerge( ref package ) || TryAddToEmpty( package );
        }
        public bool TryAddAt( int slot, ref Package arrival )
        {
            if( !CanAddAt( slot , arrival, out var other ) )
                return false;

            if( other.Any() )
            {
                if( !TryCombineAt( other[0], ref arrival ) )
                    SwapAt( slot, ref arrival, other[0] );
                
                return true;
            }
            
            SwapAt (slot, ref arrival, slot );
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
            var pointers = removed.Item.GetPointers( ToPosition( slot ), RotationType.Deg0 );
            foreach( var pointer in pointers )
                _gridPointer.Remove( pointer );
            
            OnContentsChanged?.Invoke( Contents );
            return true;
        }
        public bool TryRemove( Package remaining )
        {
            if( !remaining.hasValidItem )
                return false;
            
            if( TryUnmerge( ref remaining ) )
                return true;
            
            for( var slot = Contents.Length; slot --> 0; )
            {
                if( IsEmpty( slot ) ) 
                    continue;
         
                if( TryRemove( slot, out remaining ) )
                    return true;
            }
            
            return false;
        }
        
        private bool TryUnmerge( ref Package remaining )
        {
            if( remaining.Item.stackLimit <= StackLimitType.Single)
                return false;
            
            for( var slot = Contents.Length; slot --> 0; )
            {
                if( !TrySplitAt( slot, ref remaining ) )
                    continue;

                if( 0 < remaining.Amount ) 
                    continue;
                
                remaining = new Package();
                //OnContentsChanged?.Invoke( Contents ); // already invoked in TrySplitAt
                return true;
            }
            return false;
        }
        private bool TrySplitAt( int slot, ref Package remaining )
        {
            if( IsEmpty( slot ) || !Contents[slot].Item.Equals( remaining.Item ) )
                return false;
            
            var removed = Contents[slot].Remove( remaining.Amount );
            _ = remaining.Remove( removed );
            
            OnContentsChanged?.Invoke( Contents );
            return true;
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
                //OnContentsChanged?.Invoke( Contents ); // already invoked in TryCombineAt
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
        private void SwapAt( int slot, ref Package arrival, int other )
        {
            _ = TryRemove( other, out var removed );
            
            Contents[slot] = arrival;
            var pointers = arrival.Item.GetPointers( ToPosition( slot ), RotationType.Deg0 );
            foreach( var pointer in pointers )
                _gridPointer.Add( pointer, slot );
            arrival = removed;
                
            OnContentsChanged?.Invoke( Contents );
        }
        private bool CanAddAt( int slot, Package arrival, out List<int> other )
        {
            other = new List<int>();
            
            if( !arrival.hasValidItem )
                return false;
            
            other = GetOverlappingItems( ToPosition( slot ), arrival );
            
            return other.Count <= 1;
        }
        private int ToSlot( Vector2Int position ) => position.x + position.y * Dimensions.x;
        private Vector2Int ToPosition( int slot ) => new( slot % Dimensions.x, slot / Dimensions.x );
        private List<int> GetOverlappingItems( Vector2Int position, Package package )
        {
            var slots = new List<int>();
            
            var pointers = package.Item.GetPointers( position, RotationType.Deg0 );
            
            foreach( var pointer in pointers )
            {
                if( !IsValidSlot( ToSlot( pointer ) ) )
                    continue;
                
                if( !IsEmpty( pointer, out var otherSlot ) )
                    slots.Add( otherSlot );
            }

            return slots.Distinct().ToList();
        }
        private bool IsEmpty( int slot ) => IsValidSlot( slot) && !Contents[slot].hasValidItem;
        private bool IsEmpty( Vector2Int pointer, out int slot ) => !_gridPointer.TryGetValue( pointer, out slot );
        private bool IsValidSlot( int slot ) => 0 <= slot && slot < Contents.Length;
    
        // TODO: void RotateItemAt( int slot, RotationType rotation )
        // update pointers accordingly
    }

    [Serializable]
    public sealed class TetrisContainer : GridContainer
    {
        public TetrisContainer( Vector2Int dimensions ) : base( dimensions ) {}
    }
}