using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker : MonoBehaviour
{
    public SlotMachineManager slotMachineManager;

    public List<MatchingPoint> VShapeMatchingTransformPoints;
    public List<MatchingPoint> WShapeMatchingTransformPoints;

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

    protected void CheckFixedMatching(List<MatchingPoint> matchingPoints)
    {
        Figure.FigureType targetFigureType = matchingPoints[0].figure.figureType;

        for(int i = 1; i < matchingPoints.Count; i++)
        {
            if(matchingPoints[i].figure.figureType != targetFigureType)
            {
                Debug.Log(targetFigureType + "NONO");
                return;
            }
        }

        //If watching has happened, then trigger the matching function
        VisualMatchingFeedback(0, matchingPoints); // Modify value to make score appear
    }

    protected void VisualMatchingFeedback(int credits, List<MatchingPoint> matchingPoints)
    {
        Debug.Log("MATCH!");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            CheckFixedMatching(VShapeMatchingTransformPoints);
        }
    }

}
