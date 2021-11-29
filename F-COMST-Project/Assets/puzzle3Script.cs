using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle3Script : MonoBehaviour
{

    CeasarBust[] ceasarBustList;
    
    public void CheckMasks()
    {
        foreach(CeasarBust cb in ceasarBustList)
        {
            if (cb.IsMaskOn())
            {
                if (cb.CheckMask())
                {
                    Debug.Log("Correct Mask");
                }
                else
                {
                    Debug.Log("Not all mask where correct");
                    return;
                }
            }
            else
            {
                Debug.Log("Not All bust have a mask");
                return;
            }
        }
        Debug.Log("All masks where correct");
    }

    // Start is called before the first frame update
    void Start()
    {
        ceasarBustList = GetComponentsInChildren<CeasarBust>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
