using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker : MonoBehaviour
{
    public SlotMachineManager slotMachineManager;

    public List<MatchingPoint> VShapeMatchingTransformPoints;
    public List<MatchingPoint> WShapeMatchingTransformPoints;

    public List<MatchingPoint> UpperRowHorizontalMatchingPoints;
    public List<MatchingPoint> MiddleRowHorizontalMatchingPoints;
    public List<MatchingPoint> LowerRowHorizontalMatchingPoints;

    public List<Figure> currentMatch = new List<Figure>();

    public List<MatchFound> matchesFound;
    protected MatchFound matchFound;
    protected int matchAmount = 0;

    protected int currentHorizontalPosition = 0;

    public void CheckAllMatching()
    {
        CheckHorizontalMatching(UpperRowHorizontalMatchingPoints);
        CheckHorizontalMatching(MiddleRowHorizontalMatchingPoints);
        CheckHorizontalMatching(LowerRowHorizontalMatchingPoints);

        CheckFixedMatching(VShapeMatchingTransformPoints);
        CheckFixedMatching(WShapeMatchingTransformPoints);

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

    protected void CheckHorizontalMatching(List<MatchingPoint> horizontalMatchingPoints)
    {
        Figure.FigureType? targetFigureType = null;

        for (int i = 0; i < horizontalMatchingPoints.Count; i++)
        {
            //Debug.Log(horizontalMatchingPoints[i].figure.figureType);
            if (targetFigureType == null)
            {
                targetFigureType = horizontalMatchingPoints[i].figure.figureType;
                currentMatch.Add(horizontalMatchingPoints[i].figure);
            }
            else if (horizontalMatchingPoints[i].figure.figureType == targetFigureType)
            {
                currentMatch.Add(horizontalMatchingPoints[i].figure);
                Debug.Log(horizontalMatchingPoints[i].figure.figureType);
                if (i == horizontalMatchingPoints.Count-1)
                {
                    foreach (Figure f in currentMatch)
                    {
                        //Debug.Log(f.figureType);
                        f.animator.SetTrigger("match");
                        matchAmount++;
                    }
                    currentMatch.Clear();
                    targetFigureType = null;
                }
            }
            else
            {
                //Debug.Log("BREAK MATCH");
                if(currentMatch.Count > 1)
                {
                    foreach(Figure f in currentMatch)
                    {
                        f.animator.SetTrigger("match");
                        matchAmount++;
                    }
                }

                currentMatch.Clear();
                targetFigureType = horizontalMatchingPoints[i].figure.figureType;
                currentMatch.Add(horizontalMatchingPoints[i].figure);
            }
        }
        currentMatch.Clear();
    }

    protected void CheckFixedMatching(List<MatchingPoint> matchingPoints)
    {
        Figure.FigureType targetFigureType = matchingPoints[0].figure.figureType;

        for(int i = 1; i < matchingPoints.Count; i++)
        {
            if(matchingPoints[i].figure.figureType != targetFigureType)
            {
                return;
            }
        }

        //If watching has happened, then trigger the matching function
        StartCoroutine(VisualMatchingFeedback(0, matchingPoints)); // Modify value to make score appear
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //CheckFixedMatching(VShapeMatchingTransformPoints);
            CheckHorizontalMatching(UpperRowHorizontalMatchingPoints);
            CheckHorizontalMatching(MiddleRowHorizontalMatchingPoints);
            CheckHorizontalMatching(LowerRowHorizontalMatchingPoints);
        }
    }

}
