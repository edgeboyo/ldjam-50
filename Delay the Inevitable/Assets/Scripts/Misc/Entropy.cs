using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entropy : MonoBehaviour
{
    public float lifeSpan = 10;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathAfterTime(lifeSpan));
    }

    IEnumerator DeathAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
