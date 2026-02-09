using UnityEngine.UIElements;

namespace Code.Runtime.GUI
{
    public static class VisualElementExtensions
    {
        public static VisualElement CreateChild( this VisualElement parent, params string[] classes )
        {
            var child = new VisualElement();
            AddClass( child, classes ).AddTo( parent );
            return child;
        }
        public static T CreateChild<T>( this VisualElement parent, params string[] classes ) where T : VisualElement, new()
        {
            var child = new T();
            AddClass( child, classes ).AddTo( parent );
            return child;
        }

        private static T AddTo<T>( this T child, VisualElement parent ) where T : VisualElement
        {
            parent.Add( child );
            return child;
        }

        private static T AddClass<T>( this T visualElement, params string[] classes ) where T : VisualElement
        {
            foreach( var className in classes )
                if( !string.IsNullOrEmpty( className ) )
                    visualElement.AddToClassList( className );

            return visualElement;
        }

        private static T AddTo<T>( this T visualElement, IManipulator manipulator ) where T : VisualElement
        {
            visualElement.AddManipulator( manipulator );
            return visualElement;
        }
    }
}