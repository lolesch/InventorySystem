using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Container.Items;
using UnityEngine;

namespace Code.Runtime.Container
{
    [Serializable]
    public sealed class InfiniteContainer : IInfiniteContainer
    {
        [SerializeField] private List<Package> contents = new();

        public List<Package> Contents => contents;
        public event Action<List<Package>> OnContentsChanged;

        public void Add( Package arrival )
        {
            if( !arrival.hasValidItem )
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
                if( Contents[i].Item.Equals( arrival.Item ) )
                {
                    var added = Contents[i].Add( arrival.Amount );
                    _ = arrival.Remove( added );
                }
            }

            return arrival.Amount == 0;
        }
    }
}