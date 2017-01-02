using Microsoft.VisualStudio.TestTools.UnitTesting;
using OctoCTypes;

namespace OctoCTests
{
	[TestClass]
    public class ArrayListOperations
    {
		[TestMethod]
		[Timeout(300)]
		public void Add()
		{
			System.Console.WriteLine("Add test begun");
			var arrayList = new ArrayList<int>();
			Assert.AreEqual(0, arrayList.Count, "Empty ArrayList should be empty");
			arrayList.Add(0);
			Assert.AreEqual(1, arrayList.Count, "ArrayList should have one added element");
			arrayList.Add(1);
			Assert.AreEqual(2, arrayList.Count, "ArrayList should have two added elements");
			arrayList.Add(2);
			Assert.AreEqual(3, arrayList.Count, "ArrayList should have three added elements");
		}
	}
}
