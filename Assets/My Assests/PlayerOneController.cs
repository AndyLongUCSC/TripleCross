using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneController : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 90f;

    public Animator animator;

    private Rigidbody rb;

    float axis;
    float axis2;
    bool move;

    public int counter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        counter = 0;

    }

    // Update is called once per frame
    private void Update()
    {
    }

    void FixedUpdate()
    {
        axis = Input.GetAxis("Vertical");
        axis2 = Input.GetAxis("Horizontal");
        if (axis != 0)
        {
            move = true;
        }
        else if (axis == 0)
        {
            move = false;
        }


        rb.velocity = transform.forward * speed * axis * Time.deltaTime;

        rb.angularVelocity = (Vector3.up * rotateSpeed * axis2 * Time.deltaTime);

        animator.SetFloat("Velocity", rb.velocity.z);
        animator.SetFloat("HVelocity", rb.velocity.x);
        animator.SetBool("Move", move);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "pickup")
        {
            Destroy(other.gameObject);
            counter++;
        }
    }

    public int GetCount()
    {
        return counter;
    }
}
