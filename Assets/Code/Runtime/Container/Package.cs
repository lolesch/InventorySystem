using System;
using Code.Runtime.Container.Items;
using UnityEngine;

namespace Code.Runtime.Container
{
    [Serializable]
    public struct Package : IPackage
    {
        [field: SerializeField] public AbstractItem Item { get; private set; }
        [field: SerializeField] public uint Amount { get;  private set; }

        public Package( AbstractItem item, uint amount = 1 )
        {
            Item = item;
            Amount = amount;
        }

        public bool hasValidItem => Item != null && 0 < Amount;
        public uint spaceLeft => Math.Clamp( (uint)Item.stackLimit - Amount, 0, (uint)Item.stackLimit );
        public bool hasSpace => 0 < spaceLeft;

        public uint Add( uint amount )
        {
            var increase = Math.Min(spaceLeft, amount);
            Amount += increase;

            return increase;
        }

        public uint Remove( uint amount )
        {
            var decrease = Math.Min(Amount, amount);
            Amount -= decrease;

            return decrease;
        }

        public bool Equals( Package other ) => GetHashCode() == other.GetHashCode();

        public override bool Equals( object obj ) => obj is Package other && Equals( other );

        public override int GetHashCode() => HashCode.Combine( Item, Amount );
    }
}