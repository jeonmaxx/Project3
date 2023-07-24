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
public enum TutorialState { Introduction, Shovel, Water, SeedPouch, Seed, Waiting, WaterAgain, Harvest, GoToBrewery, Brewing, Done}
public class TutorialDialogue : MonoBehaviour
{
    public DialogueTrigger trigger;
    public DialogueManager manager;
    public InventoryManager inventoryManager;
    public PointsCollector collector;
    public FarmingManager farmingManager;
    public RecipeManager recipeManager;
    public PlayerInput input;
    public List<DialogueList> diaList;
    public TutorialState state;
    public bool newState;
    public GameObject exclaPrefab;
    public Item[] givenItems;
    public Recipe recipe;

    public int neededGrowPoints = 5;
    public Item lastGiven;


    private void Update()
    {
        if(newState && transform.childCount == 0)
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
            diaList[8].Done = true;
            newState = true;
        }
        
    }

    public void OnInteract()
    {
        if (manager.isActive)
        {
            switch (state)
            {
                case TutorialState.Introduction:
                    diaList[(int)state].Done = true;
                    newState = true;
                    break;
                case TutorialState.Shovel:
                    GiveItem(0, 1);
                    newState = false;
                    break;
                case TutorialState.Water:
                    GiveItem(1, 1);
                    newState = false;
                    break;
                case TutorialState.SeedPouch:
                    GiveItem(2, 1);
                    diaList[(int)state].Done = true;
                    break;
                case TutorialState.Seed:
                    GiveItem(3, 1);
                    newState = false;
                    break;
                case TutorialState.Waiting:
                    newState = false;
                    StartCoroutine(GrowingTutorial());
                    break;
                case TutorialState.WaterAgain:
                    newState = false;
                    break;
                case TutorialState.Harvest:
                    GiveItem(4, 1);
                    newState = false;
                    StartCoroutine(GrowingTutorial());
                    break;
                case TutorialState.GoToBrewery:
                    newState = false;
                    break;
                case TutorialState.Brewing:
                    recipeManager.AddRecipe(recipe);
                    GiveItem(5, 2);
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
        if (givenItems[id] != lastGiven || lastGiven == null)
        {
            for (int i = 0; i < count; i++)
            {
                inventoryManager.AddItem(givenItems[id]);
            }
            lastGiven = givenItems[id];
        }        
    }

    public void NextState(TutorialState nextState, bool done)
    {
        if (done)
            state = nextState;
    }

    public IEnumerator GrowingTutorial()
    {
        for (int i = 0; i < neededGrowPoints; i++)
        {
            yield return new WaitForSeconds(1);
            collector.AddFarmPoints();
            farmingManager.GiveFarmPoints();
        }

        diaList[(int)state].Done = true;
        newState = true;

    }
}
