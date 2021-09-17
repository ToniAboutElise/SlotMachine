using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker : MonoBehaviour
{
    public SlotMachineManager slotMachineManager;

    public MatchRow upperMatchRow;
    public MatchRow middleMatchRow;
    public MatchRow lowerMatchRow;
    public MatchRow vShapeMatchRow;
    public MatchRow wShapeMatchRow;

    //public Animator upperRow

    public List<Figure> currentMatch = new List<Figure>();

    public List<MatchFound> matchesFound;
    protected MatchFound matchFound;
    protected int matchAmount = 0;
    public MatchScore matchScore;

    protected int currentHorizontalPosition = 0;

    public void CheckAllMatching()
    {
        CheckHorizontalMatching(upperMatchRow);
        CheckHorizontalMatching(middleMatchRow);
        CheckHorizontalMatching(lowerMatchRow);

        CheckFixedMatching(vShapeMatchRow);
        CheckFixedMatching(wShapeMatchRow);

        StartCoroutine(WaitForSpinButtonInteractableAgain());
    }

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

    protected void CheckHorizontalMatching(MatchRow matchingRow)
    {
        Figure.FigureType? targetFigureType = null;

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
                    foreach (Figure f in currentMatch)
                    {
                        f.animator.SetTrigger("match");
                        matchAmount++;
                    }
                    //Debug.Log(currentMatch[0].figureType + " " + currentMatch.Count.ToString());
                    //Debug.Log(currentMatch[0].figureType + " " + matchScore.RetrieveScore(currentMatch[0].figureType, currentMatch.Count));
                    slotMachineManager.creditsWon += matchScore.RetrieveScore(currentMatch[0].figureType, currentMatch.Count);
                    currentMatch.Clear();
                    targetFigureType = null;
                }
            }
            else
            {
                if(currentMatch.Count > 1)
                {
                    foreach(Figure f in currentMatch)
                    {
                        f.animator.SetTrigger("match");
                        matchAmount++;
                    }
                    //Debug.Log(currentMatch[0].figureType + " " + currentMatch.Count.ToString());
                    //Debug.Log(currentMatch[0].figureType +" " + matchScore.RetrieveScore(currentMatch[0].figureType, currentMatch.Count));
                    slotMachineManager.creditsWon += matchScore.RetrieveScore(currentMatch[0].figureType, currentMatch.Count);
                }
                currentMatch.Clear();
                targetFigureType = matchingRow.matchingPoints[i].figure.figureType;
                currentMatch.Add(matchingRow.matchingPoints[i].figure);
            }
        }
        currentMatch.Clear();
    }

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

        //If watching has happened, then trigger the matching function
        StartCoroutine(VisualMatchingFeedback(0, matchingRow.matchingPoints)); // Modify value to make score appear
    }

    protected IEnumerator VisualMatchingFeedback(int credits, List<MatchingPoint> matchingPoints)
    {
        foreach (MatchingPoint mp in matchingPoints)
        {
            mp.figure.animator.SetBool("match", true);
            yield return new WaitForSeconds(2);
            mp.figure.animator.SetBool("match", false);
        } 
    }
}