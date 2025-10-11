using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Runtime.GUI.Inventory
{
    public class InventoryView : ContainerView
    {
        public override IEnumerator Initialize(int size = 20)
        {
            yield return null;
            Slots = new Slot[size];
            
            root = document.rootVisualElement;
            
            gridContainer = root.Q<VisualElement>("GridContainer");
            gridContainer.Clear();
            
            for (int i = 0; i < size; i++)
            {
                var slot = new Slot();
                //slot.Initialize(i);
                slot.AddToClassList("slot"); // Attach USS styling
                
                Slots[i] = slot;
                gridContainer.Add(slot);
            }
        }
    }
}