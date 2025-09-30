using System;
using UnityEngine;

namespace Code.Runtime.Container.Items
{
    [Serializable]
    public abstract class AbstractItem : IItem
    {
        [field: SerializeField] public Sprite Icon { get; protected set; } = null;
        [field: SerializeField] private string _name { get; set; } = "Item";
        
        public readonly IItemData ItemData;

        protected AbstractItem( IItemData itemData, StackLimit stackLimit = StackLimit.Single )
        {
            ItemData = itemData;
            this.stackLimit = stackLimit;
            guid = Guid.NewGuid();
        }

        public Guid guid { get; }
        public StackLimit stackLimit { get; }
        public abstract void Use();

        public bool Equals( AbstractItem other )
        {
            if( other is null ) 
                return false;
            if( ReferenceEquals( this, other ) ) 
                return true;
            return GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode() => HashCode.Combine( ItemData, guid, stackLimit );
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
        StackLimit stackLimit { get; }
        void Use();
    }
}