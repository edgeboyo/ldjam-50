using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public SpriteRenderer sr;

    public GameObject CreateOnClick;

    // private float alphaPost;
    // Start is called before the first frame update
    void Start()
    {
        sr.enabled = false;
    }
    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
        sr.enabled = true;
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
        sr.enabled = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse clik");
        if(CreateOnClick !=  null)
            Instantiate(CreateOnClick);
    }
}
