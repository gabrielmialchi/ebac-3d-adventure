using System.IO;
using UnityEngine;
using Ebac.Core.Singleton;
using System;
using Cloth;

public class SaveManager : Singleton<SaveManager>
{
    public int lastLevel;

    public Action<SaveSetup> FileLoaded;

    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    [Header("Debugger")]
    [SerializeField] private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "/save.txt";

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
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
        SaveHealth();
        SaveCheckpointKey();
        SaveClothSetup();
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

    private void SaveHealth()
    {
        _saveSetup.health = PlayerBase.Instance.health._currentLife;
        Save();
    }

    private void SaveCheckpointKey()
    {
        _saveSetup.checkpointKey = CheckpointManager.Instance.lastCheckpointKey;
    }

    private void SaveClothSetup()
    {
        _saveSetup.clothSetup = ClothManager.Instance.clothSetups[0];
    }

    private void SaveFile(string json)
    {
        File.WriteAllText(_path, json);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "PlayerName";
    }

    [NaughtyAttributes.Button]
    public void Load()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }

        FileLoaded.Invoke(_saveSetup);
    }
    #endregion
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;

    public int coins;
    public int health;
    public int lifePack;

    public int checkpointKey;

    public ClothSetup clothSetup;
}