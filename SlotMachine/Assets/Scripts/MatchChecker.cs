using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class manages the possible checks that may happen

public class MatchChecker : MonoBehaviour
{
    public SlotMachineManager slotMachineManager;

    //All 3 rows variables to check horizontal matches
    public MatchRow upperMatchRow;
    public MatchRow middleMatchRow;
    public MatchRow lowerMatchRow;

    //The 2 optional extra patterns where all figures must be the exact same
    public MatchRow vShapeMatchRow;
    public MatchRow wShapeMatchRow;

    public List<Figure> currentMatch = new List<Figure>();

    protected int matchAmount = 0;
    public MatchScore matchScore;

    protected int currentHorizontalPosition = 0;

    //Method to check all possible patterns
    public void CheckAllMatching()
    {
        CheckHorizontalMatching(upperMatchRow);
        CheckHorizontalMatching(middleMatchRow);
        CheckHorizontalMatching(lowerMatchRow);

        CheckFixedMatching(vShapeMatchRow);
        CheckFixedMatching(wShapeMatchRow);

        StartCoroutine(WaitForSpinButtonInteractableAgain());
    }

    //Coroutine that reenables the Spin Button. It will take a couple seconds if there's at least one match so the desired
    //match animation can be played
    protected IEnumerator WaitForSpinButtonInteractableAgain()
    {
        if(matchAmount > 0)
        { 
            yield return new WaitForSeconds(2.2f);
        }
        else
        {
            yield return null;
        }
        slotMachineManager.spinButton.interactable = true;
        matchAmount = 0;
    }

    //Method to check sequentially the figures for horizontal matching
    protected void CheckHorizontalMatching(MatchRow matchingRow)
    {
        Figure.FigureType? targetFigureType = null;

        matchingRow.credits = 0;

        for (int i = 0; i < matchingRow.matchingPoints.Count; i++)
        {
            //Debug.Log(horizontalMatchingPoints[i].figure.figureType);
            if (targetFigureType == null)
            {
                targetFigureType = matchingRow.matchingPoints[i].figure.figureType;
                currentMatch.Add(matchingRow.matchingPoints[i].figure);
            }
            else if (matchingRow.matchingPoints[i].figure.figureType == targetFigureType)
            {
                currentMatch.Add(matchingRow.matchingPoints[i].figure);
                if (i == matchingRow.matchingPoints.Count-1)
                {
                    VisualMatchingFeedback(currentMatch);
                    matchingRow.credits += matchScore.RetrieveScore(currentMatch[0].figureType, currentMatch.Count);
                    currentMatch.Clear();
                    targetFigureType = null;
                }
            }
            else
            {
                if(currentMatch.Count > 1)
                {
                    VisualMatchingFeedback(currentMatch);
                    matchingRow.credits += matchScore.RetrieveScore(currentMatch[0].figureType, currentMatch.Count);
                }
                currentMatch.Clear();
                targetFigureType = matchingRow.matchingPoints[i].figure.figureType;
                currentMatch.Add(matchingRow.matchingPoints[i].figure);
            }
        }
        if(matchingRow.credits != 0)
        {
            matchingRow.creditsText.text = matchingRow.credits.ToString() + " CREDITS";
            matchingRow.pointsAnimator.SetTrigger("Show");
        }
        currentMatch.Clear();
    }


    //Method to check if all the figures in the special patterns are exactly the same
    protected void CheckFixedMatching(MatchRow matchingRow)
    {
        Figure.FigureType targetFigureType = matchingRow.matchingPoints[0].figure.figureType;

        for(int i = 1; i < matchingRow.matchingPoints.Count; i++)
        {
            if(matchingRow.matchingPoints[i].figure.figureType != targetFigureType)
            {
                return;
            }
        }

       //VisualMatchingFeedback(matchingRow.matchingPoints);
    }

    protected void VisualMatchingFeedback(List<Figure> figureList)
    {
        foreach (Figure f in currentMatch)
        {
            f.animator.SetTrigger("match");
            matchAmount++;
        }
    }
}