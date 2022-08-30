using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObj : MonoBehaviour
{
    public bool canChoose = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        canChoose = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        canChoose = false;
    }
}
