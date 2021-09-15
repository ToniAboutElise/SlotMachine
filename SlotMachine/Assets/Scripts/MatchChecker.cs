using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker : MonoBehaviour
{
    public SlotMachineManager slotMachineManager;

    public List<Figure> VShapeMatchingFigures;

    protected int currentHorizontalPosition = 0;

    public void CheckAllMatching()
    {

    }

    protected void CheckHorizontalMatching()
    {
        int maxHorizontal = slotMachineManager.rollersList.Count - 1;

        for(int i = 0; i < maxHorizontal; i++)
        {
            //slotMachineManager.rollersList[i].figureInstances[1]
        }

    }

    protected void CheckVShapeMatching()
    {
        /*
        target

        for(int i = 1;)
        */
    }
}
