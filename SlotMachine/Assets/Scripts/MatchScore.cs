using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class stores are the values of each figure match, variable depending on the figure amount for each combination

public class MatchScore : MonoBehaviour
{
    public int[] bellPoints = { 50, 75, 100, 100 };
    public int[] berryPoints = { 10, 20, 40, 40 };
    public int[] cherryPoints = { 2, 5, 10, 10 };
    public int[] watermelonPoints = { 20, 30, 60, 60 };
    public int[] orangePoints = { 10, 15, 30, 30 };
    public int[] grapesPoints = { 10, 20, 50, 50 };
    public int[] lemonPoints = { 5, 10, 20, 20 };


    //Method used to retrieve score to then display it on screen
    public int RetrieveScore(Figure.FigureType figureType, int figureAmount)
    {
        int result = 0;
        switch (figureType)
        {
            case Figure.FigureType.Bell:
                result = bellPoints[figureAmount - 2];
                break;
            case Figure.FigureType.Berry:
                result = berryPoints[figureAmount - 2];
                break;
            case Figure.FigureType.Cherry:
                result = cherryPoints[figureAmount - 2];
                break;
            case Figure.FigureType.Watermelon:
                result = watermelonPoints[figureAmount - 2];
                break;
            case Figure.FigureType.Orange:
                result = orangePoints[figureAmount - 2];
                break;
            case Figure.FigureType.Grapes:
                result = grapesPoints[figureAmount - 2];
                break;
            case Figure.FigureType.Lemon:
                result = lemonPoints[figureAmount - 2];
                break;
        }
        return result;
    }
}
