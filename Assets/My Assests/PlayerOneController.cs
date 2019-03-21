using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerOneController : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 90f;

    public Text healthText;
    public Text manaText;

    public string[] Inputs;

    public List<GameObject> Spells;

    public int InputSize = 5;

    public float inputSecondTime = 2f;

    public float time = 0;
    public bool startTime = false;

    public bool Inputting = false;

    public Animator animator;

    private Rigidbody rb;

    private GameObject spell;

    private bool once = false;
    private bool canCast = true;

    public int hp = 12;
    public int mp = 12;

    public List<GameObject> health;
    public List<GameObject> mana;

    private bool gainMana = true;

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
        SetHealth();
        SetMana();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Inputs[InputSize-1] != null)
        {
            if(Inputs[0] == "i" && Inputs[1] == "j" && Inputs[2] == "l" && once == false)
            {
                CastSpell("aoe");
            }
            if (Inputs[0] == "i" && Inputs[1] == "j" && Inputs[2] == "k" && once == false)
            {
                CastSpell("flamethrower");
            }
        }

        if(mp < 12 && gainMana == true)
        {
            StartCoroutine(GainingMana());
        }

        if(Inputs[0] == null && (Inputs[1] != null || Inputs[2] != null))
        {
            ClearInputs();
        }

        if (Input.GetKeyDown(KeyCode.I) && canCast == true)
        {
            for (int i = 0; i < InputSize; i++)
            {
                if (Inputs[i] == null)
                {
                    time = 0;
                    animator.SetBool("Up", true);
                    Inputs[i] = "i";
                    startTime = true;
                    break;
                }
            }
            StartCoroutine(Wait());
        }

        if (Input.GetKeyDown(KeyCode.J) && canCast == true)
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

        if (Input.GetKeyDown(KeyCode.K) && canCast == true)
        {
            for (int i = 0; i < InputSize; i++)
            {
                if (Inputs[i] == null)
                {
                    if (i == 0)
                    {
                        animator.SetBool("Down", true);
                        Inputs[i] = "k";
                        startTime = true;
                        break;
                    }
                    else if (i > 0 && Inputs[i - 1] != "k")
                    {
                        time = 0;
                        animator.SetBool("Down", true);
                        Inputs[i] = "k";
                        startTime = true;
                        break;
                    }
                }
            }
            StartCoroutine(Wait());
        }

        if (Input.GetKeyDown(KeyCode.L) && canCast == true)
        {
            for (int i = 0; i < InputSize; i++)
            {
                if (Inputs[i] == null)
                {
                    if (i == 0)
                    {
                        animator.SetBool("Right", true);
                        Inputs[i] = "l";
                        startTime = true;
                        break;
                    }
                    else if (i > 0 && Inputs[i - 1] != "l")
                    {
                        time = 0;
                        animator.SetBool("Right", true);
                        Inputs[i] = "l";
                        startTime = true;
                        break;
                    }
                }
            }
            StartCoroutine(Wait());
        }

        if(hp <= 0)
        {
            StartCoroutine(WaitToDie());
        }
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "pickup")
        {
            Destroy(other.gameObject);
            counter++;
        }

        if(other.gameObject.tag == "enemy")
        {
            hp -= 3;
            animator.Play("Hurt", 0);
            if(hp == 0)
            {
                animator.SetBool("Death", true);
            }
            SetHealth();
        }
    }

    public int GetCount()
    {
        return counter;
    }

    private IEnumerator Wait()
    {
        canCast = false;
        yield return new WaitForSeconds(0.2f);
        canCast = true;
        ClearBools();
    }

    private void ClearInputs()
    {
        Inputting = false;
        time = 0;
        startTime = false;
        once = false;
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
        animator.SetBool("Down", false);
        animator.SetBool("Right", false);
    }

    private void CastSpell(string str)
    {
        if (mp > 0)
        {
            once = true;
            if (str == "aoe")
            {
                spell = Instantiate(Spells[0], new Vector3(0, 0, 0), Quaternion.identity);
                spell.transform.position = this.transform.position;
            }
            if (str == "flamethrower")
            {
                spell = Instantiate(Spells[1], new Vector3(0, 0, 0), Quaternion.identity);
                spell.transform.position = this.transform.position;
            }
            mp -= 3;
            SetMana();
        }
    }

    private void SetHealth()
    {
        healthText.text = "Health: " + hp + "/12";
        for (int i = 11; i >= 0; i--)
        {
            if (i == (hp -1))
            {
                break;
            }
            if (health[i] != null)
            {
                Destroy(health[i]);
            }
        }
    }

    private void SetMana()
    {
        manaText.text = "Mana: " + mp + "/12";
        for (int i = 11; i >= 0; i--)
        {
            if (i == (mp - 1))
            {
                break;
            }
            if (mana[i] != null)
            {
                mana[i].GetComponentInChildren<Image>().enabled = false;
            }
        }
    }

    private IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
    }

    private IEnumerator GainingMana()
    {
        gainMana = false;
        yield return new WaitForSeconds(10);
        mp += 1;
        for (int i = 0; i <12; i++)
        {
            if (mana[i].GetComponentInChildren<Image>().enabled != true)
            {
                mana[i].GetComponentInChildren<Image>().enabled = true;
            }
            if (i == (mp - 1))
            {
                break;
            }
        }
        manaText.text = "Mana: " + mp + "/12";
        gainMana = true;
    }
}
