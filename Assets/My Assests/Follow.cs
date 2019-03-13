using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private CapsuleCollider cap;
    // Start is called before the first frame update
    void Start()
    {
        cap = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        cap.center = transform.position;
    }
}
