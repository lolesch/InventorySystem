using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Container.Items;
using UnityEngine;

namespace Code.Runtime.Container
{
    [Serializable]
    public sealed class GridContainer : SlotContainer
    {
        public GridContainer( Vector2Int dimensions ) : base( dimensions.x * dimensions.y ) => Dimensions = dimensions;

        public readonly Vector2Int Dimensions;

        private Dictionary<Vector2Int, Vector2Int> _gridPointer = new();

        protected override bool CanAddAt( int slot, Package arrival )
        {
            // all pointers are within bounds of the grid
            // get position 
            // get all pointers for the incoming package
            
            // max one item overlapping
            
            var canAdd = base.CanAddAt( slot, arrival );
            return canAdd;
        }

        protected override void SwapAt( int slot, ref Package arrival )
        {
            // TODO: remove all pointers associated with the current package in the slot

            // and add all pointers associated with the incoming package
            base.SwapAt( slot, ref arrival );
        }
        
        private int ToSlot( Vector2Int position ) => position.x + position.y * Dimensions.x;
        private Vector2Int ToPosition( int slot ) => new( slot % Dimensions.x, slot / Dimensions.x );
        
        public List<Vector2Int> GetOverlappingItems( Vector2Int position, RectItem item )
        {
            var itemPositions = new List<Vector2Int>();
            
            //var pointers = GetRequiredPointers( position, item );
            var pointers = item.GetPointers( ToSlot( position ) );
            
            foreach (var p in pointers)
                if( _gridPointer.TryGetValue(p, out var itemPosition ) )
                    itemPositions.Add( itemPosition );

            return itemPositions.Distinct().ToList();
        }
    }

    [Serializable]
    public sealed class TetrisContainer : SlotContainer
    {
        public TetrisContainer( Vector2Int dimensions ) : base( dimensions.x * dimensions.y ) => Dimensions = dimensions;

        public readonly Vector2Int Dimensions;
    }
}