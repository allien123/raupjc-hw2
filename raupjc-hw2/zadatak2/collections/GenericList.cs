using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadatak2.collections
{
    public class GenericList<X> : IGenericList<X>, IEnumerable
    {
        private X[] _internalStorage;
        public int Count { get; private set; }

        public GenericList(int initialSize)
        {
            _internalStorage = new X[initialSize];
        }

        public GenericList(): this(4)
        {
        }

        public void Add(X item)
        {
            if (Count == _internalStorage.Length)
            {
                reallocStorage();
            };

            _internalStorage[Count] = item;
            Count++;
        }

        private void reallocStorage()
        {
            X[] largerStorage = new X[_internalStorage.Length * 2];
            for (int i = 0; i < _internalStorage.Length; i++)
            {
                largerStorage[i] = _internalStorage[i];
            }

            _internalStorage = largerStorage;
        }

        public void Clear()
        {
            Count = 0;
            _internalStorage = new X[4];
        }

        public bool Contains(X item)
        {
            return IndexOf(item) != -1;
        }

        public X GetElement(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            };

            return _internalStorage[index];
        }

        public int IndexOf(X item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_internalStorage[i].Equals(item))
                {
                    return i;
                }
            };
            return -1;
        }

        public bool Remove(X item)
        {
            int index = IndexOf(item);
            return index == -1 ? false : RemoveAt(index);
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            };

            for (int i = index; i < Count - 1; i++)
            {
                _internalStorage[i] = _internalStorage[i + 1];
            }

            Count--;
            return true;
        }

        public IEnumerator<X> GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class GenericListEnumerator<X> : IEnumerator<X>
    {
        private GenericList<X> genericList;
        private int _currentIndex;

        public GenericListEnumerator(GenericList<X> genericList)
        {
            this.genericList = genericList;
        }

        public void Dispose()
        {
            Current = default(X);
        }

        public bool MoveNext()
        {
            if (_currentIndex < genericList.Count)
            {
                Current = genericList.GetElement(_currentIndex);
                _currentIndex++;
                return true;
            };
            return false;
        }

        public void Reset()
        {
            _currentIndex = 0;
        }

        public X Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
