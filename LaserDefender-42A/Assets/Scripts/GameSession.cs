using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score;

    private void Awake()
    {
        SetUpSingleton();
    }

    void SetUpSingleton()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue; // score = score + scoreValue
    }

    /* We have the Play Again option but the score cannot be accessed from outside the script and other scripts
     * cannot reduce the value of the score. So when the game should be played again, this current game session
     * will be destroyed so that the Singleton principle will allow for a new one (with reset values) to be 
     * regenerated and thus, score will reset to 0.
     */
    public void ResetGame()
    {
        Destroy(gameObject);
    }

}
