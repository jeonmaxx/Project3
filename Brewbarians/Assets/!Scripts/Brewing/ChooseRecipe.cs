using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseRecipe : MonoBehaviour, IPointerClickHandler
{
    public GameObject brewStation;
    private OpenBrewing openBrewing;
    private BrewingManager brewingManager;
    public GameObject manager;
    private OpenInventory inventory;

    private void Start()
    {
        openBrewing = brewStation.GetComponent<OpenBrewing>();
        brewingManager = brewStation.GetComponent<BrewingManager>();
        inventory = manager.GetComponent<OpenInventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventory.windows = Windows.Recipe;
        inventory.inventoryActive = true;
        openBrewing.menuOpen = false;
        openBrewing.choosing = true;

    }
}


