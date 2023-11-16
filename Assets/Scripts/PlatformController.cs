using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    PlatformEffector2D pe2d;

    public bool leftPlatform;
    // Start is called before the first frame update
    void Start()
    {
        pe2d= GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("down") || Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Down"); // Tryng to see if it detects the key so it lets me fall through the platforms
        }// lets the player traspass the platform downwards
        if (Input.GetKeyDown("down") && !leftPlatform) 
        {
            Debug.Log("Down");
            pe2d.rotationalOffset = 180;
            leftPlatform = true;

            gameObject.layer = 2; 
        }
    }
    //lets the player traspass the platform upwards
    private void OnCollisionExit2D(Collision2D collision)
    {
        pe2d.rotationalOffset=0;
        leftPlatform = false;

        gameObject.layer = 6;
    }
}
