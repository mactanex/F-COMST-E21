using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle3Script : MonoBehaviour
{
    private bool isFinished = false;
    CeasarBust[] ceasarBustList;
    
    public void CheckMasks()
    {
        if (!isFinished)
        {
            foreach (CeasarBust cb in ceasarBustList)
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
            isFinished = true;
            GameManager.FinishPuzzleStatic(3);
        }
        
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
