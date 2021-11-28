using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeasarBust : MonoBehaviour
{
    public int correctMaskNumber;
    private bool MaskOn;
    Mask childMaskAsses;
    puzzle3Script puzzle3Parent;

    public bool CheckMask()
    {
        childMaskAsses = gameObject.GetComponentInChildren<Mask>();
        if (childMaskAsses.maskNumber == correctMaskNumber)
        {
            return true;
        }
        else
            return false;
    }

    public void PutMaskOn()
    {
        MaskOn = true;
        puzzle3Parent.CheckMasks();
    }

    public void TakeMaskOff()
    {
        MaskOn = false;
    }

    public bool IsMaskOn()
    {
        return MaskOn;
    }


    // Start is called before the first frame update
    void Start()
    {
        puzzle3Parent = transform.parent.GetComponent<puzzle3Script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
