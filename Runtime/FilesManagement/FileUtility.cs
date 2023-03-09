namespace EyapLibrary.FilesManagement
{
	using System.IO;
	using System.Linq;

	public static class FileUtility
	{
		public static string GetLatestFileMatchingPattern(string directoryPath, string pattern)
		{
			var directory = new DirectoryInfo(directoryPath);
			var files = directory.GetFiles(pattern)
							 .OrderByDescending(f => f.LastWriteTimeUtc);
			return files.FirstOrDefault()?.FullName;
		}
	}
}
