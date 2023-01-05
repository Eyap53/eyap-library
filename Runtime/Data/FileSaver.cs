namespace EyapLibrary.Data
{
	using System.IO;
	using UnityEngine;

	public abstract class FileSaver<T> : IDataSaver<T> where T : IDataStore
	{
		protected readonly string _filePath;

        /// <summary>
        /// Instantiate a file saver, for managing save/load.
        /// PlayersPrefs are also easy to use, but should not be used according to unity forum.
        /// </summary>
        /// <param name="filename">The filename (including extension) to use.</param>
        /// <remarks><paramref name="filename" /> is relative to <see cref="Application.persistentDataPath" /></remarks>
        protected FileSaver(string filename)
		{
			_filePath = GetFinalSaveFilename(filename);
		}

		public abstract void Save(T data);

		public abstract bool Load(out T data);

		public void Delete()
		{
			File.Delete(_filePath);
		}

		public static string GetFinalSaveFilename(string filename)
		{
			return Path.Combine(Application.persistentDataPath, filename);
		}
	}
}
