using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController: MonoBehaviour
{
    public GameObject pauseMenu;
    public float Score = 0;
    public Text scoreText;



    public void Update()
    {
       // scoreText.text = Score.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void PlayGame()
    { 
        SceneManager.LoadScene(1);
    } 

    public void QuitGame()
    {
        Application.Quit();
    }

    // public void Settings()
    // {
    //     SceneManager.LoadScene(1);
    // }

    public void Pause()
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0f; 
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; 
    }

    public void Home(int sceneID)
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(sceneID); 
    }    

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);

        Time.timeScale = 1f;
    } 


    public void UpdateScore()
    {
        Score++;
        scoreText.text = Score.ToString();

    }

    // public void LoadLevel()
    // {
    //     StartCoroutine(LoadAsynchronously());
    // }

    // IEnumerator LoadAsynchronously()
    // {
    //     AsyncOperation operation = SceneManager.LoadSceneAsync(levelSelection.selectedButton.GetComponentInChildren<Text>().text);

    //     loadingScreen.SetActive(true);

    //     while (!operation.isDone)
    //     {
    //         float progress = Mathf.Clamp01(operation.progress / .9f);

    //         slider.value = progress;
    //         slider1.value = progress;

    //         if (progress > 0.9f)
    //         {
    //             explosionImage.SetActive(true);
    //         }

    //         yield return null; 
    //     }
    // }
}

