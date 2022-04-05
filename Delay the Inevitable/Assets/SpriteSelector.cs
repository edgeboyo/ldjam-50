using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpriteSelector : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        if(sprites.Length == 0)
        {
            return;
        }

        int choice = Random.Range(0, sprites.Length);

        renderer.sprite = sprites[choice];

        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
