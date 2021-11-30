using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    public int maskNumber;
    Vector3 originalPosition;

    public int GetMaskNumber()
    {
        return maskNumber;
    }

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        
    }
}
