using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "New Roller", menuName = "Slot Machine/Roller")]
public class Roller : ScriptableObject
{
    public List<Figure> figures;

    [HideInInspector]
    public Figure figure;

    public enum Figure
    {
        Bell,
        Cherry,
        Berry,
        Orange,
        Watermelon,
        Lemon,
        Grapes
    }
}
