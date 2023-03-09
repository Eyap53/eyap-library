namespace EyapLibrary.FilesManagement.Tests
{
	using System.IO;
	using System.Linq;
	using NUnit.Framework;
	using UnityEngine;
	using UnityEngine.TestTools;

	public class FileUtilityTests
	{
		private const string TestDirectoryPath = "Assets/TestData";
		private const string TestFilePath1 = "Assets/TestData/TestFile1.txt";
		private const string TestFilePath2 = "Assets/TestData/TestFile2.txt";

		[SetUp]
		public void SetUp()
		{
			Directory.CreateDirectory(TestDirectoryPath);
			File.WriteAllText(TestFilePath1, "Test content 1");
			File.WriteAllText(TestFilePath2, "Test content 2");
		}

		[TearDown]
		public void TearDown()
		{
			File.Delete(TestFilePath1);
			File.Delete(TestFilePath2);
			Directory.Delete(TestDirectoryPath);
		}

		[Test]
		public void GetLatestFileMatchingPattern_ReturnsLatestFile()
		{
			// Arrange
			FileInfo expectedFile = new FileInfo(TestFilePath2);

			// Act
			var actualFile = FileUtility.GetLatestFileMatchingPattern(TestDirectoryPath, "*.txt");

			// Assert
			Assert.AreEqual(expectedFile.FullName, actualFile.FullName);
		}

		[Test]
		public void GetLatestFileMatchingPattern_NoMatchingFiles_ReturnsNull()
		{
			// Act
			var actualFile = FileUtility.GetLatestFileMatchingPattern(TestDirectoryPath, "*.png");

			// Assert
			Assert.IsNull(actualFile);
		}
	}
}
