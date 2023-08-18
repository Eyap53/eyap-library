namespace EyapLibrary.Data
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Binary implementation of file saver
    /// </summary>
    public class BinarySaver<T> : FileSaver<T> where T : IDataStore
    {
        public BinarySaver(string filename)
            : base(filename)
        {
        }

        /// <summary>
        /// Save the specified data store
        /// </summary>
        public override void Save(T data)
        {
            data.PreSave();

            using (FileStream stream = new FileStream(_filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
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

            using (FileStream stream = new FileStream(_filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                data = (T)formatter.Deserialize(stream);
            }

            data.PostLoad();
            return true;
        }
    }
}
