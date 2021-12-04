using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maskTriggerController : MonoBehaviour
{

    public GrabSystemClass grabSystem;
    Rigidbody body;
    CeasarBust parentCeasarBust;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "puzzle3")
        {
            Debug.Log("Mask is touching trigger");
            if (this.transform.parent.childCount == 1)
            {
                Debug.Log("There is no mask on bust");
                grabSystem.DropPuzzleItem(other.gameObject.transform.GetComponent<PickablePuzzleItem>());//Make player loose grip of item
                other.gameObject.transform.SetParent(this.transform.parent, true);//Make mask child of bust
                other.gameObject.transform.rotation = transform.parent.rotation;//make the mask face the same watas the parent
                body = other.gameObject.GetComponent<Rigidbody>();
                body.constraints = RigidbodyConstraints.FreezeRotation;//freeze the rotation of the mask
                iTween.MoveTo(other.gameObject, iTween.Hash("position", new Vector3(0.0f, 0.5f, 0.55f), "islocal", true, "time", 2f, "easetype", iTween.EaseType.linear));//make the mask move to wanted location on bust 
                body.velocity = Vector3.zero;//kill the velocity on mask so it does not keep moving
                other.attachedRigidbody.useGravity = false;//remove gravity to make mask stay and not fall
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; //stop maks from being able to move 
                parentCeasarBust.PutMaskOn();
                if (parentCeasarBust.CheckMask())// check if the mask is correct
                {
                    Debug.Log("Correct mask");
                }
                else
                    Debug.Log("Wrong mask");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "puzzle3")
        {
            parentCeasarBust.TakeMaskOff();
            body = other.gameObject.GetComponent<Rigidbody>();
            body.freezeRotation = false;//Free rotation of mask 
            other.gameObject.transform.SetParent(null);//remove as child of bust 
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        parentCeasarBust = transform.parent.GetComponent<CeasarBust>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
