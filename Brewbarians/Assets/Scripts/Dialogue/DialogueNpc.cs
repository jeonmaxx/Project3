using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueNpc : MonoBehaviour
{
    public PlayerInput Input;
    public DialogueTrigger trigger;
    public bool playerNear = false;

    public void OnInteract()
    {
        if (playerNear == true)
        {
            trigger.StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerNear = false;
        }
    }
}
