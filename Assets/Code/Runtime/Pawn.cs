using Code.Runtime.Container;
using Code.Runtime.Container.Items;
using Code.Runtime.Statistics;
using Submodules.Utility.SerializeInterface;
using UnityEngine;

namespace Code.Runtime
{
    public sealed class Pawn : MonoBehaviour, IModifierSource
    {
        [SerializeField] private InterfaceReference<IItemData> itemData;
        [SerializeField] private EnumSlotContainer<EquipmentType> Equipment;

        private void OnValidate()
        {
            var package = new Package( new EquipmentItem( itemData.Value, EquipmentType.Accessory ), 1 );
            Equipment.TryAdd( ref package );
            Equipment.TryRemove( EquipmentType.Accessory );
        }
    }
}