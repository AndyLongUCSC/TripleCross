using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneController : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 90f;

    public string[] Inputs;

    public int InputSize = 5;

    public float inputSecondTime = 2f;

    public float time = 0;
    public bool startTime = false;

    public bool Inputting = false;

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
        Inputs = new string[InputSize];
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
            if(Inputting == true)
            {
                ClearInputs();
            }
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

        if (Input.GetKeyDown(KeyCode.I))
        {
            for(int i = 0; i < InputSize; i++)
            {
                if (Inputs[i] == null)
                {
                    if (i == 0)
                    {
                        animator.SetBool("Up", true);
                        Inputs[i] = "i";
                        startTime = true;
                        break;
                    }
                    else if (i > 0 && Inputs[i - 1] != "i")
                    {
                        time = 0;
                        animator.SetBool("Up", true);
                        Inputs[i] = "i";
                        startTime = true;
                        break;
                    }
                }
            }
            StartCoroutine(Wait());
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            for (int i = 0; i < InputSize; i++)
            {
                if (Inputs[i] == null)
                {
                    if (i == 0)
                    {
                        animator.SetBool("Left", true);
                        Inputs[i] = "j";
                        startTime = true;
                        break;
                    }
                    else if (i > 0 && Inputs[i - 1] != "j")
                    {
                        time = 0;
                        animator.SetBool("Left", true);
                        Inputs[i] = "j";
                        startTime = true;
                        break;
                    }
                }
            }
            StartCoroutine(Wait());
        }

        if (startTime == true)
        {
            time += Time.deltaTime;
            Inputting = true;
            if (time >= inputSecondTime)
            {
                ClearInputs();
            }
        }
        animator.SetFloat("time", time);
        Debug.Log(time);
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

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        ClearBools();
    }

    private void ClearInputs()
    {
        Inputting = false;
        time = 0;
        startTime = false;
        for(int i = 0; i < InputSize; i++)
        {
            Inputs[i] = null;
        }
        if(move == true)
        {
            animator.Play("Run", 0);
        }
        else
        {
            animator.Play("Idle", 0);
        }
        ClearBools();
    }

    private void ClearBools()
    {
        animator.SetBool("Up", false);
        animator.SetBool("Left", false);
    }
}
