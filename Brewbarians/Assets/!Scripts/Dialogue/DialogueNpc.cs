using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueNpc : PlayerNear
{
    public PlayerInput Input;
    public DialogueTrigger trigger;

    private void Update()
    {
        CalcDistance();
    }

    public void OnInteract()
    {
        if (isPlayerNear)
        {
            trigger.StartDialogue();
        }
    }

    
}
