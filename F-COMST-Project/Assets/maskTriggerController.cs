using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maskTriggerController : MonoBehaviour
{

    public GrabSystemClass grabSystem;
    Rigidbody body;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "puzzle3")
        {
            Debug.Log("mask is touching trigger");
            grabSystem.DropPuzzleItem(other.gameObject.transform.GetComponent<PickablePuzzleItem>());
            other.gameObject.transform.SetParent(this.transform.parent, true);
            body = other.gameObject.GetComponent<Rigidbody>();
            other.gameObject.transform.localRotation = Quaternion.identity;
            body.freezeRotation = true;
            iTween.MoveTo(other.gameObject, iTween.Hash("position", new Vector3(0.7f, 0.27f, 0), "islocal", true, "time", 2f, "easetype", iTween.EaseType.linear));
            body.velocity = Vector3.zero;
            other.attachedRigidbody.useGravity = false;
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
