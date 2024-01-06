using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionEnd: MonoBehaviour
{
    Rigidbody2D rb;
    public string SceneToLoad;
    public GameObject GameWonScreen;


    public void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger){
            GameWonScreen.SetActive(true);
            Time.timeScale = 0f;
            // SceneManager.LoadScene(SceneToLoad);
        }

    } 
     
        
    
}
