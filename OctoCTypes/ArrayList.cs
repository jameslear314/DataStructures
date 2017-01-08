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
		public ArrayList(T element) : this() { storage[count++] = element; }

		public T this[int index] {
			get
			{
				CheckOutOfRange(index);
				return storage[index];
			}
			set
			{
				CheckOutOfRange(index);
				storage[index] = value;
			} }

		private void CheckOutOfRange(int index)
		{
			if (index >= Count || index < 0) throw new ArgumentOutOfRangeException("Index was outside the bounds of the ArrayList.");
		}

		public int Count { get { return count; } }
		public bool IsReadOnly => false;

		private void Expand()
		{
			var oldArray = storage;
			storage = new T[storageSize *= 2];
			var i = 0;
			while (i < count)
				storage[i] = oldArray[i++];
		}

		public void Add(T item)
		{
			while (count >= storageSize)
				Expand();
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
				if (storage[i].Equals(item)) return true;
			return false;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null) throw new ArgumentNullException("array", "Array receiving elements must not be null.");
			if (arrayIndex < 0) throw new ArgumentOutOfRangeException("arrayIndex", "Array index must be positive.");
			if (array.Length - arrayIndex < count) throw new ArgumentException("Array receiving elements must be longer than this ArrayList.");
			var sourceArray = new T[count];
			for (var i = 0; i < count; i++)
				sourceArray[i] = storage[i];
			sourceArray.CopyTo(array, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator()
		{
			var sourceArray = new T[count];
			for (var i = 0; i < count; i++)
				sourceArray[i] = storage[i];
			return new ArrayListEnumerator<T>(this);
		}

		public int IndexOf(T item)
		{
			for (var i = 0; i < count; i++)
				if (storage[i].Equals(item))
					return i;
			return -1;
		}

		public void Insert(int index, T item)
		{
			IndexOutOfRange(index);
			Add(storage[count - 1]);
			for (var i = count - 2; i > index; i--)
				storage[i] = storage[i - 1];
			storage[index] = item;
		}

		public bool Remove(T item)
		{
			var index = IndexOf(item);
			if (index == -1 || index >= count)
				return false;
			RemoveAt(index);
			return true;
		}

		public void RemoveAt(int index)
		{
			IndexOutOfRange(index);
			while (index < count)
				storage[index] = storage[++index];
			count--;
			storage[count] = default(T);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private void IndexOutOfRange(int index)
		{
			if (index < 0 || index >= count) throw new ArgumentOutOfRangeException("index", "Index must be within the index range of the ArrayList");
		}
	}

	public class ArrayListEnumerator<T> : IEnumerator<T>
	{
		private readonly T[] storage;
		private int current;
		public ArrayListEnumerator(ArrayList<T> arrayList)
		{
			storage = new T[arrayList.Count];
			for (var i = 0; i < arrayList.Count; i++)
				storage[i] = arrayList[i];
			current = -1;
		}

		public T Current { get { if (current < 0 || current >= storage.Length) return default(T); return storage[current]; } }

		object IEnumerator.Current { get { return Current; } }

		public void Dispose() { }

		public bool MoveNext()
		{
			current++;
			return current < storage.Length;
		}

		public void Reset()
		{
			current = -1;
		}
	}
}
