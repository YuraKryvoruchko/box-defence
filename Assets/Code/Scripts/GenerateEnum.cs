#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "GenerateEnum", menuName = "Box Defence/GenerateEnum", order = 1)]
public class GenerateEnum : ScriptableObject
{
    [SerializeField] private string _enumName = "MyEnum";
    [SerializeField] private List<string> _enumEntries = new List<string>();
    [SerializeField] private string _filePathAndName;
    [SerializeField] private bool _show;
    public string EnumName { get => _enumName; set => _enumName = value; }
    public List<string> EnumEntries { get => _enumEntries; set => _enumEntries = value; }
    public string FilePathAndName { get => _filePathAndName; set => _filePathAndName = value; }
    public bool ShowEntries { get => _show; set => _show = value; }

    [MenuItem("Tools/GenerateEnum")]
    public void Go()
    {
        using (StreamWriter streamWriter = new StreamWriter(_filePathAndName + _enumName + ".cs"))
        {
            streamWriter.WriteLine("public enum " + _enumName);
            streamWriter.WriteLine("{");
            for (int i = 0; i < _enumEntries.Count; i++)
            {
                streamWriter.WriteLine("\t" + _enumEntries[i] + ",");
            }
            streamWriter.WriteLine("}");
        }
        AssetDatabase.Refresh();
    }
}
#endif