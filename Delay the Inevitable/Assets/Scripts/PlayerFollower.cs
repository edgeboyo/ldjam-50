using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    private Transform followTransform;

    void LateUpdate()
    {
        followTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z);
    }
}
