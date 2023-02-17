using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Fragsurf.Movement;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;
public class tut : MonoBehaviour
{
    public Text Bh;
    public Text Nav;
    public Text Reset;
    public Image a;
    public Image w;
    public Image s;
    public Image d;
    public Image space; 
    private bool isStarttriggered = false; 
    private bool isjumptriggered = false; 
    private bool isbhtriggered = false;
    private List<Collider> triggers = new List<Collider>();
    public GameObject player;
    private bool help = true;
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
                      "To increase your speed you need to hold A or D and move mouse in the same direction.\nTry to reach about 140 speed units and jump to next platform.";
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

        if (Time.fixedTime >300 && help )
        {
            Reset.text = "To see how you should beat this level, press H. To skip it, use space.";
            help = false;
        }
        

            if (Input.GetKeyDown(KeyCode.R) || player.transform.position.y < -300 || Input.GetKeyDown(KeyCode.H))
        {
            Reset.text = " ";
        }
        if (Input.GetKeyDown(KeyCode.A))
            a.color= new Color32(0,255,0,255);
        if(Input.GetKeyUp(KeyCode.A))
            a.color= new Color32(0,0,0,29);
        if (Input.GetKeyDown(KeyCode.S))
            s.color= new Color32(0,255,0,255);
        if(Input.GetKeyUp(KeyCode.S))
            s.color= new Color32(0,0,0,29);
        if (Input.GetKeyDown(KeyCode.Space))
            space.color= new Color32(0,255,0,255);
        if(Input.GetKeyUp(KeyCode.Space))
            space.color= new Color32(0,0,0,29);
        if (Input.GetKeyDown(KeyCode.W))
            w.color= new Color32(0,255,0,255);
        if(Input.GetKeyUp(KeyCode.W))
            w.color= new Color32(0,0,0,29);
        if (Input.GetKeyDown(KeyCode.D))
            d.color= new Color32(0,255,0,255);
        if(Input.GetKeyUp(KeyCode.D))
            d.color= new Color32(0,0,0,29);
    }


   
}
