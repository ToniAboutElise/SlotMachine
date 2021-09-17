using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class is used to get the rows containing possible matches and display their score if one or more
//matches take place

public class MatchRow : MonoBehaviour
{
    public List<MatchingPoint> matchingPoints;
    public Animator pointsAnimator;
    public int credits = 0;
    public Text creditsText;
}
