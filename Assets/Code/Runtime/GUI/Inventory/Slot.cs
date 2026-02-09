using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Runtime.GUI.Inventory
{
    public sealed class Slot : VisualElement
    {
        public Image Icon;
        public Label StackLabel;
        //public SerializableGuid ItemId { get; private set; } = SerializableGuid.Empty;
        
        public event Action<Vector2, Slot> OnStartDrag = delegate { };
        
        public int Index => parent.IndexOf( this );
        
        public Slot()
        {
            Icon = this.CreateChild<Image>("slotIcon");
            _ = this.CreateChild("slotFrame");
            StackLabel = this.CreateChild<Label>("slotAmount");
            StackLabel.text = "#"; // Placeholder
            RegisterCallback<PointerDownEvent>( OnPointerDown );
        }

        private void OnPointerDown( PointerDownEvent evt )
        {
            if( evt.button != 0 )// || ItemId == SerializableGuid.Empty )
                return;
            OnStartDrag?.Invoke( evt.position, this );
            evt.StopPropagation();
        }

        public void Clear()
        {
            //ItemId = SerializableGuid.Empty;
            Icon.sprite = null;
        }
        
        public void Set( /*SerializableGuid itemId,*/ Sprite icon, int qty = 0 )
        {
            //ItemId = itemId;
            Icon.sprite = icon != null ? icon : null;
            StackLabel.text = qty > 1 ? qty.ToString() : string.Empty;
            StackLabel.visible = qty > 1;
        }
    }
}