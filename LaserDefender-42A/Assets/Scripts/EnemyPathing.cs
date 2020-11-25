using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //reference to the current wave asset (type wave config)
    WaveConfig waveConfig;

    List<Transform> wayPoints;

    int wayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //current waypoints list is filled up by all of the points saved in the pathprefab specified in the
        //current wave asset.
        wayPoints = waveConfig.GetWaypoints();
        //we are setting the current enemy to be placed in the position of the first waypoint
        transform.position = wayPoints[wayPointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        if (wayPointIndex < wayPoints.Count)
        //if(wayPointIndex <= wayPoints.Count - 1)
        {
            /* since the enemy movement method will be called every frame, the movement's speed
             * will be frame dependant. Just like we did for the player, we are using Time.deltaTime
             * to make the speed frame independant.
             */
            var movementThisFrame = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;
            var targetPosition = wayPoints[wayPointIndex].position;

            /* MoveTowards is a method found in the Vector2 class and it basically moves an object
             * from the current position to a target position/destination by returning a new
             * position every frame to slowly move the object to the target step by step and 
             * making it look as if it is moving along a path.
             */

            transform.position = Vector2.MoveTowards(transform.position, targetPosition,
                movementThisFrame);

            if (transform.position == targetPosition)
                wayPointIndex++; // wayPointIndex = wayPointIndex + 1;
        }
        else // if there are no more coordinates left in the list
        {
            /* The gameObject keyword refers to the object which contains the current script.
             * This is important since we are going to have multiple Enemies and we need to one
             * which one to destroy. The one which has moved along ALL the coordinates (so the one
             * that reached this else statement) will be destroyed.
             */
            Destroy(gameObject);
        }
    }

    /* This method was created so that the EnemySpawner script can call it and set the current wave for
     * the enemy clone which has just been generated.
     * We need to use a parameter since the value of the wave will be different with different method calls
     * and the value can only be accessed from outside of the script.
     */
    public void SetWaveConfig(WaveConfig waveConfigToSet)
    {
        waveConfig = waveConfigToSet;
    }
}
