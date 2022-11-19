using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer thisSprite;

    int i = 0;
    void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
        thisSprite.sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Iterate()
    {
        Debug.Log("interacted with" + gameObject.name);
        if (i < sprites.Length)
        {
            thisSprite.sprite = sprites[i];
            Debug.Log("Flag 1, Material Changed");
            i++;
        }
        else if (i >= sprites.Length)
        {
            i = 0;
            thisSprite.sprite = sprites[i];
            Debug.Log("Flag 2, Material Changed, i looped to 0");
            i++;
        }
    }
}
