using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;

    public int GetDamage() //getter method to retrieve the current damage value
    {
        return damage;
    }

    public void Hit() //destroys the current game object
    {
        //gameObject is a keyword used to refer the current object in which this script is in
        Destroy(gameObject);
    }

}
