using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float laserDelay = 0.3f;

    [SerializeField] GameObject laserPrefab;

    [SerializeField] AudioClip playerDeathSound; //zapthreetonedown
    [SerializeField][Range(0,1)] float playerDeathSoundVolume = 0.75f; 
    [SerializeField] AudioClip shootSound; //laser3
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.1f; 
        
    Coroutine fireCoroutine;

    // boundary coordinates
    float xMin;
    float xMax;
    float yMin;
    float yMax;


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

       // StartCoroutine(PrintAndWait());
    }

    // Update is called once per frame
    void Update()
    {
        //print("The Update Method has been called!");

        Move();
        Fire();
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
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

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
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    void Fire()
    {
        /* GetButtonDown returns true as soon as the user presses down on the key which
         * is represented by the given button.
         */

        if (Input.GetButtonDown("Fire1")) // if(Input.GetButtonDown("Fire1") == true)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator PrintAndWait()
    {
        print("Message 1 sent!");

        yield return new WaitForSeconds(3f);

        print("Message 2 sent!");
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            /* Instantiate generates a clone (a copy) of the object which is passed as
             * the first parameter. We need to indicate where the clone will be placed
             * in the scene and in this case, the laser should be placed in the ship's
             * position thus, transform.position.
             * Quaternion.identity refers to NO Rotation.
             * 
             * The Instantiate method also returns a reference to the object clone which has just been
             * generated.
             */

            GameObject laserClone = Instantiate(laserPrefab, transform.position, Quaternion.identity);

            laserClone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);

            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);

            yield return new WaitForSeconds(laserDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        /* To avoid from having runtime errors due to trying to use elements from the damage
         * dealer if the other object does not have a DamageDealer component, we are checking
         * to see whether the damageDealer variable is null/empty. If it is, the process will
         * return and the method execution will terminate.
         * The ! (Not) operator is used in boolean expressions to check for a false value, if the
         * value is not a boolean type, it automatically checks if the value is null/empty.
         * A return always indicates the end of the method execution. If there is any code 
         * following the return, this code will not be executed.
         */
        if (!damageDealer)
            return;

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        //health = health - damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);

        Destroy(gameObject);
    }
}
