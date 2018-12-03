using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
	public Text levelName;
	
    void Start()
    {
		  levelName.text = LevelGenerator.currentLevel.name;
    }

    void Update()
    {
        for (int i = 0; i < SkeletonController.skeletonsList.Count; i++)
        {
            bool input = Input.GetKeyDown((i+1).ToString());
            if(input)
                SkeletonController.skeletonsList[i].controller.StopUnstop();
        }
    }
}
