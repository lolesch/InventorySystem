using Code.Runtime.Container.Items;
using Submodules.Utility.Attributes;
using UnityEngine;

namespace Code.Data.SO
{
    [CreateAssetMenu(fileName = "TestItem", menuName = "Scriptable Objects/TestItem")]
    public class TestItem : ScriptableObject, IItemData
    {
        [PreviewIcon] public Sprite Icon;
        public StackLimitType maxStack = StackLimitType.Single;
    }
}
