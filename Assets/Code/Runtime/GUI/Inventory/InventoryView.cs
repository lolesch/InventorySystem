using System.Collections;
using System.Collections.Generic;
using Code.Data.SO;
using Code.Runtime.Container;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

namespace Code.Runtime.GUI.Inventory
{
    public class InventoryView : ContainerView
    {
        [SerializeField] int capacity = 20;
        [SerializeField] private List<TestItem> startingItems = new ();
        [SerializeField] private LocalizedString menuName;

        public override IEnumerator Initialize( int size )
        {
            Slots = new Slot[capacity];
            
            root = document.rootVisualElement;
            
            var varStyle = Resources.Load<StyleSheet>( UIReferences.GlobalVariablesStyle );
            if( !root.styleSheets.Contains(varStyle) )
                root.styleSheets.Insert(0, varStyle);
            
            gridContainer = root.Q<VisualElement>( "grid" );
            gridContainer.Clear();
            
            var label = root.Q<Label>( "header" );
            label.text = menuName.GetLocalizedString();
            
            for (int i = 0; i < size; i++)
            {
                var slot = gridContainer.CreateChild<Slot>( "inventory-slot" );
                Slots[i] = slot;
                //slot.Initialize(i);
            }

            ghostIcon = gridContainer.CreateChild( "ghostIcon" );
            ghostIcon.BringToFront();

            yield return null;

            for( var i = 0; i < startingItems.Count; i++ )
            {
                var item = startingItems[i];
                Slots[i].Set( item.Icon, (int) item.maxStack );
            }
        }
    }
}