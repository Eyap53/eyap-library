namespace EyapLibrary.Utils.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using NUnit.Framework;
	using EyapLibrary.Utils;

	[TestFixture]
	public class EnumUtilsTests
	{
		private enum MyEnum
		{
			Value1,
			Value2,
			Value3
		}

		[Test]
		public void GetValuesTest()
		{
			IEnumerable<MyEnum> myEnum = EnumUtils.GetValues<MyEnum>();
			Assert.True(myEnum.Contains(MyEnum.Value1));
			Assert.True(myEnum.Contains(MyEnum.Value2));
			Assert.True(myEnum.Contains(MyEnum.Value3));
			Assert.AreEqual(3, myEnum.Count());
		}
	}
}
