using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFound : MonoBehaviour
{
    public List<Figure> matchingFigures;

    private void Start()
    {
        matchingFigures = new List<Figure>();
    }
}
