using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectable : MonoBehaviour
{
    public AudioSource audioSource;
    private int framesleft = 0;
    // Update is called once per frame
    void Update()
    {
        if (framesleft > 0){
            framesleft --;
            if (framesleft == 0){
                gameObject.SetActive(false);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Touched1");
        if (other.gameObject.tag == "Player" && framesleft == 0){
            audioSource.Play();
            GetComponent<Renderer>().enabled = false;
            framesleft = 60;
            //Debug.Log("Touched2");
            gameObject.SetActive(false);
        }
    }
}
