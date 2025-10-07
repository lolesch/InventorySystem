using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Container.Items
{
    [Serializable]
    public sealed class TetrisItem : AbstractItem
    {
        public readonly Vector2Int[] Shape;
        //public RotationType rotation { get; }

        public TetrisItem( IItemData itemData, StackLimitType stackLimit, Vector2Int[] shape ) 
            : base( itemData, stackLimit ) => Shape = shape;

        public override void Use() => throw new NotImplementedException();

        public override void Revert() => throw new NotImplementedException();
        protected override List<int> GetPointers( int slot ) => throw new NotImplementedException();

        //public override List<Vector2Int> GetShape() => Shape.ToList();
    }
}