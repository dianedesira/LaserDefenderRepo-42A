using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
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
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        print("Game quitting");
        Application.Quit();
    }
}
