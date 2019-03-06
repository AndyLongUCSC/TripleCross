using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    private GameObject player;

    public float timeDie = 10f;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
        time += Time.deltaTime;

        if (time >= timeDie)
        {
            Destroy(this.gameObject);
        }
    }
}
