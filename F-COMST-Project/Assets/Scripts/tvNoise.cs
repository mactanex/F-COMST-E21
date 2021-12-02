using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tvNoise : MonoBehaviour
{

    MeshRenderer renderer;
    AudioSource source;
    public AudioClip[] audioclips;
    public Material blankScrene;
    public Material staticNoise;
    public Material JumpScare;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            interactWithTv();
        }
    }

    public void interactWithTv()
    {
        var materials = renderer.sharedMaterials;

        if (string.Equals(materials[1].name, "YourPic"))
        {
            source.clip = audioclips[0];
            source.loop = true;
            source.Play();
            materials[1] = staticNoise;
        }

        else if (string.Equals(materials[1].name, "tvNoise"))
        {
            source.Stop();
            source.clip = audioclips[1];
            source.loop = false;
            materials[1] = JumpScare;
            source.Play();
        }

        else if (string.Equals(materials[1].name, "jumpScare"))
        {
            materials[1] = blankScrene;
        }

        renderer.sharedMaterials = materials;
    }
}
