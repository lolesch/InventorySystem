using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Container.Items;
using UnityEngine;

namespace Code.Runtime.Container
{
    [Serializable]
    internal sealed class WeightContainer
    {
        [SerializeField] private List<Package> contents;
        public List<Package> Contents => contents;
        public event Action<List<Package>> OnContentsChanged;
        public readonly int MaxLoad;
        // instead of summing every time, keep track of current weight and update on add/remove
        public float Weight => contents.Sum( package => package.Amount * ( package.Item as IWeightItem ).weight );
        private bool CanAdd( float weight ) => weight + Weight <= MaxLoad;
        private bool CanAdd( Package package ) => CanAdd( (package.Item as IWeightItem ).weight );

        internal WeightContainer( int maxLoad ) 
        {
            MaxLoad = maxLoad;
        }

    }
}