using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.VisualScripting.Metadata;

public class ChooseRecipe : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject brewStation;
    private OpenBrewing openBrewing;
    private BrewingManager brewingManager;
    [SerializeField] private GameObject manager;
    private OpenInventory inventory;
    [SerializeField] private GameObject recipeHolder;
    [HideInInspector] public List<RecipeClicked> recipeClicked;
    public Recipe chosenRecipe;

    private void Start()
    {
        openBrewing = brewStation.GetComponent<OpenBrewing>();
        brewingManager = brewStation.GetComponent<BrewingManager>();
        inventory = manager.GetComponent<OpenInventory>();        
    }

    private void Update()
    {
        if(openBrewing.choosing)
        {
            for(int i = 0; i < recipeClicked.Count; i++)
            {
                recipeClicked[i].clickable = true;
            }
        }
        else if(!openBrewing.choosing)
        {
            for (int i = 0; i < recipeClicked.Count; i++)
            {
                recipeClicked[i].clickable = false;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(openBrewing.menuOpen)
        {
            inventory.windows = Windows.Recipe;
            inventory.inventoryActive = true;
            openBrewing.menuOpen = false;
            openBrewing.choosing = true;
        }
        //maybe eine Klasse machen, von der geerbt wird
        //wo es eine Methode gibt, mit der man das Inventar auf und zu machen kann

    }
}


