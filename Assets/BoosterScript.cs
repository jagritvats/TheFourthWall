using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BoosterScript : MonoBehaviour
{
    public AudioClip AudioClip = null;
    public GameManager manager;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 

        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Minting booster");
            ReneController.MintBooster();



            GetComponent<AudioSource>().PlayOneShot(AudioClip);
            spriteRenderer.enabled = false;
            manager.addScore(2);
            Destroy(gameObject, AudioClip.length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
