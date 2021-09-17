using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class manages figure's type, their sprites and animator

public class Figure : MonoBehaviour
{
    public Image figureImage;
    public Animator animator;

    public FigureType figureType;

    public enum FigureType
    {
        Bell,
        Cherry,
        Berry,
        Orange,
        Watermelon,
        Lemon,
        Grapes
    }
}
