using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

namespace Code.Runtime.GUI.Inventory
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class ContainerView : MonoBehaviour
    {
        public Slot[] Slots;
        
        [SerializeField] protected UIDocument document;
        [SerializeField] protected StyleSheet styleSheet;
        
        protected VisualElement root;
        protected VisualElement gridContainer;
        
        public abstract IEnumerator Initialize( int size = 20 );
        
        private IEnumerator Start()
        {
            yield return StartCoroutine( Initialize() ); // Move into controller later
        }
    }
}