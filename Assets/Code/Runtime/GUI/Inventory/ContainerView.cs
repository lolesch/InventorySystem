using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;

namespace Code.Runtime.GUI.Inventory
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class ContainerView : MonoBehaviour
    {
        public Slot[] Slots;
        
        [SerializeField] protected UIDocument document;
        //[SerializeField] protected StyleSheet styleSheet;
        
        protected static  VisualElement ghostIcon;
        
        static bool isDragging;
        static Slot dragOrigin;
        
        protected VisualElement root;
        protected VisualElement gridContainer;
        
        public event Action<Slot, Slot> onDrop;
        
        public abstract IEnumerator Initialize( int size = 20 );
        
        private IEnumerator Start()
        {
            yield return StartCoroutine( Initialize() ); // Move into controller later
            
            ghostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            ghostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
            foreach( var slot in Slots )
                slot.OnStartDrag += OnStartDrag;
        }

        private void OnPointerUp( PointerUpEvent evt )
        {
            if( !isDragging ) return;

            Slot closestSlot = Slots.Where( x => x.worldBound.Overlaps( ghostIcon.worldBound ) )
                .OrderBy( x => Vector2.Distance( x.worldBound.position, ghostIcon.worldBound.position ) )
                .FirstOrDefault();
            
            if( closestSlot != null )
                onDrop?.Invoke( dragOrigin, closestSlot );
            else
            {
                dragOrigin.Icon.tintColor = Color.white;
                dragOrigin.StackLabel.visible = true;
            }
            
            isDragging = false;
            dragOrigin = null;
            ghostIcon.style.visibility = Visibility.Hidden;
        }

        private void OnPointerMove( PointerMoveEvent evt )
        {
            if( !isDragging ) return;
            
            SetGhostIconPosition( evt.position );
        }

        private static void OnStartDrag( Vector2 position, Slot slot )
        {
            isDragging = true;
            dragOrigin = slot;
            
            SetGhostIconPosition( position );
            
            ghostIcon.style.backgroundImage = dragOrigin.Icon.sprite.texture;
            dragOrigin.Icon.tintColor = Color.gray;
            dragOrigin.StackLabel.visible = false;
            
            ghostIcon.style.visibility = Visibility.Visible;
        }

        private static void SetGhostIconPosition( Vector2 position )
        {
            ghostIcon.style.top = position.y - ghostIcon.layout.height / 2;
            ghostIcon.style.left = position.x - ghostIcon.layout.width / 2;
        }
    }
}