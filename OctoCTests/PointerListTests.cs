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
    }
}
