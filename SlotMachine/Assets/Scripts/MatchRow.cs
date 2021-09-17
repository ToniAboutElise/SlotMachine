using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchRow : MonoBehaviour
{
    public List<MatchingPoint> matchingPoints;
    public Animator pointsAnimator;
    public int credits = 0;
    public Text creditsText;
}
