using UnityEngine;
using UnityEditor;
using System.IO;

namespace com.AmazingFusion.HyperWhiteBloodCellDash.Editor
{
    public class SaveDataEditor
    {
        [MenuItem("Hyper White Blood Cell/Saved Data/Delete All")]
        static void DeleteAllData()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
