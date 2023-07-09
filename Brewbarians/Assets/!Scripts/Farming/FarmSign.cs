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
    public RectTransform seedWheel;
    private SeedWheelManager seedWheelManager;

    public Item signSeed;

    public SpriteRenderer signSprite;

    private Color color;

    public bool wheelOpen = false;

    public void Start()
    {
        seedWheel.transform.localScale = Vector3.zero;
        color = signSprite.color;
    }

    public void Update()
    {
        CheckHand();
        CalcDistance();

        if (wheelOpen && !isPlayerNear)
        {
            signSeed = seedWheelManager.chosenSeed;
            if (signSeed != null)
            {
                signSprite.sprite = signSeed.image;
                signSprite.color = new Color(color.r, color.g, color.b, 100);
            }
            seedWheel.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            wheelOpen = false;
        }
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


        if ((int)handType == 3 && inHand)
            seedInHand = true;
        else if ((int)handType != 3 || !inHand)
            seedInHand = false;

    }

    public void OnInteract()
    {
        if (isPlayerNear && seedInHand && !wheelOpen)
        {
            seedWheel.LeanScale(Vector3.one, 0.5f);
            seedWheelManager = seedWheel.GetComponent<SeedWheelManager>();
            signSeed = seedWheelManager.chosenSeed;
            if (signSeed != null)
            {
                signSprite.sprite = signSeed.image;
                signSprite.color = new Color(color.r, color.g, color.b, 100);
            }
            wheelOpen = true;
        }

        else if (wheelOpen)
        {
            signSeed = seedWheelManager.chosenSeed;
            if (signSeed != null)
            {
                signSprite.sprite = signSeed.image;
                signSprite.color = new Color(color.r, color.g, color.b, 100);
            }
            seedWheel.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            wheelOpen = false;
        }
    }
}
