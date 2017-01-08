using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OctoCTypes
{
	public class PointerList<T> : IList<T>
	{
		internal Dictionary<int, Element<T>> storage;
		internal int start;

		public PointerList() { Initialize(); }
		public PointerList(T element) : this() { Add(element); }

		public T this[int index] {
			get { return storage[GetPointerToValue(index)].Value; }
			set { storage[GetPointerToValue(index)].Value = value; }
		}

		public int Count { get { return storage.Count - 1; } }

		public bool IsReadOnly => false;

		public void Add(T item)
		{
			int newPoint = GetNewPointer();

			var position = start;
			while (position != 0 && storage[position].Next != 0)
				position = storage[position].Next;

			if (position != 0)
				storage[position].Next = newPoint;
			else
				start = newPoint;
			storage.Add(newPoint, new Element<T> { Next = 0, Value = item });
		}

		public void Clear() { Initialize(); }

		public bool Contains(T item)
		{
			var position = start;
			while (position != 0)
			{
				if (storage[position].Value.Equals(item))
					return true;
				position = storage[position].Next;
			}
			return false;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null) throw new ArgumentNullException("array", "Array must not be null.");
			if (arrayIndex < 0) throw new ArgumentOutOfRangeException("arrayIndex", "ArrayIndex must be greater than or equal to zero.");
			if (array.Length - arrayIndex < Count) throw new ArgumentException("arrayIndex", "PointerList has more elements than fit in array.");

			if (start == 0)
				return;
			var position = start;
			var index = 0;
			while (position != 0)
			{
				array[arrayIndex + index++] = storage[position].Value;
				position = storage[position].Next;
			}
		}

		public IEnumerator<T> GetEnumerator() { return new PointerListEnumerator<T>(this); }

		public int IndexOf(T item)
		{
			if (start != 0)
			{
				var position = start;
				var index = 0;
				while (position != 0)
				{
					if (storage[position].Value.Equals(item))
						return index;
					index++;
					position = storage[position].Next;
				}
			}
			return -1;
		}

		public void Insert(int index, T item)
		{
			if (index < 0) throw new ArgumentOutOfRangeException("index", "Index must be greater than or equal to zero.");
			if (index >= Count) throw new ArgumentOutOfRangeException("index", "Index must be less than Count.");

			int newPoint = GetNewPointer();

			if (index == 0)
			{
				storage.Add(newPoint, new Element<T> { Next = start, Value = item });
				start = newPoint;
				return;
			}

			int position = GetPointerBefore(index);
			var element = new Element<T> { Next = storage[position].Next, Value = item };
			storage.Add(newPoint, element);
			storage[position].Next = newPoint;
		}

		public bool Remove(T item)
		{
			var indexToRemove = IndexOf(item);
			if (indexToRemove == -1)
				return false;
			RemoveAt(indexToRemove);
			return true;
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException("index", "Index did not fall within the bounds of this ArrayList.");
			var point = GetPointerBefore(index);
			var keyToRemove = storage[point].Next;
			storage[point].Next = storage[keyToRemove].Next;
			storage.Remove(keyToRemove);
		}

		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

#region privates

		private void Initialize()
		{
			storage = new Dictionary<int, Element<T>> { { 0, new Element<T> { Next = 0, Value = default(T) } } };
			start = 0;
		}

		private int GetPointerToValue(int index)
		{
			ValueExceptions(index);
			MoveToIndex(index, out int point, out int count);
			DidNotReachIndexException(index, point, count);
			return point;
		}

		private void MoveToIndex(int index, out int point, out int count)
		{
			point = start;
			count = 0;
			while (count++ < index && storage[point].Next != 0)
				point = storage[point].Next;
		}

		private int GetPointerBefore(int index)
		{
			var position = start;
			for (var i = 0; i < index - 1; i++)
			{
				position = storage[position].Next;
			}

			return position;
		}

		private static void DidNotReachIndexException(int index, int point, int count)
		{
			if (count < index || point == 0)
				throw new ArgumentOutOfRangeException("index", "PointerList had fewer elements than index.");
		}

		private void ValueExceptions(int index)
		{
			if (index < 0) throw new ArgumentOutOfRangeException("index", "Index must be greater than or equal to zero.");
			if (Count <= index) throw new ArgumentOutOfRangeException("index", "PointerList has fewer elements than index.");
		}

		private int GetNewPointer()
		{
			var generator = new Random();
			var newPoint = generator.Next();
			while (storage.ContainsKey(newPoint))
				newPoint = generator.Next();
			return newPoint;
		}
		#endregion privates
	}
	internal class Element<S>
	{
		internal int Next;
		internal S Value;

		public override string ToString()
		{
			return Value.ToString() + " -> " + Next;
		}
	}
	public class PointerListEnumerator<T> : IEnumerator<T>
	{
		private Dictionary<int, Element<T>> storage;
		private int start;
		private int point;

		public PointerListEnumerator(PointerList<T> pointerList)
		{
			storage = pointerList.storage;
			var random = new Random();
			var zeroPoint = random.Next();
			while (storage.ContainsKey(zeroPoint))
				zeroPoint = random.Next();
			storage.Add(zeroPoint, new Element<T> { Next = pointerList.start, Value = default(T) });
			point = start = zeroPoint;
		}

		public T Current
		{
			get
			{
				return storage[point].Value;
			}
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}

		public void Dispose()
		{
			storage = null;
			point = 0;
		}

		public bool MoveNext()
		{
			if (storage[point].Next == 0)
				return false;
			point = storage[point].Next;
			return true;
		}

		public void Reset()
		{
			point = start;
		}
	}
}
