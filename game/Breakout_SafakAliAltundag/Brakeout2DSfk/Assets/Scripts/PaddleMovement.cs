using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public float speed;
    public AudioClip sound;

    void Start()
    {
        
    }

    
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * Time.deltaTime * horizontalInput * speed);

        if (transform.position.x < -8){
            transform.position = new Vector2(-8, transform.position.y);
        }

         if (transform.position.x > 8){
            transform.position = new Vector2(8, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            SoundManager.instance.PlaySound(sound);
        }
        
    }
}
