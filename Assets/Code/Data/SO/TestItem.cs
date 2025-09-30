using Code.Runtime.Container.Items;
using UnityEngine;

namespace Code.Data.SO
{
    [CreateAssetMenu(fileName = "TestItem", menuName = "Scriptable Objects/TestItem")]
    public class TestItem : ScriptableObject, IItemData
    {
        public Sprite icon;
    }
}
