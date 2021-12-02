using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class ExplodingCow : MonoBehaviour
{
    public GameObject explosion;
    public GameObject cow;
    // Start is called before the first frame update
    void Start()
    {
        explosion = GameObject.Find("BigExplosion");
        cow = GameObject.Find("CowBlW");
    }


    // Update is called once per frame
    void Update()
    {
     
    }

    public void explodeCow()
    {
        explosion.GetComponent<TraumaInducer>().play();
        GetComponent<AudioSource>().Play();
        explosion.GetComponent<ParticleSystem>().Play();
        // Instantiate(explosion, explosion.transform.position, explosion.transform.rotation);
        // Destroy(cow);
        cow.GetComponent<MeshRenderer>().enabled = false;
        cow.GetComponent<MeshCollider>().enabled = false;
    }
}
