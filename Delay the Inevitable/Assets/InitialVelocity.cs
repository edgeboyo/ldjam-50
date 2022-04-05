using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InitialVelocity : MonoBehaviour
{
    public Vector2 initVelocity;
    public float initAngVelocity;
    // Start is called before the first frame update
    void Start()
    {
        float randomX = Random.Range(0.35f, 2.35f);
        float randomY = Random.Range(0.35f, 2.35f);
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(initVelocity.x * randomX, initVelocity.y * randomY));
        rb.AddTorque(initAngVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
