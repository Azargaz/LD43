using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform cameraTransform;
    public Texture2D[] maps;
    public ColorToPrefab[] colorMappings;

    List<MechanismColors> mechanismsWithColors = new List<MechanismColors>();
    public class MechanismColors
    {
        public Color color;
        public List<GameObject> mechanisms = new List<GameObject>();
        public HashSet<Vector2> postions = new HashSet<Vector2>();
    }

    void Start()
    {
        cameraTransform.position = new Vector3(Mathf.FloorToInt(maps[0].width / 2), Mathf.FloorToInt(maps[0].height / 4), -15);

        Vector2 offset = Vector2.zero;
        foreach (Texture2D map in maps)
        {
            GenerateLevel(map, offset);
            offset += new Vector2(0, map.height);
        }

        SetupMechanisms();
        mechanismsWithColors.Clear();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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
}
