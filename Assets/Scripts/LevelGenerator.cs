using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D[] maps;
    public ColorToPrefab[] colorMappings;

    public Transform cameraTransform;

    public List<MechanismColors> mechanismsWithColors = new List<MechanismColors>();
    public class MechanismColors
    {
        public Color color;
        public List<GameObject> mechanisms = new List<GameObject>();
    }

    void Start()
    {
        cameraTransform.position = new Vector3(Mathf.FloorToInt(maps[0].width / 2), Mathf.FloorToInt(maps[0].height / 2), -15);

        Vector2 offset = Vector2.zero;
        foreach (Texture2D map in maps)
        {
            GenerateLevel(map, offset);
            offset += new Vector2(0, map.height);
        }
    }

    void GenerateLevel(Texture2D map, Vector2 offset)
    {
        for (int x = map.width / 2 + 1; x < map.width; x++)
        {
            for (int y = map.height / 2 + 1; y < map.height; y++)
            {
                Color pixelColor = map.GetPixel(x, y);
                GenerateMechanism(x, y, pixelColor);
            }
        }

        for (int x = 0; x < map.width / 2; x++)
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
				AddMechanism(pixelColor, spawnedPrefab);
            }
        }
    }

    void GenerateMechanism(int x, int y, Color pixelColor)
    {
        if (!mechanismsWithColors.Exists(element => element.color == pixelColor))
		{
			MechanismColors mechanismWithColor = new MechanismColors()
			{
				color = pixelColor
			};
			mechanismsWithColors.Add(mechanismWithColor);
		}
    }

	void AddMechanism(Color pixelColor, GameObject mechanism)
	{
		foreach(MechanismColors mech in mechanismsWithColors)
		{
			if(mech.color == pixelColor)
			{
				mech.mechanisms.Add(mechanism);
			}
		}
	}

    void CreateMechanisms()
    {
        foreach (MechanismColors mechColors in mechanismsWithColors)
        {
            foreach (GameObject mech in mechColors.mechanisms)
            {
                MechanismController controller = mech.GetComponent<MechanismController>();
                Mechanism mechanism = mech.GetComponent<Mechanism>();

                if(controller != null)
                {
                    // add each mechanism to this controller
                }
                else if(mechanism != null)
                {
                    // ???
                }
            }
        }
    }
}
