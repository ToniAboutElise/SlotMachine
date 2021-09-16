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

    public List<MatchFound> matchesFound;
    protected MatchFound matchFound;

    protected int currentHorizontalPosition = 0;

    private void Start()
    {
        //matchFound = new MatchFound();
    }

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
        yield return new WaitForSeconds(2);
        slotMachineManager.spinButton.interactable = true;
    }

    protected void CheckHorizontalMatching(List<MatchingPoint> horizontalMatchingPoints)
    {
        Figure.FigureType targetFigureType = Figure.FigureType.Bell;

        for (int i = 0; i < horizontalMatchingPoints.Count; i++)
        {
            Debug.Log(horizontalMatchingPoints[i].figure.figureType);
            if (horizontalMatchingPoints[i].figure.figureType == targetFigureType)
            {
                //targetFigureType = horizontalMatchingPoints[i].figure.figureType;
                //matchFound.matchingFigures.Add(horizontalMatchingPoints[i].figure);
            }
            else
            {
                targetFigureType = horizontalMatchingPoints[i].figure.figureType;
                /*if (matchFound.matchingFigures.Count > 1)
                {
                    //Debug.Log("MATCH" + matchFound.matchingFigures.Count + matchFound.matchingFigures[0].figureType);
                    foreach(Figure f in matchFound.matchingFigures)
                    {
                        //Debug.Log("Figure: " + f);
                        f.animator.SetBool("match", true);
                    }
                    matchesFound.Add(matchFound);
                    targetFigureType = horizontalMatchingPoints[i + 1].figure.figureType;
                }
                //matchFound.matchingFigures.Clear();
                //matchFound.matchingFigures.Add(horizontalMatchingPoints[i].figure);
                */
            }
        }

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
            CheckFixedMatching(VShapeMatchingTransformPoints);
        }
    }

}
