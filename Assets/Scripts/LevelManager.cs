using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelSettings
{
    public GameObject blackPrefab;
    public GameObject whitePrefab;
    public GameObject currentPlayerObj;

    public int levelSize = 30;
    public int nextLevelSize = 30;
    public int minMapSize = 2;
    public int maxMapSize = 8;

    [HideInInspector]
    public float height;
    [HideInInspector]
    public float width;

    public bool infiniteLevel;
    public List<GameObject> levelObjects = new List<GameObject>(); 
}

[System.Serializable]
public class PrefabsConfig
{   
    public GameObject playerSquarePrefab;
    public GameObject playerCirclePrefab;
    public GameObject playerTrianglePrefab;
    public GameObject playerPrefab;
    public GameObject googlePlay;
    public GameObject deathScreen;

    public Button btnPlayerSwitch;
}

public class LevelManager : MonoBehaviour
{
    public LevelSettings settings;

    public PrefabsConfig prefabsConfig;

    private void Start()
    {
        settings.height = Camera.main.orthographicSize * 2.0f;
        settings.width = settings.height * Screen.width / Screen.height;
    }

    public void selectSquare()
    {
        prefabsConfig.playerPrefab = prefabsConfig.playerSquarePrefab;
    }

    public void selectCircle()
    {
        prefabsConfig.playerPrefab = prefabsConfig.playerCirclePrefab;
    }

    public void selectTriangle()
    {
        prefabsConfig.playerPrefab = prefabsConfig.playerTrianglePrefab;
    }

    public void deathScreen()
    {
        if (!prefabsConfig.deathScreen.activeInHierarchy)
            prefabsConfig.deathScreen.SetActive(true);
    }

    public void CreateNewLevel()
    {
        for (int i = 0; i < settings.levelSize; i++)
        {
            if (i == 0)
            {
                Camera.main.transform.position = Vector3.zero;

                GameObject blackMap = Instantiate(settings.blackPrefab);

                blackMap.transform.localScale = new Vector3(settings.width, settings.height);

                Vector3 initialPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

                initialPos.z = 0;
                initialPos.x += blackMap.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                initialPos.y += blackMap.GetComponent<SpriteRenderer>().bounds.size.y / 2;

                blackMap.transform.position = initialPos;

                settings.levelObjects.Add(blackMap);
            }
            else
            {
                GameObject lastMap = settings.levelObjects[(i -= 1)].gameObject;
                GameObject currentMap;

                if (lastMap.CompareTag("White"))
                    currentMap = Instantiate(settings.blackPrefab);
                else
                    currentMap = Instantiate(settings.whitePrefab);

                currentMap.transform.localScale = new Vector3(settings.width, Random.Range(settings.minMapSize, settings.maxMapSize));

                Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

                pos.z = 0;
                pos.x += currentMap.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                pos.y = lastMap.transform.position.y + (lastMap.GetComponent<SpriteRenderer>().bounds.size.y / 2) + (currentMap.GetComponent<SpriteRenderer>().bounds.size.y / 2);

                currentMap.transform.position = pos;

                settings.levelObjects.Add(currentMap);

                i++;
            }
        }
    }

    public void CreateConnectLevel()
    {
        for (int i = 0; i < settings.nextLevelSize; i++)
        {
            GameObject lastMap = settings.levelObjects[(settings.levelObjects.Count - 1)].gameObject;
            GameObject currentMap;

            if (lastMap.CompareTag("White"))
                currentMap = Instantiate(settings.blackPrefab);
            else
                currentMap = Instantiate(settings.whitePrefab);

            currentMap.transform.localScale = new Vector3(settings.width, Random.Range(settings.minMapSize, settings.maxMapSize));

            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

            pos.z = 0;
            pos.x += currentMap.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            pos.y = lastMap.transform.position.y + (lastMap.GetComponent<SpriteRenderer>().bounds.size.y / 2) + (currentMap.GetComponent<SpriteRenderer>().bounds.size.y / 2);

            currentMap.transform.position = pos;

            settings.levelObjects.Add(currentMap);
            i++;
        }
    }

    public void InstantiatePlayer()
    {
        settings.currentPlayerObj = Instantiate(prefabsConfig.playerPrefab, Vector3.zero, Quaternion.identity);
        prefabsConfig.btnPlayerSwitch.onClick.AddListener(delegate { settings.currentPlayerObj.GetComponent<PlayerController>().Switch(); });
        prefabsConfig.googlePlay.GetComponent<GooglePlayController>().UnlockAchievement("CgkImPGFh-ocEAIQAg");
    }

    public void RetryRandom()
    {
        if (settings.currentPlayerObj != null)
        {
            prefabsConfig.googlePlay.GetComponent<GooglePlayController>().SetScore("CgkImPGFh-ocEAIQAQ", (int)settings.currentPlayerObj.GetComponent<PlayerController>().points);
            Destroy(settings.currentPlayerObj);
        }

        if (settings.levelObjects.Count != 0)
        {
            foreach (GameObject obj in settings.levelObjects)
            {
                Destroy(obj);
            }
            
            settings.levelObjects.Clear();
        }

        prefabsConfig.btnPlayerSwitch.onClick.RemoveAllListeners();       

        CreateNewLevel();
        InstantiatePlayer();       
    }

    public void CleanScene()
    {
        if (settings.currentPlayerObj != null)
        {
            prefabsConfig.googlePlay.GetComponent<GooglePlayController>().SetScore("CgkImPGFh-ocEAIQAQ", (int)settings.currentPlayerObj.GetComponent<PlayerController>().points);
            Destroy(settings.currentPlayerObj);
        }

        if (settings.levelObjects.Count != 0)
        {
            foreach (GameObject obj in settings.levelObjects)
            {
                Destroy(obj);
            }

            settings.levelObjects.Clear();
        }

        prefabsConfig.btnPlayerSwitch.onClick.RemoveAllListeners();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
