using System;
using Code.Runtime.Container.Items;

namespace Code.Runtime.Container
{
    public interface IPackage: IEquatable<Package>
    {
        // AbstractContainer Sender { get; }
        AbstractItem Item { get; }
        uint Amount { get; }
        bool hasValidItem { get; }
        uint spaceLeft { get; }
        
        uint Add( uint amountToAdd );
        uint Remove( uint amountToRemove );
    }
}