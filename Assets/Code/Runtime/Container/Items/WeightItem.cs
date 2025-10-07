using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Container.Items
{
    internal sealed class WeightItem : AbstractItem, IWeightItem
    {
        internal WeightItem( IItemData itemData, StackLimitType stackLimit, float weight ) : base( itemData, stackLimit )
        {
            this.weight = weight;
        }

        public float weight { get; }

        public override void Use() => throw new NotImplementedException();

        public override void Revert() => throw new NotImplementedException();

        public override List<Vector2Int> GetShape() => new List<Vector2Int>() { Vector2Int.zero };
    }

    public interface IWeightItem
    {
        float weight { get; }
    }
}