using LinguisticLibrary.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisticLibrary.Data
{
    abstract class CombineTextElement<T> : TextElement, ICollection<T> where T : ITextElement
    {
        private ICollection<T> _items;

        public CombineTextElement()
        {
            _items = new List<T>();
        }

        public override string StringValue
        {
            get
            {
                return _items.Aggregate(string.Empty, (value, item) => string.Concat(value, item.StringValue));
            }
        }


        #region ICollection interface implementation

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
