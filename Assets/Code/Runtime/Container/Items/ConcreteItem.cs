using System;
using UnityEngine;

namespace Code.Runtime.Container.Items
{
    [Serializable]
    public sealed class ConcreteItem : AbstractItem
    {
        public ConcreteItem( IItemData itemData ) : base( itemData )
        {
        }

        public override void Use() => throw new NotImplementedException();
    }
}