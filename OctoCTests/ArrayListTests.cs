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
		public void ValueOutOfRange()
		{
			var arrayList = new ArrayList<int>(0);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[-1]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[1]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[-1] = -1);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[1] = 0);
		}

		[TestMethod]
		public void ValueInRange()
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
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[0]);
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

			int[] targetArray = null;
			Assert.ThrowsException<ArgumentNullException>(() => arrayList.CopyTo(targetArray, 0));

			targetArray = new int[10];
			arrayList.CopyTo(targetArray, 0);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList.CopyTo(targetArray, -1));
			Assert.ThrowsException<ArithmeticException>(() => arrayList.CopyTo(targetArray, 1));
		}
	}
}
