using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Container.Items;
using UnityEngine;

namespace Code.Runtime.Container
{
    // INVENTORY TYPES:
    // 1. infinite -> add/stack/remove
    // 2. finite slots -> swap item at slot
    // 2.1 slot -> 1 slot per item (i.e. equipment -> enum based)
    // 2.2 grid -> multiple slots per item (i.e. backpack)
    // 3. finite weight

    [Serializable]
    public sealed class InfiniteContainer : IInfiniteContainer
    {
        [SerializeField] private List<Package> contents = new();

        public List<Package> Contents => contents;
        public event Action<List<Package>> OnContentsChanged;

        public void Add( Package arrival )
        {
            if( !arrival.IsValid )
                return;

            if( !Merge( ref arrival ) )
                Contents.Add( arrival );

            OnContentsChanged?.Invoke( Contents );
        }

        public bool TryRemove( Package package )
        {
            if( !Contents.Remove( package ) )
                return false;

            OnContentsChanged?.Invoke( Contents );
            return true;
        }

        private bool Merge( ref Package arrival )
        {
            for( var i = 0; i < Contents.Count && 0 < arrival.Amount; i++ )
            {
                if( Contents[i].Item.Equals( arrival.Item ) && 0 < Contents[i].SpaceLeft )
                {
                    var added = Contents[i].Increase( arrival.Amount );
                    _ = arrival.Reduce( added );
                }
            }

            return arrival.Amount == 0;
        }
    }
}