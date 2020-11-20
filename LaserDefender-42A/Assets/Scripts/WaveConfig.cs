using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Usual scripts are used to control game objects and they are of type MonoBehaviour. This script
 * is generated so as to create a collection of data/variables which will be required by all
 * enemy prefabs/copies. Without creating this script (of type ScriptableObject), the same set
 * of variables will be duplicated for EACH prefab and thus, using up extra memory. Instead, one
 * copy of the collection of data will be stored and it can be accessed by all of the prefabs.
 */

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    //the enemy
    [SerializeField] GameObject enemyPrefab;

    //the path on which to go
    [SerializeField] GameObject pathPrefab;

    //time between each spawn
    [SerializeField] float timeBetweenSpawns = 0.5f;

    //include this random difference between spwans
    [SerializeField] float spawnRandomFactor = 0.3f;

    //number of enemies in the wave
    [SerializeField] int numberOfEnemies = 5;

    //enemy movement speed
    [SerializeField] float enemyMoveSpeed = 2f;

    /* (an element is items created in a class so variable, property or method)
     * The variables above are all set to private since unless we specify the element's access
     * modifier, by default they will be private since this is the most secure access modifier.
     * A private element would mean that the element can ONLY be accessed from the same class in
     * which it was created in.
     * The above variables will be left as private so that no other class can change their value
     * by mistake. The variables are going to be set as Read Only since the below methods will
     * return the variables' values and thus, can ONLY be read from the script in which the method
     * will be called.
     */
    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }
    /*public GameObject GetPathPrefab()
    {
        return pathPrefab;
    }*/

    public List<Transform> GetWaypoints()
    {
        //creating an empty list to place each waypoint from the pathprefab inside this list
        List<Transform> waypoints = new List<Transform>();

        //Syntax for a Foreach is foreach item (indicate type of item and give it a temp name) in collection
        foreach (Transform child in pathPrefab.transform)
        {
            /* Go through all of the pathprefabs children (which are the waypoints for the path) and place
             * each one in the waypoints list.
             */
            waypoints.Add(child);
        }

        return waypoints;
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }
    public float GetSpawnRandomFactor()
    {
        return spawnRandomFactor;
    }
    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }
    public float GetEnemyMoveSpeed()
    {
        return enemyMoveSpeed;
    }
}
