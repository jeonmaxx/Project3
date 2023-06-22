using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeClicked : MonoBehaviour, IPointerClickHandler
{
    //To-do:
    //ein bool, was auf true gesetzt wird, sobald man die Rezepte anklicken kann
    //dieses bool muss in chooseRecipe auf true gesetzt werden
    //dort eine Liste mit den gesammelten Rezepten anlegen? (ähnlich wie im letzten Projekt)
    //beim schließen des brew-recipe Menus müssen die bools wieder auf false gesetzt werden
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("recipe added");
    }
}
