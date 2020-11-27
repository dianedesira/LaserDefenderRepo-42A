using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{

    /* OnTriggerEnter2D() is a built-in method (so the Unity Compiler will know when this 
     * method needs to be called which is when another collider hits this object's collider).
     * We choose to use OnTriggerEnter2D() instead of OnCollisionEnter2D() because we have a
     * trigger collider (basically since we ticked the is trigger property).
     * 
     * IMP - Remember we need to use the proper spelling and casing to define the built-in
     * method's name.
     * 
     * This method has a collision parameter which gives us all the information about the 
     * collision which has just occurred - includin the other object which hit the current
     * object.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy() is a method which removes the object, passed as a parameter, from the 
        //hierarchy and from memory.
        Destroy(collision.gameObject);
    }

    /* This collision method is used when the current object's collider is not a trigger
     * collider (the default collider).
     * 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    */
}
