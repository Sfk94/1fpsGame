using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporter : MonoBehaviour
{
        void OnTriggerEnter2D(Collider2D hit){
            if(hit.gameObject.name == "Player"){
                Debug.Log("msg sent");
                GameObject.Find("Player").SendMessage("React");
            }
        }
}
