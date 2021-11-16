using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlateController : MonoBehaviour
{
    Animator plateAnimator;

    private float totalmass = 0;
    private float desiredWeight = 6;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "puzzle2")
        {
            totalmass += other.gameObject.GetComponent<Rigidbody>().mass;
            if (totalmass == desiredWeight)
            {
                plateAnimator.SetBool("offPlate", false);
                plateAnimator.SetBool("onPlate", true);
            }
            Debug.Log("cube entered trigger");
            Debug.Log(totalmass);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "puzzle2")
        {
            totalmass -= other.gameObject.GetComponent<Rigidbody>().mass;
            if (totalmass != desiredWeight)
            {
                plateAnimator.SetBool("offPlate", true);
                plateAnimator.SetBool("onPlate", false);
            }
            Debug.Log("cube left trigger");
            Debug.Log(totalmass);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        plateAnimator = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}


