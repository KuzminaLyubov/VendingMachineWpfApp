using System.IO;

namespace VendingMachineWpfApp
{
    public static class VendingMachineStateHandler
    {
		/// <summary>
		/// Writes the given object instance to a binary file.
		/// </summary>
		public static void Save<T>(string filePath, T objectToWrite)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

		/// <summary>
		/// Reads an object instance from a binary file.
		/// </summary>
		public static T Restore<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
