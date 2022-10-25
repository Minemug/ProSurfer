using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tut : MonoBehaviour
{
    public Text Bh;
    public Text Nav;
    public Text Reset;
    private bool isStarttriggered = false; 
    private bool isjumptriggered = false; 
    private bool isbhtriggered = false;
    private List<Collider> triggers = new List<Collider>();
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {

        if (!triggers.Contains(other))
            triggers.Add(other);
        if (other.gameObject.tag == "Tut_start" && isStarttriggered==false)
        {
            Nav.text = "Hello!";
            Bh.text = "In this tutorial" + " you will learn basics in ProSurfer. \nTo move use WSAD keys and to rotate the camera use mouse";
            isStarttriggered = true;
        }
        if (other.gameObject.tag == "Tut_jump" && isjumptriggered==false)
        {
            Nav.text = "Jump";
            Bh.text = "To jump press space bar";
            isjumptriggered = true;
        }
        if (other.gameObject.tag == "Tut_bh" && isbhtriggered==false)
        {
            Nav.text = "Bunnyhop";
            Bh.text = "Sometimes single jump won't be enough. \nTo jump over next gap you need to gain speed. " +
                      "To do this just click W and hold SPACEBAR. After that release W button and gain speed by looking around and pressing side movement buttons. " +
                      "To increase your speed you need to hold A or D and move mouse in the same direction.\nTry to reach 200 speed units and jump to next platform.";
            isbhtriggered = true;
        }
        if (other.gameObject.tag == "Tut_surf")
        {
            Nav.text = "Surf";
            Bh.text = "Nice, you did it! \n" +
                      "Now all you need to do is surf to finish. To do that just simple gain speed and jump over surf ramp. " +
                      "Then you need to hold opposite movement button to side where you are. \n" +
                      "For example if you want to go on right side of ramp you need to hold A to surf.\n" +
                      "Good luck";
        }
            

    }

    private void Update()
    {
        if (player.transform.position.y < -100)
        {
            Reset.text = "To restart level press R";
        }

        if (Input.GetKeyDown(KeyCode.R) || player.transform.position.y < -300)
        {
            Reset.text = " ";
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
