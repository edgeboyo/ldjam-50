using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{

    float scale;

    [SerializeField] private float initDist;
    [SerializeField] private float mulitiplier;

    // Start is called before the first frame update
    void Start()
    {
        scale = Input.mouseScrollDelta.y;
    }

    // Update is called once per frame

    void Update()
    {
            float newScale = Input.mouseScrollDelta.y;

            scale = scale - newScale;

            if(scale < -22.5f){
                scale = -22.5f;
            } else if(scale > 22.5f) {
                scale = 22.5f;
            }

            transform.position = new Vector3(this.transform.position.x, this.transform.position.y, initDist - scale*mulitiplier);
    }
}
