using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    void GoToNextLevel()
    {
		if(LevelGenerator.NextLevel())
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		else
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);	
		}
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            if (Input.GetButtonDown("Sacrifice"))
            {
				GoToNextLevel();
            }
        }
    }
}