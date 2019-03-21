using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBlast : MonoBehaviour
{
    private GameObject player;

    public float timeDie = 30f;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x - 0.6f, player.transform.position.y + 1f, player.transform.position.z);
        time += Time.deltaTime;

        if (time >= timeDie)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
