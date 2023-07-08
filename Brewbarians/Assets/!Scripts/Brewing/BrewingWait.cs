using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrewingWait : MonoBehaviour
{
    public BrewingManager manager;
    public Slider progressBar;
    public Image recipeItem;
    public TextMeshProUGUI quantityText;

    public void Update()
    {
        if(manager.chosenRecipe != null)
        {
            recipeItem.sprite = manager.chosenRecipe.Drink.image;
            quantityText.SetText(manager.quantity.ToString());
        }
    }
}
