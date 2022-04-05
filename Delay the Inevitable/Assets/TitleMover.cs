using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMover : MonoBehaviour
{

    private GameObject fader;
    // Start is called before the first frame update
    void Start()
    {
        fader = GameObject.Find("Fader");

        fader.GetComponent<FadeInOut>().DoFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if(!fader.GetComponent<FadeInOut>().Running)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
