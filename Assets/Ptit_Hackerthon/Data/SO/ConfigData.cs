using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigData", menuName = "ScriptableObjects/ConfigData", order = 1)]
public class ConfigData : SingletonScriptableObject<ConfigData>
{
    public List<TrashData> trashDataList = new List<TrashData>();

    public TrashData GetData(int id)
    {
        foreach (TrashData data in trashDataList)
        {
            if (data.id == id)
            {
                return data;
            }
        }
        return null;
    }
}

[System.Serializable]
public class TrashData
{
    public int id;
    public TrashType type;
    public string name;
    public Sprite image;
}
