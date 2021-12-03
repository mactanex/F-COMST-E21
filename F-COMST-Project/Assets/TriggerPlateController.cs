using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class TriggerPlateController : MonoBehaviour
{
    Animator plateAnimator;
    AudioSource audioSource;

    private float totalmass = 0;
    public float desiredWeight = 12.5f;
    private bool isFinished = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!isFinished)
        {
            if (other.gameObject.tag == "puzzle2")
            {
                totalmass += other.gameObject.GetComponent<Rigidbody>().mass;
                if (totalmass == desiredWeight)
                {
                    isFinished = true;
                    audioSource.Play();
                    GameManager.FinishPuzzleStatic(2);
                    plateAnimator.SetBool("offPlate", false);
                    plateAnimator.SetBool("onPlate", true);
                }
                Debug.Log("cube entered trigger");
                Debug.Log(totalmass);
            }
        }
       

    }

    private void OnTriggerExit(Collider other)
    {
        if (!isFinished)
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
    }

    // Start is called before the first frame update
    void Start()
    {
        plateAnimator = this.transform.parent.GetComponent<Animator>();
        audioSource = this.transform.parent.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}


