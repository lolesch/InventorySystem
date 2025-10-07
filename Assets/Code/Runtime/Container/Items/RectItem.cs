using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Container.Items
{
    [Serializable]
    public abstract class RectItem : AbstractItem
    {
        public readonly Vector2Int Dimensions;
        protected RectItem( IItemData itemData, StackLimitType stackLimit ) : base( itemData, stackLimit ) {}

        protected override List<int> GetPointers( int slot ) 
        {
            var pointers = new List<int>();
            var position = new Vector2Int( slot % Dimensions.x, slot / Dimensions.x );
            var corner = position + Dimensions;

            for (var x = position.x; x < corner.x; x++)
                for (var y = position.y; y < corner.y; y++) 
                    pointers.Add( x + y * Dimensions.x );

            return pointers;
        }
    }
}