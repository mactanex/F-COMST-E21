using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    public int maskNumber;
    public TooltipSystem tooltip;
    Vector3 originalPosition;
    Vector3 minRespawnPoint;
    Vector3 maxRespawnPoint;

    public int GetMaskNumber()
    {
        return maskNumber;
    }

    void Start()
    {
        originalPosition = transform.position;
        minRespawnPoint = new Vector3(-1, -1, -1);
        maxRespawnPoint = new Vector3(1, 1, 1);
    }

    void Update()
    {
        if(transform.position.x > minRespawnPoint.x && transform.position.x < maxRespawnPoint.x &&
           transform.position.y > minRespawnPoint.y && transform.position.y < maxRespawnPoint.y &&
           transform.position.z > minRespawnPoint.z && transform.position.z < maxRespawnPoint.z)
        {
            transform.rotation = Quaternion.identity;
            transform.position = originalPosition;
            tooltip.SetTooltipWithTimer(2f, "Ups something went wrong");
        }
    }
}
