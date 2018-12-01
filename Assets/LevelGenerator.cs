using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	public Texture2D[] maps;
	public ColorToPrefab[] colorMappings;

	public Transform cameraTransform;

	void Start () 
	{		
		cameraTransform.position = new Vector3(Mathf.FloorToInt(maps[0].width/2), Mathf.FloorToInt(maps[0].height/2), -15);
		
		Vector2 offset = Vector2.zero;
		foreach (Texture2D map in maps)
		{
			GenerateLevel(map, offset);
			offset += new Vector2(0, map.height);
		}
	}

	void GenerateLevel(Texture2D map, Vector2 offset)
	{
		for (int x = 0; x < map.width; x++)
		{
			for (int y = 0; y < map.height; y++)
			{
				Color pixelColor = map.GetPixel(x, y);
				if(pixelColor.a != 0)
				{					
					GenerateTile(x, y, pixelColor, offset);
				}
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
				Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
			}
		}
	}
}
