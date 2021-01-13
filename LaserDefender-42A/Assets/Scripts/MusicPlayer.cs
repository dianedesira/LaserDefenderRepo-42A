using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    /* The Awake() method is a built-in method which is very similar to the Start() built-in method. The Start()
     * method is called wheneve the object has been loaded and initialised (all components properly set up). The
     * Awake() method is called before when the compiler notices that the object is there. Thus, the Awake is
     * used when we need to treat something urgently.
     */
    private void Awake()
    {
        SetUpSingleton();
    }

    /* The Singleton principle is used to ensure that there is only one instance/copy of an object at the same time.
     * Therefore, it needs to check if there is already an instance, so that a new instance is never created. An
     * instance is ONLY created if there is no other instance.
     */
    void SetUpSingleton()
    {
        /* FindObjectsOfType<>() is a method which returns all of the objects, in the current hierarchy, which
         * contain the script indicated in between <>. In this case, all objects which contain the MusicPlayer
         * script. This method returns a list of all of these objects. Thus, we will check how many music players
         * there are in the list. If there is more than one music player ...
         */
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            //.. then destroy the new Music Player so that we are left with the previous, which is already playing
            // the background music
            Destroy(gameObject);
        }
        else // else it would mean that there is only one music player and this shouldn't be destroyed
        {
            /* The DontDestroyOnLoad() method is used to tell the compiler not the destroy the object, passed as a 
             * parameter, whenever a new scene is loaded.
             */
            DontDestroyOnLoad(gameObject);
        }
    }
}
