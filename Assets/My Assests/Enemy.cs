using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{

    private Animator anim;

    private bool dead = false;

    private bool forward = true;
    private bool stopped = false;
    private Rigidbody rb;
    private GameObject player;

    private bool canSee = false;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        canSee = GetComponentInChildren<See>().Sees();
        if (canSee == false && dead == false)
        {
            if (transform.position.z < 11.1 && forward == true && stopped == false)
            {
                anim.SetBool("Walking", true);
                rb.velocity = new Vector3(0, 0, 1);
            }
            if (transform.position.z >= 11 && forward == true)
            {
                stopped = true;
                anim.SetBool("Walking", false);
                rb.velocity = new Vector3(0, 0, 0);
                StartCoroutine(Spin());

            }
            if (transform.position.z > 1.9 && forward == false && stopped == false)
            {
                anim.SetBool("Walking", true);
                rb.velocity = new Vector3(0, 0, -1);
            }
            if (transform.position.z <= 2 && forward == false)
            {
                stopped = true;
                anim.SetBool("Walking", false);
                rb.velocity = new Vector3(0, 0, 0);
                StartCoroutine(Spin2());
            }
        }
        if(canSee == true && dead == false)
        {
            player = GetComponentInChildren<See>().GetPlayer();
            transform.LookAt(player.transform);
            if ((Vector3.Distance(player.transform.position, transform.position)) <= 2.1 && (Vector3.Distance(player.transform.position, transform.position)) >= 0){
                anim.SetBool("Running", false);
                anim.SetBool("Attacking", true);
            }
            else
            {
                anim.SetBool("Running", true);
                anim.SetBool("Attacking", false);
                transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, 2f);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "spell")
        {
            anim.Play("Hit", 0);
            dead = true;
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
    }
    
    private void Died()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

    private IEnumerator Spin()
    {
        rb.angularVelocity = (-Vector3.up * 60 * Time.deltaTime);
        yield return new WaitForSeconds(3);
        rb.angularVelocity = new Vector3(0, 0, 0);
        forward = false;
        stopped = false;
    }
    private IEnumerator Spin2()
    {
        rb.angularVelocity = (-Vector3.up * 60 * Time.deltaTime);
        yield return new WaitForSeconds(3);
        rb.angularVelocity = new Vector3(0, 0, 0);
        forward = true;
        stopped = false;
    }

    public bool IsDead()
    {
        return dead;
    }
}
