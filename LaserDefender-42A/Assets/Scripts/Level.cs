using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f; // delay before loading the Game over scene

    public void LoadStartMenu()
    {
        /* The LoadScene method is found in the SceneManager class which is found in the SceneManagement library.
         * To use the SceneManagement library we need to import it (via the using keyword).
         * LoadScene loads a specified scene either via the buildIndex or via the scene name.
         * Scene 0 would be the first scene in the build order and the first scene which appears when the 
         * executable is running.
         */
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("LaserDefender");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    public void QuitGame()
    {
        print("Game quitting");
        Application.Quit();
    }

    IEnumerator WaitAndLoad()
    {
        /* This coroutine will be used so that when the Game Over scene needs to be loaded, there will be a delay
         * and once the delay is up the scene can load.
         */

        yield return new WaitForSeconds(delayInSeconds);

        SceneManager.LoadScene("GameOver");
    }
}
