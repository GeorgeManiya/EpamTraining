using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SweetGift.Interfaces;

namespace SweetGift.Data
{
    class Gift : GiftComponent, ICollection<IGiftComponent>, ISweet, IChocolate
    {
        public Gift()
        {
            _giftComponents = new List<IGiftComponent>();
        }

        private ICollection<IGiftComponent> _giftComponents;

        #region ICollection implementations

        public IEnumerator<IGiftComponent> GetEnumerator()
        {
            return _giftComponents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IGiftComponent item)
        {
            _giftComponents.Add(item);
        }

        public void Clear()
        {
            _giftComponents.Clear();
        }

        public bool Contains(IGiftComponent item)
        {
            return _giftComponents.Contains(item);
        }

        public void CopyTo(IGiftComponent[] array, int arrayIndex)
        {
            _giftComponents.CopyTo(array, arrayIndex);
        }

        public bool Remove(IGiftComponent item)
        {
            return _giftComponents.Remove(item);
        }

        public int Count
        {
            get { return _giftComponents.Count; }
        }

        public bool IsReadOnly
        {
            get { return _giftComponents.IsReadOnly; }
        }

        #endregion

        #region ISweet implementation

        public new int Sugar
        {
            get
            {
                return _giftComponents.Aggregate(0, (totalSugar, component) => totalSugar + component.Sugar);
            }
        }

        public new int Weight
        {
            get
            {
                return _giftComponents.Aggregate(0, (totalWeight, component) => totalWeight + component.Weight);
            }
        }

        #endregion

        #region IChocolate implementation

        public int Chocolate
        {
            get
            {
                return _giftComponents.OfType<IChocolate>()
                    .Aggregate(0, (totalChocolate, c) => totalChocolate + c.Chocolate);
            }
        }

        #endregion

        public override int NetWeight
        {
            get
            {
                return _giftComponents.Aggregate(0, (totalNetWeight, component) => totalNetWeight + component.NetWeight);
            }
        }

        public void SortBy(string propertyName)
        {
            var properties = typeof (GiftComponent).GetProperties();
            if (properties.All(p => p.Name != propertyName))
                throw new Exception(string.Format("Cannot find field '{0}'", propertyName));

            var property = properties.First(p => p.Name == propertyName);
            _giftComponents = _giftComponents.OrderBy(property.GetValue).ToList();
        }
    }
}
