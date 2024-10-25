using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private bool initilizeDataIfNull = false;

    [SerializeField] private string fileName;
    private GameData gameData;

    private List<IDataPersistance> dataPersistanceObjects;

    public static DataPersistenceManager instance { get; private set; }

    private FileDataHandler fileDataHandler;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        StaticEnterData.travelType = TravelType.Nothing;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }



    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        // MonoBehaviour türündeki tüm nesneleri bul ve IDataPersistance arayüzünü uygulayanlarý filtrele
        MonoBehaviour[] allBehaviours = FindObjectsOfType<MonoBehaviour>();
        List<IDataPersistance> dataPersistances = new List<IDataPersistance>();

        foreach (var behaviour in allBehaviours)
        {
            if (behaviour is IDataPersistance dataPersistence)
            {
                dataPersistances.Add(dataPersistence);
            }
        }

        return dataPersistances;
    }

    private void OnApplicationQuit()
    {
        GetBonfire();
    }

    private void GetBonfire()
    {
        if (gameData.lastBonfire != "")
        {
            gameData.litBonfireLocations.TryGetValue(gameData.lastBonfire, out gameData.playerPos);
            gameData.litBonfires.TryGetValue(gameData.lastBonfire, out gameData.currentScene);
        }
        SaveGame();

    }


    

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("on scane loaded");
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("on scane unloaded");
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        SaveGame();
    }
    public void LoadGame()
    {
        Debug.Log("Load Game Run");
        this.gameData = fileDataHandler.Load(); 

        if(this.gameData == null)
        {
            if(initilizeDataIfNull)
                NewGame();

            else
                return;
        }

        foreach(IDataPersistance data in dataPersistanceObjects)
        {
            data.LoadData(gameData);
        }



    }
    public void SaveGame()
    {
        Debug.Log("save game run");
        if(this.gameData == null)
        {
            return;
        }

        foreach (IDataPersistance data in dataPersistanceObjects)
        {
            data.SaveData(ref gameData);
        }



        fileDataHandler.Save(gameData);
        if(SceneManager.GetActiveScene().name != "MainMenu")
            EventSystem.instance.GameSaved();
    }

    public GameData GetData()
    {
        return this.gameData;
    }

    public bool HasGameData()
    {
        return this.gameData != null;
    }
}
