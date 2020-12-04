using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.02f; // the speed for the scrolling
    Material myMaterial; //the material from the texture
    Vector2 offSet; //the movement for the scrolling


    // Start is called before the first frame update
    void Start()
    {
        //retrieving the material property from the Renderer component which is found in this
        // object - the Background object
        myMaterial = GetComponent<Renderer>().material;
        //the scrolling will be done vertically, thus there shouldn't be an offset (change in
        //position) on the x axis but on the y axis.
        offSet = new Vector2(0f, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //moving the background via the given offset every frame but making the movement
        //frame independent
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
