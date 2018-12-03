using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button levelsButton;
    public Button quitButton;

	public Button resetLevelsButton;
	public Button unlockLevelsButton;

    Animator animator;

    [System.Serializable]
    public class Level
    {
        public string name;
        [HideInInspector]
        public int index;
    }
    public Level[] levels;
    public Transform levelsContainer;

    public GameObject levelSelectButton;

    void Start()
    {
        animator = GetComponent<Animator>();

        startButton.onClick.AddListener(StartGame);
        levelsButton.onClick.AddListener(LevelSelect);
        quitButton.onClick.AddListener(Quit);
		resetLevelsButton.onClick.AddListener(ResetLevels);
		unlockLevelsButton.onClick.AddListener(UnlockAllLevels);

        SetupLevelButtons();
    }

	void SetupLevelButtons()
	{
		foreach(Transform child in levelsContainer)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i < levels.Length; i++)
        {
            levels[i].index = i;
            GameObject newLevel = Instantiate(levelSelectButton, levelsContainer);
            Button newLevelButton = newLevel.GetComponent<Button>();
            Text newLevelText = newLevel.GetComponentInChildren<Text>();
            newLevelText.text = levels[i].name;

            int t = i;
            newLevelButton.onClick.AddListener(() => StartLevel(t));

            if (levels[i].index > PlayerPrefs.GetInt("LastLevelUnlocked"))
            {
                newLevelButton.interactable = false;
            }
        }
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            animator.SetBool("showLevelSelect", false);
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void LevelSelect()
    {
        animator.SetBool("showLevelSelect", true);
    }

    void Quit()
    {
        Application.Quit();
    }

    void ResetLevels()
    {
		PlayerPrefs.SetInt("LastLevelUnlocked", 0);
		PlayerPrefs.SetInt("LastLevel", 0);
		SetupLevelButtons();
    }

	void UnlockAllLevels()
	{
		PlayerPrefs.SetInt("LastLevelUnlocked", levels.Length-1);
		SetupLevelButtons();
	}

    void StartLevel(int i)
    {
        if (i <= PlayerPrefs.GetInt("LastLevelUnlocked"))
        {
            PlayerPrefs.SetInt("LastLevel", i);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
