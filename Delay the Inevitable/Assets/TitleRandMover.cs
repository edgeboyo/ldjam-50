using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleRandMover : MonoBehaviour
{

    private GameObject fader;

    public List<string> scenes;
    // Start is called before the first frame update
    void Start()
    {
        fader = GameObject.Find("Fader");

        fader.GetComponent<FadeInOut>().DoFadeIn();

        fader.transform.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!fader.GetComponent<FadeInOut>().Running)
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            Scene thisScene = SceneManager.GetActiveScene();

            scenes.Remove(thisScene.name);

            string nextScene = sceneChooser();
            Debug.Log("Chosen " + nextScene + " for next lvl");

            SceneManager.LoadScene(nextScene);

            // this.transform.position = new Vector3(-2112, -119);
            List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            foreach (GameObject pl in players)
            {
                if (pl != this.gameObject)
                {
                    Destroy(pl);
                }
            }

            this.enabled = false;
        }
    }
    string sceneChooser()
    {
        var rand = new System.Random();
        return scenes[rand.Next(0, scenes.Count - 1)];
    }
}
