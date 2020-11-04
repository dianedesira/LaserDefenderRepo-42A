using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;

    // boundary coordinates
    float xMin;
    float xMax;


    /* In Unity there are two types of methods; Built-in and User Defined
     * Built-in: These methods have their names already set up by Unity since the Unity compiler will know
     * when the call the method during game execution. Thus, we just need to provide the method definition.
     * It is important to use the exact name for the built-in method during method definition (proper spelling
     * + proper casing)
     * 
     * User Defined: These methods are invented by the developer for proper organisation of code. Since the 
     * Unity compiler doesn't know about these methods, we need to make sure that the method is called at the
     * proper time.
     */


    // Start is called before the first frame update
    void Start() // built-in method and this is a method definition (explaining what the method will do)
    {
        //print("The Start Method has been called!"); //method call
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        //print("The Update Method has been called!");

        Move();
    }

    // Move is a User-Defined method and it will be used to control the player ship's movements
    void Move()
    {
        /* Format for calling a method is:
         * Namespace/Project.Class.MethodName()
         */

        //GetAxis returns a -ve or +ve value depending on which button on the keyboard the user presses
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        /*
         * Since the Move() method is being called in the Update which executes once
         * per frame, the player's movement is in general frame dependant since the
         * higher the frame rate (of the PC) the faster the movement. This would
         * cause different speeds on different PCs.
         * To make the movement frame independant we are using the deltaTime 
         * property to fetch the time taken for the frame to execute and thus,
         * cancelling out the extra distance by having a high frame rate.
         */


        /* To access properties for THIS object the format is:
         * componentName.propertyName
         * 
         * To access properties for another object the format is:
         * object.componentName.propertyName
        */

        // the current x position (for the player) is changed with the slight change of deltaX EVERY frame
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        // Mathf.Clamp restricts the new x coorindate to be within the xMin and xMax values.
        var newYPos = transform.position.y + deltaY;

        // changing the player ships' position
        transform.position = new Vector3(newXPos, newYPos, transform.position.z);
    }

    void SetUpMoveBoundaries()
    {
        float padding = 0.5f;

        /*
         * We're going to set the boundaries using ViewportToWorldPoint so that the
         * coordinates of the boundaries are not dependant on the camera size but
         * they are always calculated at run time depending on the current camera
         * size.
         */

        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    }
}
