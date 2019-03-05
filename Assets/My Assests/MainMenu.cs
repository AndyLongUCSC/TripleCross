using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
