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
			for (var i = 0; i < count; i++)
			{
				if (storage[i].Equals(item)) return true;
			}
			return false;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null) throw new ArgumentNullException("array", "Array receiving elements must not be null.");
			if (arrayIndex < 0) throw new ArgumentOutOfRangeException("arrayIndex", "Array index must be positive.");
			if (array.Length - arrayIndex < count) throw new ArgumentException("Array receiving elements must be longer than this ArrayList.");
			var sourceArray = new T[count];
			for (var i = 0; i < count; i++) sourceArray[i] = storage[i];
			sourceArray.CopyTo(array, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public int IndexOf(T item)
		{
			for (var i = 0; i < count; i++)
			{
				if (storage[i].Equals(item))
					return i;
			}
			return -1;
		}

		public void Insert(int index, T item)
		{
			IndexOutOfRange(index);
			Add(storage[count - 1]);
			for (var i = count - 2; i > index; i--)
			{
				storage[i] = storage[i - 1];
			}
			storage[index] = item;
		}

		public bool Remove(T item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			IndexOutOfRange(index);
			while (index < count) storage[index] = storage[++index];
			count--;
			storage[count] = default(T);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		private void IndexOutOfRange(int index)
		{
			if (index < 0 || index >= count) throw new ArgumentOutOfRangeException("index", "Index must be within the index range of the ArrayList");
		}
	}
}
