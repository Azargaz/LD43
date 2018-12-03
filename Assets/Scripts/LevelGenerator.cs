using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public bool tutorial = false;
    public GameObject tutorialHelp;

    [System.Serializable]
    public class Level
    {
        public string name;
        public int numberOfStartingSkeletons;
        public int sacrificesNeeded;
        public Texture2D map;
    }

    public Level[] levels;
    public static int currentLevelIndex = 0;
    public static Level currentLevel;
    static int maxLevel;

    public Transform cameraTransform;
    public ColorToPrefab[] colorMappings;

    List<MechanismColors> mechanismsWithColors = new List<MechanismColors>();
    public class MechanismColors
    {
        public Color color;
        public List<GameObject> mechanisms = new List<GameObject>();
        public HashSet<Vector2> postions = new HashSet<Vector2>();
    }

    void Awake()
    {
        maxLevel = levels.Length-1;
        currentLevelIndex = PlayerPrefs.GetInt("LastLevel");
        currentLevel = levels[currentLevelIndex];
        cameraTransform.position = new Vector3(Mathf.FloorToInt(currentLevel.map.width / 2), Mathf.FloorToInt(currentLevel.map.height / 4), -15);

        GenerateLevel(currentLevel.map, Vector2.zero);

        SetupMechanisms();
        mechanismsWithColors.Clear();

        PlayerPrefs.SetInt("LastLevel", currentLevelIndex);

        tutorial = currentLevelIndex == 0;
        tutorialHelp.SetActive(tutorial);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {            
            SceneManager.LoadScene(0);
        }
    }

    void GenerateLevel(Texture2D map, Vector2 offset)
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = map.height / 2; y < map.height; y++)
            {
                Color pixelColor = map.GetPixel(x, y);
                if (pixelColor.a != 0)
                {
                    GenerateMechanismColors(x, y - map.height / 2, pixelColor);
                }
            }
        }

        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height / 2; y++)
            {
                Color pixelColor = map.GetPixel(x, y);
                GenerateTile(x, y, pixelColor, offset);
            }
        }
    }

    void GenerateTile(int x, int y, Color pixelColor, Vector2 offset)
    {
        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y) + offset;
                GameObject spawnedPrefab = Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
                AddMechanism(x, y, spawnedPrefab);
            }
        }
    }

    void GenerateMechanismColors(int x, int y, Color pixelColor)
    {
        if (!mechanismsWithColors.Exists(element => element.color == pixelColor))
        {
            MechanismColors mechanismWithColor = new MechanismColors()
            {
                color = pixelColor
            };
            mechanismWithColor.postions.Add(new Vector2(x, y));
            mechanismsWithColors.Add(mechanismWithColor);
        }
        else
        {
            foreach (MechanismColors mechColor in mechanismsWithColors)
            {
                if (mechColor.color == pixelColor)
                {
                    mechColor.postions.Add(new Vector2(x, y));
                }
            }
        }
    }

    void AddMechanism(int x, int y, GameObject mechanism)
    {
        foreach (MechanismColors mech in mechanismsWithColors)
        {
            if (mech.postions.Contains(new Vector2(x, y)))
            {
                mech.mechanisms.Add(mechanism);
            }
        }
    }

    void SetupMechanisms()
    {
        foreach (MechanismColors mechColors in mechanismsWithColors)
        {
            List<Mechanism> mechanisms = new List<Mechanism>();

            foreach (GameObject mech in mechColors.mechanisms)
            {
                Mechanism mechanism = mech.GetComponent<Mechanism>();

                if (mechanism != null)
                {
                    mechanisms.Add(mechanism);
                }
            }

            foreach (GameObject mech in mechColors.mechanisms)
            {
                MechanismController controller = mech.GetComponent<MechanismController>();

                if (controller != null)
                {
                    controller.mechanisms.AddRange(mechanisms);
                }
            }
        }
    }

    public static bool NextLevel()
    {
        if(currentLevelIndex < maxLevel)
        {
            currentLevelIndex++;
            PlayerPrefs.SetInt("LastLevel", currentLevelIndex);
            SetLastLevelUnlocked(currentLevelIndex);
            return true;
        }

        PlayerPrefs.SetInt("LastLevel", currentLevelIndex);
        SetLastLevelUnlocked(currentLevelIndex);
        return false;
    }

    static void SetLastLevelUnlocked(int i)
    {
        if(i > PlayerPrefs.GetInt("LastLevelUnlocked"))
            PlayerPrefs.SetInt("LastLevelUnlocked", i);
    }
}