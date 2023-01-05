namespace EyapLibrary.Data
{
    using System.IO;
    using UnityEngine;

    /// <summary>
    /// Json implementation of file saver
    /// </summary>
    public class JsonSaver<T> : FileSaver<T> where T : IDataStore
    {
        public JsonSaver(string filename)
            : base(filename)
        {
        }

        /// <summary>
        /// Save the specified data store
        /// </summary>
        public override void Save(T data)
        {
            data.PreSave();

            using (StreamWriter writer = GetWriteStream())
            {
                string json = JsonUtility.ToJson(data);
                writer.Write(json);
            }
        }

        /// <summary>
        /// Load the specified data store
        /// </summary>
        public override bool Load(out T data)
        {
            if (!File.Exists(_filePath))
            {
                data = default(T);
                return false;
            }

            using (StreamReader reader = GetReadStream())
            {
                data = JsonUtility.FromJson<T>(reader.ReadToEnd());
            }

            data.PostLoad();
            return true;
        }

        protected virtual StreamWriter GetWriteStream()
        {
            return new StreamWriter(new FileStream(_filePath, FileMode.Create));
        }

        protected virtual StreamReader GetReadStream()
        {
            return new StreamReader(new FileStream(_filePath, FileMode.Open));
        }
    }
}
