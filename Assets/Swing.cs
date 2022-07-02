using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && anim.gameObject.activeSelf)
            anim.SetTrigger("LMB");
        if(Input.GetMouseButtonDown(1) && anim.gameObject.activeSelf)
            anim.SetTrigger("RMB");
    }
}
