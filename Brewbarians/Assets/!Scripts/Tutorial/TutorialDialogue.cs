using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[Serializable]
public class DialogueList
{
    public TutorialState State;
    public Message[] Messages;
    public bool Done;

    public DialogueList(TutorialState state, Message[] messages, bool done) 
    { 
        State = state;
        Messages = messages;
        Done = done;
    }
}
public enum TutorialState { Introduction, Shovel, Water, SeedPouch, Seed, Harvest, GoToBrewery, GoToMachine, ChooseRec, Qte, Brewing, Done}
public class TutorialDialogue : PlayerNear
{
    public DialogueTrigger trigger;
    public DialogueManager manager;
    public InventoryManager inventoryManager;
    public PointsCollector collector;
    public FarmingManager farmingManager;
    public RecipeManager recipeManager;
    public List<DialogueList> diaList;
    public TutorialState state;
    public bool newState;
    public GameObject exclaPrefab;
    public Item[] givenItems;
    public Recipe recipe;

    public int neededGrowPoints = 5;
    public int harvestAmount = 3;
    public int currentHarvest;

    public bool itemGiven;

    public bool resetTrigger;

    public InputActionReference inputAction;
    private InputAction action;

    public void Start()
    {
        action = inputAction.action;
    }

    private void Update()
    {
        action.started += _ => OnInteract();

        CalcDistance();
        if (newState && transform.childCount == 0)
        {
            Instantiate(exclaPrefab, transform);
        }
        else if(!newState && transform.childCount != 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }


        for(int i = 0; i < diaList.Count; i++)
        {
            if(state == diaList[i].State)
            {
                trigger.messages = diaList[i].Messages;
            }

            if (diaList[i].Done)
            {
                NextState((TutorialState)(i+1), diaList[i].Done);
            }
        }

        if(SceneManager.GetActiveScene().buildIndex == 2 && state == TutorialState.GoToBrewery)
        {
            diaList[6].Done = true;
            newState = true;
        }

        if(newState)
        {
            itemGiven = false;
        }
        
        if(state == TutorialState.ChooseRec ||
            state == TutorialState.Qte ||
            state == TutorialState.Brewing)
        {
            trigger.PassiveDialogue();
            newState = false;
        }
    }

    public void OnInteract()
    {
        if (isPlayerNear)
        {
            trigger.StartDialogue();
            Debug.Log(trigger.messages.Length + " tutorial");
            switch (state)
            {
                case TutorialState.Introduction:
                    diaList[(int)state].Done = true;
                    itemGiven = false;
                    newState = true;
                    break;
                case TutorialState.Shovel:
                    GiveItem(0, 1);
                    itemGiven = true;
                    newState = false;
                    break;
                case TutorialState.Water:
                    GiveItem(1, 1);
                    itemGiven = true;
                    newState = false;
                    break;
                case TutorialState.SeedPouch:
                    GiveItem(2, 1);
                    GiveItem(3, 5);
                    itemGiven = true;
                    newState = false;
                    break;
                case TutorialState.Seed:
                    newState = false;
                    break;
                case TutorialState.Harvest:
                    GiveItem(4, 1);
                    itemGiven = true;
                    newState = false;
                    break;
                case TutorialState.GoToBrewery:
                    newState = false;
                    break;
                case TutorialState.GoToMachine:
                    recipeManager.AddRecipe(recipe);
                    newState = false;
                    break;
                case TutorialState.ChooseRec:
                    newState = false;
                    break;
                case TutorialState.Qte:
                    newState = false;
                    break;
                case TutorialState.Brewing:
                    newState = false;
                    break;
                case TutorialState.Done:
                    newState = false;
                    break;

            }
        }
    }

    public void GiveItem(int id, int count)
    {
        if (!itemGiven)
        {
            for (int i = 0; i < count; i++)
            {
                inventoryManager.AddItem(givenItems[id]);
            }
        }        
    }

    public void NextState(TutorialState nextState, bool done)
    {
        if (done)
            state = nextState;
    }

    //public IEnumerator GrowingTutorial()
    //{
    //    for (int i = 0; i < neededGrowPoints; i++)
    //    {
    //        yield return new WaitForSeconds(1);
    //        collector.AddFarmPoints();
    //        farmingManager.GiveFarmPoints();
    //    }

    //    diaList[(int)state].Done = true;
    //    newState = true;

    //}
}
