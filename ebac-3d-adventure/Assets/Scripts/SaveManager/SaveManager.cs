using System.IO;
using UnityEngine;
using Ebac.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{

    [Header("Debugger")]
    private SaveSetup _saveSetup;
    

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 2;
        _saveSetup.playerName = "PlayerTest";
        _saveSetup.health = 50;
        _saveSetup.coins = 0;
        _saveSetup.lifePack = 0;
    }

    #region SAVE
    [NaughtyAttributes.Button]
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        //SaveHealth();
        Save();
    }

    private void SaveName(string name)
    {
        _saveSetup.playerName = name;
        Save();
    }

    private void SaveItems()
    {
        _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.lifePack = Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value;
        Save();
    }

    //private void SaveHealth()
    //{
    //    _saveSetup.health = PlayerBase.Instance.health._currentLife;
    //    Save();
    //}

    private void SaveFile(string json)
    {
        string path = Application.streamingAssetsPath + "/save.txt";

        //string fileLoaded = "";

        //if (File.Exists(path)) fileLoaded = File.ReadAllText(path);

        File.WriteAllText(path, json);
    }
    #endregion

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }

    [NaughtyAttributes.Button]
    private void SaveLevelFive()
    {
        SaveLastLevel(5);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;

    public int coins;
    public int health;
    public int lifePack;
}