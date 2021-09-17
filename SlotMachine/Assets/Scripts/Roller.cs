using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Class used to create new rollers as scriptable objects so the figure amount can be set in a visual way
//To create a new roller, right click on the project and then select Create -> Slot Machine -> Roller

[CreateAssetMenu(fileName = "New Roller", menuName = "Slot Machine/Roller")]
public class Roller : ScriptableObject
{
    public List<Figure.FigureType> figureType;
}
