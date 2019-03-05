using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject follow;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = follow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (follow != null)
        {
            this.transform.position = follow.transform.position;
        }
    }
}
