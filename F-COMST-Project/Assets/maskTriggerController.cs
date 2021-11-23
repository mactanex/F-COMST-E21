using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maskTriggerController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "puzzle3")
        {
            Debug.Log("mask is touching trigger");
            other.gameObject.transform.SetParent(this.transform.parent, true);
            other.attachedRigidbody.useGravity = false;
            other.gameObject.transform.localPosition = new Vector3(1, 0, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "puzzle3")
        {
            other.gameObject.transform.SetParent(null);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
