using UnityEngine.UIElements;

namespace Code.Runtime.GUI.Inventory
{
    public sealed class Slot : VisualElement
    {
        public Image Icon;
        public Label StackLabel;

        public int Index => parent.IndexOf( this );
    }
}