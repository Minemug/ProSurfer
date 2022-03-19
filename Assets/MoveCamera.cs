using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public GameObject player;
    //public float sensitivity;

    //void Update()
    //{
    //    float rotateHorizontal = Input.GetAxis("Mouse X");
    //    float rotateVertical = Input.GetAxis("Mouse Y");
    //    //transform.RotateAround(player.transform.position, -Vector3.up, rotateHorizontal * sensitivity); 
    //    player.transform.Rotate(transform.up * rotateHorizontal * sensitivity * Time.deltaTime); //instead if you dont want the camera to rotate around the player
    //    //transform.RotateAround(Vector3.zero, transform.right, rotateVertical * sensitivity);

    //    player.transform.Rotate(-transform.right * rotateVertical * sensitivity * Time.deltaTime); //if you don't want the camera to rotate around the player
    //    //transform.
    //}
    public float HorizontalSens;
    public float VerticalSens;
    //float horizontalSpeed = 2.0f;
    //float verticalSpeed = 2.0f;

    void Update()
    {
        // Get the mouse delta. This is not in the range -1...1
        float h = HorizontalSens * Input.GetAxis("Mouse X");
        float v = -VerticalSens * Input.GetAxis("Mouse Y");

        player.transform.Rotate(v, 0, 0);
        player.transform.Rotate(0, h, 0);
    }
}
