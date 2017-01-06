using Microsoft.VisualStudio.TestTools.UnitTesting;
using OctoCTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OctoCTests
{
	[TestClass]
	public class PointerListTests
	{
		[TestMethod]
		public void Empty()
		{
			var pointList = new PointerList<int>();
			Assert.IsNotNull(pointList, "pointList was not instantiated empty");
			Assert.AreEqual(0, pointList.Count, "Empty pointList should have no elements.");
		}

		[TestMethod]
		public void IsNotReadOnly()
		{
			var pointList = new PointerList<int>();
			Assert.IsFalse(pointList.IsReadOnly);
		}

		[TestMethod]
		public void OneElement()
		{
			var pointList = new PointerList<int>(0);
			Assert.IsNotNull(pointList, "pointList was not instantiated with an element");
			Assert.AreEqual(1, pointList.Count, "pointList should have one element.");
			Assert.AreEqual(0, pointList[0]);
		}

		[TestMethod]
		[Timeout(100)]
		public void Array()
		{
			var pointList = new PointerList<int> { 0, 2, 3 };
			Assert.AreEqual(0, pointList[0]);
			Assert.AreEqual(2, pointList[1]);
			Assert.AreEqual(3, pointList[2]);
			Assert.AreEqual(3, pointList.Count);
		}

		[TestMethod]
		public void Add()
		{
			var pointList = new PointerList<int>();
			Assert.AreEqual(0, pointList.Count, "Empty PointerList should be empty");
			pointList.Add(0);
			Assert.AreEqual(1, pointList.Count, "PointerList should have one added element");
			pointList.Add(1);
			Assert.AreEqual(2, pointList.Count, "PointerList should have two added elements");
			pointList.Add(2);
			Assert.AreEqual(3, pointList.Count, "PointerList should have three added elements");
		}

		[TestMethod]
		public void ValueExceptions()
		{
			var pointList = new PointerList<int>(0);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => pointList[-1]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => pointList[1]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => pointList[-1] = -1);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => pointList[1] = 0);
		}

		[TestMethod]
		public void Value()
		{
			var pointList = new PointerList<int>(0);
			Assert.AreEqual(0, pointList[0]);
			pointList[0] = 1;
			Assert.AreEqual(1, pointList[0]);
		}

		[TestMethod]
		public void Clear()
		{
			var pointList = new PointerList<int>(0);
			Assert.AreEqual(1, pointList.Count);
			Assert.AreEqual(0, pointList[0]);
			pointList.Clear();
			Assert.AreEqual(0, pointList.Count);
		}

		[TestMethod]
		public void ClearExceptions()
		{
			var pointList = new PointerList<int>(0);
			pointList.Clear();
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => pointList[pointList.Count]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => pointList[-1]);
		}

		[TestMethod]
		public void Contains()
		{
			var pointList = new PointerList<int> { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };
			for (var i = 0; i < pointList.Count; i++)
			{
				Assert.IsTrue(pointList.Contains(pointList[i]), "found " + pointList[i].ToString() + " but expected " + i.ToString());
			}
			Assert.IsFalse(pointList.Contains(4));

		}

		[TestMethod]
		public void Contains16k()
		{
			var multiplier = 128;
			var pointList = new PointerList<int>();
			for (var i = 0; i < multiplier * multiplier; i++) pointList.Add(i);
			Assert.AreEqual(multiplier * multiplier, pointList.Count);
			for (var i = 0; i < multiplier; i++) Assert.IsTrue(pointList.Contains(pointList[pointList.Count - 1 - i]));
			Assert.IsFalse(pointList.Contains(multiplier * multiplier));
		}

		[TestMethod]
		public void CopyTo()
		{
			var pointList = new PointerList<int>();
			for (var i = 0; i < 10; i++) pointList.Add(i);

			var targetArray = new int[11];
			int start = 1;
			pointList.CopyTo(targetArray, start);
			for (var i = 0; i < pointList.Count; i++) Assert.AreEqual(pointList[i], targetArray[start + i]);
		}

		[TestMethod]
		public void CopyToExceptions()
		{
			var pointList = new PointerList<int>();
			for (var i = 0; i < 10; i++) pointList.Add(i);
			int[] targetArray = null;
			Assert.ThrowsException<ArgumentNullException>(() => pointList.CopyTo(targetArray, 0));
			targetArray = new int[11];
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => pointList.CopyTo(targetArray, -1));
			Assert.ThrowsException<ArgumentException>(() => pointList.CopyTo(targetArray, 11));
			Assert.ThrowsException<ArgumentException>(() => pointList.CopyTo(targetArray, 2));
		}

		[TestMethod]
		public void IndexOf()
		{
			var pointList = new PointerList<int>();
			for (var i = 0; i < 10; i++) pointList.Add(2 * i);

			for (var i = 0; i < 10; i++) Assert.AreEqual(i, pointList.IndexOf(2 * i));
		}

		[TestMethod]
		public void Insert()
		{
			var pointList = new PointerList<int>();
			for (var i = 0; i < 10; i++) pointList.Add(2 * i);
			for (var i = 0; i < 10; i++) pointList.Insert(2 * i, 2 * i - 1);

			for (var i = 0; i < 20; i++) Assert.AreEqual(i - 1, pointList[i]);
		}

		[TestMethod]
		public void InsertExceptions()
		{
			var pointList = new PointerList<int>(0);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => pointList.Insert(pointList.Count, 0));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => pointList.Insert(-1, 0));
		}

		[TestMethod]
		public void RemoveAtReverse()
		{
			var pointList = new PointerList<int>();
			for (var i = 0; i < 10; i++) pointList.Add(i);
			var count = pointList.Count;
			Assert.AreEqual(10, count);
			for (var i = 0; i < 10; i++) Assert.AreEqual(i, pointList[i]);

			for (var i = 10; i > 0; i--)
			{
				pointList.RemoveAt(i - 1);
				count--;
				Assert.AreEqual(count, pointList.Count);

				for (var j = 0; j < pointList.Count; j++) Assert.AreEqual(j, pointList[j]);
			}
		}

		[TestMethod]
		public void RemoveAtBetween()
		{
			var pointList = new PointerList<int>();
			for (var i = 0; i < 10; i++) pointList.Add(i);
			var count = pointList.Count;
			Assert.AreEqual(10, count);
			for (var i = 0; i < 10; i++) Assert.AreEqual(i, pointList[i]);

			for (var i = 4; i >= 0; i--)
			{
				pointList.RemoveAt(2 * i + 1);
				count--;
				Assert.AreEqual(count, pointList.Count);
			}
			for (var j = 0; j < 5; j++) Assert.AreEqual(2 * j, pointList[j]);
		}

		[TestMethod]
		public void RemoveAtExceptions()
		{
			var pointList = new PointerList<int>();
			for (var i = 0; i < 10; i++) pointList.Add(i);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => pointList.RemoveAt(-1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => pointList.RemoveAt(10));
		}

		[TestMethod]
		public void Remove()
		{
			var pointList = new PointerList<int>();
			for (var i = 0; i < 10; i++) pointList.Add(i);
			var count = pointList.Count;
			Assert.AreEqual(10, count);
			for (var i = 0; i < 10; i++) Assert.AreEqual(i, pointList[i]);

			Assert.IsTrue(pointList.Remove(3));
			var offset = 0;
			for (var i = 0; i < pointList.Count; i++)
			{
				if (i != 3) Assert.AreEqual(i + offset, pointList[i]);
				else offset++;
			}
			Assert.AreEqual(false, pointList.Contains(3));
			Assert.IsFalse(pointList.Remove(3));
		}

		[TestMethod]
		public void IEnumeratorT()
		{
			var pointList = new PointerList<int>();
			for (var i = 0; i < 5; i++) pointList.Add(i);
			IEnumerator<int> intEnum = pointList.GetEnumerator();
			int count = 0;

			Assert.ThrowsException<IndexOutOfRangeException>(() => intEnum.Current);
			while (intEnum.MoveNext())
				Assert.AreEqual(count++, intEnum.Current);
			Assert.ThrowsException<IndexOutOfRangeException>(() => intEnum.Current);
			intEnum.Reset();
			Assert.ThrowsException<IndexOutOfRangeException>(() => intEnum.Current);
			intEnum.Dispose();
		}

		[TestMethod]
		public void IEnumerator()
		{
			var pointList = new PointerList<int>();
			for (var i = 0; i < 5; i++) pointList.Add(i);
			IEnumerator intEnum = ((IEnumerable)pointList).GetEnumerator();
			int count = 0;

			Assert.ThrowsException<IndexOutOfRangeException>(() => intEnum.Current);
			while (intEnum.MoveNext())
				Assert.AreEqual(count++, intEnum.Current);
			Assert.ThrowsException<IndexOutOfRangeException>(() => intEnum.Current);
			intEnum.Reset();
			Assert.ThrowsException<IndexOutOfRangeException>(() => intEnum.Current);
		}
	}
}
