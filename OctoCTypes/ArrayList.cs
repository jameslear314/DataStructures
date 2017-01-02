using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OctoCTypes
{
	internal class ArrayList<T> : IList<T>
	{
		private T[] storage;
		private int storageSize;
		private int count;

		public ArrayList()
		{
			storageSize = 0;
			count = 0;
		}

		public ArrayList(T element)
		{
			storageSize = 1;
			storage = new T[1];
			storage[1] = element;
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
