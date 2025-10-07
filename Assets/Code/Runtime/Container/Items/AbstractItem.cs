using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Container.Items
{
    [Serializable]
    public abstract class AbstractItem : IItem
    {
        [field: SerializeField] public Sprite Icon { get; protected set; } = null;
        [field: SerializeField] private string _name { get; set; } = "Item";
        
        public readonly IItemData ItemData;

        protected AbstractItem( IItemData itemData, StackLimitType stackLimit )
        {
            ItemData = itemData;
            this.stackLimit = stackLimit;
            guid = Guid.NewGuid();
        }

        public Guid guid { get; }
        public StackLimitType stackLimit { get; }

        public abstract void Use();
        public abstract void Revert();

        public bool Equals( AbstractItem other )
        {
            if( other is null ) 
                return false;
            if( ReferenceEquals( this, other ) ) 
                return true;
            return GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode() => HashCode.Combine( ItemData, guid, stackLimit );
        
        // review! this should return all required slots
        protected virtual List<int> GetPointers( int slot ) => new() { slot };
    }
    
    public interface IItemData
    {
        // DATA -> in ScriptableObject
        // name
        // description
        //[field: SerializeField] public Sprite icon { get; }
    }
    
    public interface IItem : IEquatable<AbstractItem> // : IItemData
    {
        Guid guid { get; }
        StackLimitType stackLimit { get; }
        void Use();
    }
}