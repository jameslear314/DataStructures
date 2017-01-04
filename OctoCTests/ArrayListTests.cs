using Microsoft.VisualStudio.TestTools.UnitTesting;
using OctoCTypes;
using System;

namespace OctoCTests
{
	[TestClass]
	public class ArrayListTests
	{
		[TestMethod]
		public void Empty()
		{
			var arrayList = new ArrayList<int>();
			Assert.IsNotNull(arrayList, "ArrayList was not instantiated empty");
			Assert.AreEqual(0, arrayList.Count, "Empty ArrayList should have no elements.");
		}

		[TestMethod]
		public void OneElement()
		{
			var arrayList = new ArrayList<int>(1);
			Assert.IsNotNull(arrayList, "ArrayList was not instantiated with an element");
			Assert.AreEqual(1, arrayList.Count, "ArrayList should have one element.");
		}

		[TestMethod]
		public void Array()
		{
			var arrayList = new ArrayList<int> { 0, 2, 3 };
			Assert.AreEqual(3, arrayList[2]);
			Assert.AreEqual(3, arrayList.Count);
		}

		[TestMethod]
		public void Add()
		{
			var arrayList = new ArrayList<int>();
			Assert.AreEqual(0, arrayList.Count, "Empty ArrayList should be empty");
			arrayList.Add(0);
			Assert.AreEqual(1, arrayList.Count, "ArrayList should have one added element");
			arrayList.Add(1);
			Assert.AreEqual(2, arrayList.Count, "ArrayList should have two added elements");
			arrayList.Add(2);
			Assert.AreEqual(3, arrayList.Count, "ArrayList should have three added elements");
		}

		[TestMethod]
		public void ValueExceptions()
		{
			var arrayList = new ArrayList<int>(0);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[-1]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[1]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[-1] = -1);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[1] = 0);
		}

		[TestMethod]
		public void Value()
		{
			var arrayList = new ArrayList<int>(0);
			Assert.AreEqual(0, arrayList[0]);
			arrayList[0] = 1;
			Assert.AreEqual(1, arrayList[0]);
		}

		[TestMethod]
		public void Clear()
		{
			var arrayList = new ArrayList<int>(0);
			Assert.AreEqual(1, arrayList.Count);
			Assert.AreEqual(0, arrayList[0]);
			arrayList.Clear();
			Assert.AreEqual(0, arrayList.Count);
		}

		[TestMethod]
		public void ClearExceptions()
		{
			var arrayList = new ArrayList<int>(0);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[arrayList.Count]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[-1]);
		}

		[TestMethod]
		public void Contains()
		{
			var arrayList = new ArrayList<int> { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };
			for (var i = 0; i < arrayList.Count; i++)
			{
				Assert.IsTrue(arrayList.Contains(arrayList[i]));
			}
			Assert.IsFalse(arrayList.Contains(4));

		}

		[TestMethod]
		public void Contains16k()
		{
			var multiplier = 256;
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < multiplier * multiplier; i++) arrayList.Add(i);
			Assert.AreEqual(multiplier * multiplier, arrayList.Count);
			for (var i = 0; i < multiplier; i++) Assert.IsTrue(arrayList.Contains(arrayList[arrayList.Count - 1 - i]));
			Assert.IsFalse(arrayList.Contains(multiplier * multiplier));
		}

		[TestMethod]
		public void CopyTo()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(i);
			
			var targetArray = new int[11];
			int start = 1;
			arrayList.CopyTo(targetArray, start);
			for (var i = 0; i < arrayList.Count; i++) Assert.AreEqual(arrayList[i], targetArray[start + i]);
		}

		[TestMethod]
		public void CopyToExceptions()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(i);
			int[] targetArray = null;
			Assert.ThrowsException<ArgumentNullException>(() => arrayList.CopyTo(targetArray, 0));
			targetArray = new int[11];
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList.CopyTo(targetArray, -1));
			Assert.ThrowsException<ArgumentException>(() => arrayList.CopyTo(targetArray, 11));
			Assert.ThrowsException<ArgumentException>(() => arrayList.CopyTo(targetArray, 2));
		}

		[TestMethod]
		public void IndexOf()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(2 * i);

			for (var i = 0; i < 10; i++) Assert.AreEqual(i, arrayList.IndexOf(2 * i));
		}

		[TestMethod]
		public void Insert()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(2 * i);
			for (var i = 0; i < 10; i++) arrayList.Insert(2 * i, 2 * i - 1);

			for (var i = 0; i < 20; i++) Assert.AreEqual(i - 1, arrayList[i]);
		}

		[TestMethod]
		public void InsertExceptions()
		{
			var arrayList = new ArrayList<int>(0);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList.Insert(arrayList.Count, 0));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList.Insert(-1, 0));
		}

		[TestMethod]
		public void RemoveAtReverse()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(i);
			var count = arrayList.Count;
			Assert.AreEqual(10, count);
			for (var i = 0; i < 10; i++) Assert.AreEqual(i, arrayList[i]);

			for (var i = 10; i > 0; i--)
			{
				arrayList.RemoveAt(i - 1);
				count--;
				Assert.AreEqual(count, arrayList.Count);

				for (var j = 0; j < arrayList.Count; j++) Assert.AreEqual(j, arrayList[j]);
			}
		}

		[TestMethod]
		public void RemoveAtBetween()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(i);
			var count = arrayList.Count;
			Assert.AreEqual(10, count);
			for (var i = 0; i < 10; i++) Assert.AreEqual(i, arrayList[i]);

			for (var i = 4; i >= 0; i--)
			{
				arrayList.RemoveAt(2 * i + 1);
				count--;
				Assert.AreEqual(count, arrayList.Count);
			}
			for (var j = 0; j < 5; j++) Assert.AreEqual(2 * j, arrayList[j]);
		}

		[TestMethod]
		public void RemoveAtExceptions()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(i);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList.RemoveAt(-1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList.RemoveAt(10));
		}

		[TestMethod]
		public void Remove()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(i);
			var count = arrayList.Count;
			Assert.AreEqual(10, count);
			for (var i = 0; i < 10; i++) Assert.AreEqual(i, arrayList[i]);

			Assert.IsTrue(arrayList.Remove(3));
			var offset = 0;
			for (var i = 0; i < arrayList.Count; i++)
			{
				if (i != 3) Assert.AreEqual(i + offset, arrayList[i]);
				else offset++;
			}
			Assert.AreEqual(false, arrayList.Contains(3));
			Assert.IsFalse(arrayList.Remove(3));
		}
	}
}
