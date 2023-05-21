using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class FarmSign : PlayerNear
{
    public HandManager handManager;
    private ActionType handType;
    private bool inHand = false;
    public bool seedInHand;
    public PlayerInput input;
    private Seed seed;

    public Item signSeed;

    public SpriteRenderer signSprite;

    public void Update()
    {
        CheckHand();
        CalcDistance();
    }

    public void CheckHand()
    {
        if (handManager.handItem != null)
        {
            handType = handManager.handItem.actionType;
            inHand = true;
        }
        else
            inHand = false;


        if ((int)handType == 2 && inHand)
            seedInHand = true;
        else if ((int)handType != 2 || !inHand)
            seedInHand = false;

    }

    public void OnInteract()
    {
        if (isPlayerNear && seedInHand)
        {
            signSeed = handManager.handItem;
            signSprite.sprite = handManager.handItem.image;
        }
    }
}
