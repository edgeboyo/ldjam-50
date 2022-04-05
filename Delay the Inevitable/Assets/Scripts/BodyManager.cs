using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete]
public class BodyManager : MonoBehaviour
{
    [SerializeField] private GameObject upperBody;
    [SerializeField] private GameObject lowerBody;

    private Dictionary<bool, float> lowerBodyOffsets = new Dictionary<bool, float>() { { false, -0.26f }, { true, 0.24f } };

    private Animator upperAnimator;
    private Animator lowerAnimator;

    private Animator[] animators;

    private SpriteRenderer upperSR;
    private SpriteRenderer lowerSR;

    private SpriteRenderer[] renderers;

    // [HideInInspector] public Collider2D collider; //helpful for calculation. Leave hidden in case someone has a v bad idea

    // Start is called before the first frame update
    void Start()
    {
        upperAnimator = upperBody.GetComponent<Animator>();
        lowerAnimator = lowerBody.GetComponent<Animator>();

        animators = new Animator[2] { upperAnimator, lowerAnimator };

        upperSR = upperBody.GetComponent<SpriteRenderer>();
        lowerSR = lowerBody.GetComponent<SpriteRenderer>();

        renderers = new SpriteRenderer[2] { upperSR, lowerSR };

        // collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // don't think I'll need this     
    }

    public void SetAnimatorBool(string flagName, bool value)
    {
        foreach(var anim in animators)
        {
            anim.SetBool(flagName, value);
        }
    }

    public void SetAnimatorFloat(string flagName, float value)
    {
        foreach(var anim in animators)
        {
            anim.SetFloat(flagName, value);
        }
    }

    public void SetFlip(bool flipped)
    {
        foreach(var sr in renderers)
        {
            sr.flipX = flipped;
        }

        Vector3 lowerOffset = lowerBody.transform.localPosition;

        lowerOffset.x = lowerBodyOffsets[flipped];

        lowerBody.transform.localPosition = lowerOffset;
    }
}
