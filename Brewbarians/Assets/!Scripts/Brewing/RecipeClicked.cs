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

    public void OnPointerClick(PointerEventData eventData)
    {
        if(clickable && recipeSlot != null)
        {
            recipeItem = GetComponent<RecipeItem>();
            choose = recipeSlot.GetComponent<ChooseRecipe>();
            choose.chosenRecipe = recipeItem.recipe;
            choose.recipeChosen = true;

            Debug.Log("recipe added");
        }
    }
}
