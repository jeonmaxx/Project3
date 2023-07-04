using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeClicked : MonoBehaviour, IPointerClickHandler
{
    public bool clickable = false;
    public GameObject recipeSlot;
    private ChooseRecipe choose;
    private RecipeItem recipeItem;

    public Recipe rec;

    public GameObject manager;
    public GameObject brewingStation;
    private OpenInventory inventory;
    private OpenBrewing openBrewing;

    public void OnPointerClick(PointerEventData eventData)
    {
        inventory = manager.GetComponent<OpenInventory>();
        openBrewing = brewingStation.GetComponent<OpenBrewing>();
        if(clickable && recipeSlot != null)
        {
            recipeItem = GetComponent<RecipeItem>();
            choose = recipeSlot.GetComponent<ChooseRecipe>();
            choose.chosenRecipe = recipeItem.recipe;
            choose.recipeChosen = true;

            Debug.Log("recipe added");

            if (!openBrewing.menuOpen && choose.recipeChosen)
            {
                openBrewing.Open();
                choose.slotImage.sprite = choose.chosenRecipe.Drink.image;
            }
        }
    }
}