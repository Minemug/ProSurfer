using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogFormat("{0} collision enter: {1}", this, collision.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
