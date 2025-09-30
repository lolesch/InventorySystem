using System;
using Code.Runtime.Container.Items;

namespace Code.Runtime.Container
{
    public interface IPackage: IEquatable<Package>
    {
        // AbstractContainer Sender { get; }
        AbstractItem Item { get; }
        uint Amount { get; }
        bool IsValid { get; }
        uint SpaceLeft { get; }
        
        uint Increase( uint amountToAdd );
        uint Reduce( uint amountToRemove );
    }
}