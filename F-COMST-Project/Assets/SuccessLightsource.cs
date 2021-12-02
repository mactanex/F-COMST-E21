using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessLightsource : MonoBehaviour
{
    Light source; 
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<Light>();
        source.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void changecolorsuccess()
    {
        if (source.color == Color.red)
        {
            source.color = Color.green;
        }
        else
        {
            source.color = Color.red;
        }
    }
}
