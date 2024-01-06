using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameIsOver;
    public Text gameOverTxt;
    public Button restartButton;
    public Text win;
    
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  GameOver()
    {
       gameIsOver = true; 
       gameOverTxt.gameObject.SetActive(true);
       restartButton.gameObject.SetActive(true);
    }

    public void  Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void winner () {
        win.gameObject.SetActive(true);
    }
}
