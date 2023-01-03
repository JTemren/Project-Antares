using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikefall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touched1");
        if (other.gameObject.tag == "Player"){
           gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
            other.transform.position = new Vector3(1,1,0);
        }
        gameObject.SetActive(false);
    }
    
}
