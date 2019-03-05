using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    private bool up = false;
    private float time;
    public ParticleSystem sparkle;
    private ParticleSystem.EmissionModule particle;

    // Start is called before the first frame update
    void Start()
    {
        if (sparkle != null)
        {
            particle = sparkle.emission;
            particle.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15f, 30f, 45f) * Time.deltaTime);

        time += Time.deltaTime;
        if (up == false)
        {
            transform.position += new Vector3(0, 0.01f * time, 0);
            if(time >= 1)
            {
                up = true;
                time = 0;
            }
        }

        if(up == true)
        {
            transform.position -= new Vector3(0, 0.01f * time, 0);
            if(time >= 1)
            {
                up = false;
                time = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            particle.enabled = true;
            sparkle.GetComponent<ParticleSystem>().Play();
        }
    }
}
