namespace EyapLibrary.Editor
{
	using System.IO;
	using System.Text;
	using UnityEditor;
	using UnityEditor.Localization;
	using UnityEditor.Localization.Plugins.CSV;

	/// <summary>
	/// Editor class for exporting localization tables to CSV files.
	/// </summary>
	public class ExportLocalizationTableEditor
	{
		[MenuItem("EyapLibrary/Localization/Export All CSV Files")]
		public static void ExportAllCsv()
		{
			// Get every String Table Collection
			var stringTableCollections = LocalizationEditorSettings.GetStringTableCollections();

			var path = EditorUtility.SaveFolderPanel("Export All String Table Collections - CSV", "", "");
			if (string.IsNullOrEmpty(path))
				return;

			foreach (var collection in stringTableCollections)
			{
				var file = Path.Combine(path, collection.TableCollectionName + ".csv");
				using (var stream = new StreamWriter(file, false, Encoding.UTF8))
				{
					Csv.Export(stream, collection);
				}
			}
		}
	}
}
