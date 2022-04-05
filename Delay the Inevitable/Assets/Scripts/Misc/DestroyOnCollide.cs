using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToDelete;
    // [SerializeField] private Component ComponentToDelete;
    [SerializeField] private string[] LayersToCover;

    [SerializeField] private float DeleteObjectDelay;
    [SerializeField] private float DeleteComponentDelay;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("ColliderTime");
        foreach(var layer in LayersToCover)
        {
            if(LayerMask.GetMask(layer) == collision.gameObject.layer)
            {
                Debug.Log("DestroyTime");
                Destroy(ObjectToDelete, DeleteObjectDelay);
                // Destroy(ComponentToDelete, DeleteComponentDelay);
                Destroy(this);
            }
        }
    }
}
