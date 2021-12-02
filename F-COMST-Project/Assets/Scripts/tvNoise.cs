using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tvNoise : MonoBehaviour
{

    MeshRenderer renderer;
    AudioSource source;
    public Material blankScrene;
    public Material staticNoise;
    public Material JumpScare;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            var materials = renderer.sharedMaterials;

            if (string.Equals(materials[1].name, "YourPic"))
            {
                materials[1] = staticNoise;
            }

            else if (string.Equals(materials[1].name, "tvNoise"))
            {
                materials[1] = JumpScare;
            }

            else if (string.Equals(materials[1].name, "jumpScare"))
            {
                materials[1] = blankScrene;
            }

            renderer.sharedMaterials = materials;
        }
    }
}
