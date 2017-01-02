using Microsoft.VisualStudio.TestTools.UnitTesting;
using OctoCTypes;
using System;

namespace OctoCTests
{
	[TestClass]
	public class ArrayListOperations
	{
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
	}
}
