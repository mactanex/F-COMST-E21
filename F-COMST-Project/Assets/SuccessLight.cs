using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessLight : MonoBehaviour
{
    Material redmaterial;
    Material greenmaterial;
    MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        // redmaterial = Resources.Load<Material>("Red_light");
        // greenmaterial = Resources.Load<Material>("Greenlight");
        renderer = GetComponent<MeshRenderer>();
        renderer.material.color = Color.red;
        renderer.material.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) )
        {
            if(renderer.material.color == Color.red)
            {
                renderer.material.SetColor("_EmissionColor", Color.green);
                renderer.material.color = Color.green;
            } else
            {
                renderer.material.SetColor("_EmissionColor", Color.red);
                renderer.material.color = Color.red;
            }
            
        }
        
    }
}
