using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OctoCTypes
{
	public class PointerList<T> : IList<T>
	{
		private Dictionary<int, Element<T>> storage;
		private int start;

		public PointerList() { Initialize(); }

		public PointerList(T element)
		{
			Initialize();
			Add(element);
		}

		public T this[int index] {
			get {
				if (index < 0) throw new ArgumentOutOfRangeException("index", "Index must be greater than or equal to zero.");
				int point = start;
				var count = 0;
				while (count++ < index && storage[point].Next != 0)
					point = storage[point].Next;
				if (count < index || point == 0)
					throw new ArgumentOutOfRangeException("index", "PointerList had fewer elements than index.");
				return storage[point].Value;
			} set => throw new NotImplementedException(); }

		public int Count { get { return storage.Count - 1; } }

		public bool IsReadOnly { get { return false; } }

		public void Add(T item)
		{
			var generator = new Random();

			var newPoint = generator.Next();
			while (storage.ContainsKey(newPoint))
				newPoint = generator.Next();

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

		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

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

		private void Initialize()
		{
			storage = new Dictionary<int, Element<T>> { { 0, new Element<T> { Next = 0, Value = default(T) } } };
			start = 0;
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
	}
}
