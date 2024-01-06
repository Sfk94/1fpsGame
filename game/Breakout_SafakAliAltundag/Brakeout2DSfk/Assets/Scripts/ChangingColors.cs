using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingColors : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public float timeToChange = 0.1f;
    private float timeSinceChange = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timeSinceChange += Time.deltaTime;

        if(spriteRenderer != null && timeSinceChange >= timeToChange) {
            
            
            Color newColor = new Color(
                Random.value,
                Random.value,
                Random.value
            );

            
            
            spriteRenderer.color = newColor;
            timeSinceChange = 0f;
        }
    

}
}

