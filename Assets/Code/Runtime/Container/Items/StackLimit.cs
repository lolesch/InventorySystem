using System;

namespace Code.Runtime.Container.Items
{
    [Serializable]
    public enum StackLimit : byte
    {
        Single = 1,
        StackOfTen = 10,
        StackOfFifty = 50,
        StackOfHundred = 100,
    }
}