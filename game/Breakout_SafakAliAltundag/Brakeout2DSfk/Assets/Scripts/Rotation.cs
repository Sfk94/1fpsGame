using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    
    private float rotZ;
    public float RotationSpeed;
    public float ballSpeed;
    public bool ClockwiseRotation;
    private float _horizontalinput;
  

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ClockwiseRotation==false){
            rotZ += Time.deltaTime * RotationSpeed + ballSpeed;
        } else {
            rotZ += -Time.deltaTime * RotationSpeed + ballSpeed;
        }

        transform.rotation = Quaternion.Euler(0,0,rotZ);
    }


}
