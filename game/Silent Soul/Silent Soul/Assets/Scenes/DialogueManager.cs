using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{


    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private Queue<string> sentences;



    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue){
        animator.SetBool("isOpen", true);
        sentences.Clear();
        Debug.Log("starting dialogue");

        nameText.text = dialogue.name;


        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if (sentences.Count == 0){
            Debug.Log("0 sentences");
            EndDialogue();
            return;
        }
        Debug.Log("displaying sentences");
        string sentence = sentences.Dequeue();  
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return new WaitForSeconds(.02f);
        }
    }


    void EndDialogue(){
        animator.SetBool("isOpen", false);  
    }

}
