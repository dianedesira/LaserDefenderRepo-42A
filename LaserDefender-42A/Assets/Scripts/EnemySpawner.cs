﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;

    int startingWave = 0;

    // Start is called before the first frame update
    IEnumerator Start() //The Start built-in method has been set to become a coroutine.
    {
        do
        {
            /*  via yield return StartCoroutine we ensure that the repetition of creation of waves is done
             *  after the previous group of waves are generated, thus, ensuring a synchronous execution.
             */
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* To generate multiple enemies within each group, a coroutine needs to be used to have a 
     * short delay between the creation of one enemy and another. Without the use of coroutines,
     * we would pause the whole (or most of the) application during that delay.
     * 
     * Parameters are used as data inputs given to a method since the particular data will be
     * different from one method call to another. Normally, and the most efficient use of parameters,
     * parameters should only be used when there is no other option.
     * In this example, the parameter represents the different waves so that the group of enemies
     * are spawned for each wave, thus, with each method call, the WaveConfig will be different.
     * (This is just an example for using parameters since the current wave can actually be accessed
     * from the wavecongfigs list)
     */
    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        // keep on repeating the creation of enemy clones until the number of enemies for this wave has
        // been reached
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            /* For the Instantiate method, the second parameter refers to the position where the clone
             * should be placed. Since the enemies need to follow the path, we should place the enemies
             * on the first point of the path that they should follow. The GetWayPoints method returns
             * the whole list of points for the path but we only need the first point. The first item
             * in a list is at position 0, thus waveConfig.GetWaypoints()[0]
             * Now GetWaypoints returns a list of the whole Transform component (incl. position, rotation
             * and scale). We just need the position property from the transform.
             */
            GameObject newEnemyClone = Instantiate(waveConfig.GetEnemyPrefab(), 
                                                   waveConfig.GetWaypoints()[0].position,
                                                   Quaternion.identity);

            //We needed a reference to the enemy clone which has just been generated so that from the clone
            //we refer to the EnemyPathing component and call the method SetWaveConfig so that ITS enemy
            //pathing can know that it should follow the path specified by the current wave/group.
            newEnemyClone.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    IEnumerator SpawnAllWaves()
    {
        //using a for loop to go through all of the waves found in our list
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            WaveConfig currentWave = waveConfigs[waveIndex];

            /* Calling a StartCoroutine from yield return would ensure synchronous running of the execution
             * of multiple calls for the SpawnAllEnemiesInWave. Without this synchronous concept, all
             * of the groups would be generated together.
             */
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
}
