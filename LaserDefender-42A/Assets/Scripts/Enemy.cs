using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{  
   [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 50;

   [Header("Shooting")]
    [SerializeField] float shotCounter; // a counter to set a time between shooting one laser and another
    [SerializeField] float minTimeBetweenShots = 0.2f; // shortest time between laser shots
    [SerializeField] float maxTimeBetweenShots = 3f; // longest time between laser shots
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float laserSpeed = 20f;

   [Header("Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip enemyDeathSound;
    [SerializeField] [Range(0, 1)] float enemyDeathSoundVolume = 0.75f; // used to create a GUI tool to drag the
    //required value
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        /* The Range method generates a random number between the min range and max range passed as 
         * parameters. If you're working with floats, both of the ranges can be generated as random numbers.
         * Though, if we're using integers, the maximum number range will not be included as a possible
         * random number.
         */
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer)
            return;

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        //health = health - damageDealer.GetDamage();
        damageDealer.Hit(); // destroying the laser which has just hit this object
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);

        AudioSource.PlayClipAtPoint(enemyDeathSound, Camera.main.transform.position, enemyDeathSoundVolume);

        //creating a copy of the blast effect
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);

        //destroying the blast after 1 second
        Destroy(explosion, 1f);

        Destroy(gameObject);
    }

    void CountDownAndShoot()
    {
        /* The shotCounter needs to be ticked down with the time that is passing in the game. Time.deltaTime
         * retrieves the time that has passed since the last frame. Since this method will be called in
         * the Update, we will be ticking down the actual game time frame by frame.
         */
        shotCounter -= Time.deltaTime; // shotCounter = shoutCounter - Time.deltaTime;
        // A -= B; => A = A - B;

        if (shotCounter <= 0)
        {
            // shoot the laser
            EnemyFire();

            // reset the counter by regenerating a new random time
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    void EnemyFire()
    {
        GameObject enemyLaserClone = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity);

        //since the enemylaser needs to be shot downwards a negative velocity needs to be applied.
        enemyLaserClone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);

        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }
}
