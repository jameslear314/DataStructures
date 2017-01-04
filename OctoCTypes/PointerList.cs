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
			storage = new Dictionary<int, Element<T>>();
		}

		public PointerList(T element)
		{
			var generator = new Random();
			start = generator.Next();
			storage = new Dictionary<int, Element<T>> { { start, new Element<T> { Value = element, Next = 0 } } };
		}

		public T this[int index] {
			get {
				int point = start;
				return storage[point].Value;
			} set => throw new NotImplementedException(); }

		public int Count { get { return storage.Count; } }

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

		internal class Element<S>
		{
			internal int Next;
			internal S Value;
		}
	}

	
}
