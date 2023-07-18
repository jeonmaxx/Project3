using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Plants
{
    public Seed Seed;
    public PlantStates PlantState;
    public int GrowPoints;

    public Plants(Seed seed, PlantStates plantState, int growPoints)
    {
        Seed = seed;
        PlantState = plantState;
        GrowPoints = growPoints;
    }
}

[Serializable]
public class Fields
{
    public FieldStates FieldState;
    public int HoedPoints;
    public int WetPoints;

    public Fields(FieldStates fieldState, int hoedPoints, int wetPoints)
    {
        FieldState = fieldState;
        HoedPoints = hoedPoints;
        WetPoints = wetPoints;
    }
}

public class FarmingManager : MonoBehaviour
{
    public GameObject[] fieldObjs;
    public List<Planting> planting;
    public List<Growing> growing;
    public List<Plants> plants;
    public List<Fields> fields;

    public void Awake()
    {
        planting = new List<Planting>();
        growing = new List<Growing>();

        if(fieldObjs.Length > 0)
        {
            foreach(GameObject field in fieldObjs)
            {
                planting.Add(field.GetComponent<Planting>());
                growing.Add(field.GetComponent<Growing>());
            }
        }
    }

    public void AddToList()
    {
        plants.Clear();
        fields.Clear();
        if (fieldObjs.Length > 0)
        {
            for (int i = 0; i < fieldObjs.Length; i++)
            {
                if (planting[i].seed != null)
                    plants.Add(new Plants(planting[i].seed, planting[i].curPlantState, growing[i].currentGrowPoints));
                else
                    plants.Add(null);


                fields.Add(new Fields(planting[i].curFieldState, growing[i].currentHoedPoints, growing[i].currentWetPoints));
            }
        }
    }

    public void UpdateFields()
    {
        if (fieldObjs.Length > 0)
        {
            foreach (GameObject field in fieldObjs)
            {
                for(int i = 0; i < field.transform.childCount; i++)
                {
                    Destroy(field.transform.GetChild(i).gameObject);
                }
            }

            for (int i = 0; i < planting.Count; i++)
            {
                if (plants[i].Seed != null)
                {
                    planting[i].seed = plants[i].Seed;
                    planting[i].curPlantState = plants[i].PlantState;
                    growing[i].currentGrowPoints = plants[i].GrowPoints;

                    //planting[i].curFieldState = fields[i].FieldState;
                    //growing[i].currentHoedPoints = fields[i].HoedPoints;
                    //growing[i].currentWetPoints = fields[i].WetPoints;
                    //planting[i].PlantField();
                }
            }

        }
    }
}
