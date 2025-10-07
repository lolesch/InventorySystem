using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Container.Items
{
    [Serializable]
    public class SimpleItem : AbstractItem
    {
        public SimpleItem( IItemData itemData, StackLimitType stackLimit ) : base( itemData, stackLimit ) {}

        public override void Use() => throw new NotImplementedException();
        public override void Revert() => throw new NotImplementedException();
        public override List<Vector2Int> GetShape() => new List<Vector2Int>() { Vector2Int.zero };
    }
}