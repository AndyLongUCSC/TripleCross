using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    public PlayerOneController player;
    public int count = 1;
    public float speed = 10f;
    public float duration = 3f;
    public int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerOneController>().GetCount() >= count)
        {
            if (direction == 1)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else if (direction == 0)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else if(direction == 3)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else if (direction == 4)
            {
                transform.Translate(-Vector3.forward * speed * Time.deltaTime);
            }


            StartCoroutine(Death(duration));
        }
    }

    IEnumerator Death(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this);
    }
}
