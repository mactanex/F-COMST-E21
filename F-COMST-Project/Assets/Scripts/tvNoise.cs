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
    private bool jumpscare = false;

    public GameObject Achievement;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            if (!jumpscare)
            {
                materials[1] = JumpScare;
                source.Play();
                Achievement.SetActive(true);
                StartCoroutine(AchievementWait());
            }
            else
            {
                materials[1] = blankScrene;
            }
        }

        else if (string.Equals(materials[1].name, "jumpScare"))
        {
            source.Stop();
            jumpscare = true;
            materials[1] = blankScrene;
        }

        renderer.sharedMaterials = materials;
    }

    IEnumerator AchievementWait()
    {
        yield return new WaitForSeconds(5f);
        
        Achievement.SetActive(false);
    }
}
