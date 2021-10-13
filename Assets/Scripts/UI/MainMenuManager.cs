using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject notConnected, cantPlayLine, thinkGear;

    IEnumerator LoadGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
            yield return null;
        SceneManager.MoveGameObjectToScene(thinkGear, SceneManager.GetSceneByName("Game"));
        SceneManager.UnloadSceneAsync(currentScene); 
    }

    public void PlayGame()
    {
        if (notConnected.activeSelf)
            cantPlayLine.SetActive(true);
        else
        {
            Debug.Log("hey");
            StartCoroutine(LoadGame());
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
