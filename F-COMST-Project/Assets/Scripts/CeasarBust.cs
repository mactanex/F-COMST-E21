using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeasarBust : MonoBehaviour
{
    public int correctMaskNumber;
    Mask childMaskAsses;


    public bool checkMask()
    {
        childMaskAsses = gameObject.GetComponentInChildren<Mask>();
        if (childMaskAsses.maskNumber == correctMaskNumber)
        {
            return true;
        }
        else
            return false; 
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
