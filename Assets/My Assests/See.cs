using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class See : MonoBehaviour
{
    private bool canSee = false;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canSee = true;
            player = other.gameObject;
        }
    }

    public bool Sees()
    {
        return canSee;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
