using System;
using System.Collections;
using System.Collections.Generic;

namespace OctoCTypes
{
	public class ArrayList<T> : IList<T>
	{
		private T[] storage;
		private int storageSize;
		private int count;

		public ArrayList()
		{
			storageSize = 1;
			storage = new T[1];
			count = 0;
		}

		public ArrayList(T element)
		{
			storageSize = 1;
			storage = new T[1] { element };
			count = 1;
		}

		public T this[int index] {
			get {
				if (index > Count - 1 || index < 0) throw new ArgumentOutOfRangeException("Index was outside the bounds of the ArrayList.");
				return storage[index];
			}
			set {
				if (index > Count - 1 || index < 0) throw new ArgumentOutOfRangeException("Index was outside the bounds of the ArrayList.");
				storage[index] = value;
			} }
		public int Count { get { return count; } }
		public bool IsReadOnly { get { return false; } }

		private void Expand()
		{
			var oldArray = storage;
			storage = new T[storageSize * 2];
			var i = 0;
			while (i < count)
			{
				storage[i] = oldArray[i];
				i++;
			}
			storageSize *= 2;
		}

		public void Add(T item)
		{
			while (count >= storageSize) Expand();
			storage[count] = item;
			count++;
		}

		public void Clear()
		{
			count = 0;
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

		public int IndexOf(T item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		public bool Remove(T item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
