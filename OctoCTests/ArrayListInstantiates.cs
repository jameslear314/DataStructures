using Microsoft.VisualStudio.TestTools.UnitTesting;
using OctoCTypes;

namespace OctoCTests
{
	[TestClass]
    public class ArrayListInstantiates
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
    }
}
