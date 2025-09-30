using Code.Runtime.Container;
using Code.Runtime.Container.Items;
using Code.Runtime.Statistics;
using Submodules.Utility.SerializeInterface;
using UnityEngine;

namespace Code.Runtime
{
    public sealed class Pawn : MonoBehaviour, IModifierSource
    {
        [SerializeField] private Modifier testModifier;
        [SerializeField] private InterfaceReference<IItemData> itemData;
        [SerializeField] private InfiniteContainer infinite;
        [SerializeField] private EnumSlotContainer<StackLimit> enumSlotContainer = new();

        private void Start()
        {
            
        }
    }
}