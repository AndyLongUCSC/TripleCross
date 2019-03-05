using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Load : MonoBehaviour
{
    public PlayerOneController player;
    public int max;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetCount() == max)
        {
            StartCoroutine(Wait());
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("MainMenu");
    }
}
