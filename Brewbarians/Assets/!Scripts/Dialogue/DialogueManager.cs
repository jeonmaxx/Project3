using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;

    [HideInInspector] public Message[] currentMessages;
    private Actor[] currentActors;
    [HideInInspector] public int activeMessage = 0;
    public bool isActive = false;

    public InputActionReference inputAction;
    private InputAction action;

    public void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
        action = inputAction.action;
    }

    public void Update()
    {
        action.started += _ => OnSkip();
    }

    public void OnSkip()
    {
        if (isActive == true)
        {
            NextMessage();
        }
    }

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        DisplayMessage();
        backgroundBox.transform.localScale = Vector3.one;
    }

    public void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        AnimateTextColor();
    }

    public void NextMessage()
    {
        activeMessage++;
        if(activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            backgroundBox.transform.localScale = Vector3.zero;
            isActive = false;
        }
    }
    
    public void AnimateTextColor()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }
}
