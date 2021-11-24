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
            if (this.transform.parent.childCount == 1)
            {
                Debug.Log("Mask is touching trigger");
                grabSystem.DropPuzzleItem(other.gameObject.transform.GetComponent<PickablePuzzleItem>());
                other.gameObject.transform.SetParent(this.transform.parent, true);
                other.gameObject.transform.rotation = transform.parent.rotation;
                body = other.gameObject.GetComponent<Rigidbody>();
                body.freezeRotation = true;
                iTween.MoveTo(other.gameObject, iTween.Hash("position", new Vector3(0.0f, 0.5f, 0.55f), "islocal", true, "time", 2f, "easetype", iTween.EaseType.linear));
                body.velocity = Vector3.zero;
                other.attachedRigidbody.useGravity = false;
                if (parentCeasarBust.checkMask())
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
            body = other.gameObject.GetComponent<Rigidbody>();
            body.freezeRotation = false;
            other.gameObject.transform.SetParent(null);
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
