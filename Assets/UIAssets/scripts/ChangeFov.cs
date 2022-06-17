using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFov : MonoBehaviour
{
    public Camera POV;
    // Start is called before the first frame update
    void Start()
    {
        POV.fieldOfView = MainManager.Instance.fov;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
