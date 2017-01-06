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

		public PointerList()
		{
			storage = new Dictionary<int, Element<T>> { { 0, new Element<T> { Next = 0, Value = default(T) } } };
		}

		public PointerList(T element)
		{
			var generator = new Random();
			start = generator.Next();
			storage = new Dictionary<int, Element<T>> { { 0, new Element<T> { Next = 0, Value = default(T) } }, { start, new Element<T> { Value = element, Next = 0 } } };
		}

		public T this[int index] {
			get {
				if (start == 0) return storage[start].Value;
				int point = start;
				var count = 0;
				while (count++ < index)
					point = storage[point].Next;
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
