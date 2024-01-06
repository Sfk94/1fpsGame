using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
   
    public GameObject player;
    public Rigidbody2D shuriken;
    public float ballSpeed;
    public bool gameStarted;
    public Vector2 offset = new Vector2 (0, 0.6f);
    public GameObject particles;
    public AudioClip sound;
    public AudioClip sound2;
    public AudioClip sound3;
    public int lives = 3;
    public Text livesTxt;
    public GameObject ball;


 
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameStarted ==false){
            shuriken.AddForce(Vector2.up * ballSpeed);
            gameStarted = true;
        }

        if (gameStarted == false){
            transform.position = (Vector2)player.transform.position + offset;
        }

        livesTxt.text = "Lives: " + lives;

        if (this.lives <= 0){
            ChcekGmaeOver();
        }

        

    }
   

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Brick")){
            Destroy(collision.gameObject);
            Instantiate(particles, transform.position, Quaternion.identity);
            SoundManager.instance.PlaySound(sound);
    }   else if (collision.gameObject.CompareTag("Wall")){
        SoundManager.instance.PlaySound(sound2);
    } else {
        SoundManager.instance.PlaySound(sound3);
    } if(collision.gameObject.CompareTag("Ground")){
        this.lives--;
    }
        
}


private void ChcekGmaeOver () {
    if(lives <= 0) {
    Destroy(ball);
    FindObjectOfType<GameManager>().GameOver();
    }
}


    
}