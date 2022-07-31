using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GenerateEnum))]
public class GenerateEnumEditor : Editor
{
    #region Fields

    private GenerateEnum _generateEnum;

    #endregion

    #region Unity Methonds

    private void OnEnable()
    {
        _generateEnum = (GenerateEnum)target;
    }

    #endregion

    #region GUI

    public override void OnInspectorGUI()
    {
        _generateEnum.EnumName = EditorGUILayout.TextField("EnumName", _generateEnum.EnumName);

        EditorGUILayout.Space(10);

        _generateEnum.FilePathAndName = EditorGUILayout.TextField("FilePathAndName", _generateEnum.FilePathAndName);

        EditorGUILayout.Space(10);

        _generateEnum.ShowEntries = EditorGUILayout.Foldout(_generateEnum.ShowEntries, "Entries", false);

        if (_generateEnum.ShowEntries == true)
            ShowEnumEntries();

        EditorGUILayout.Space(20);

        CretaeButtonForCreateEnum();

        if (GUI.changed == true)
            EditorUtility.SetDirty(_generateEnum);
    }

    #endregion

    #region Private GUI Methods

    private void CretaeButtonForCreateEnum()
    {
        if (GUILayout.Button("Create Enum", GUILayout.Height(30)))
            _generateEnum.Go();
    }
    private void ShowEnumEntries()
    {
        List<string> array = _generateEnum.EnumEntries;
        int size = Mathf.Max(0, EditorGUILayout.IntField("Size", array.Count));

        while (size > array.Count)
            array.Add(null);

        while (size < array.Count)
            array.RemoveAt(array.Count - 1);

        for (int i = 0; i < array.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            array[i] = EditorGUILayout.TextField("Element " + i, array[i]);

            if (GUILayout.Button("Remove"))
                array.RemoveAt(i);

            EditorGUILayout.EndVertical();
        }

        _generateEnum.EnumEntries = array;

        if (GUILayout.Button("Add"))
            _generateEnum.EnumEntries.Add(null);
    }

    #endregion
}
