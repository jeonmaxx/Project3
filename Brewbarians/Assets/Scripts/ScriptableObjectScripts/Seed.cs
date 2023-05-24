using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable object/Seed")]
public class Seed : ScriptableObject
{
    [Header("Plant Objects")]
    public GameObject Ph01;
    public GameObject Ph02;
    public GameObject Ph03;
    public Item Product;
}
