using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RollerBehaviour : MonoBehaviour
{
    public Roller roller;
    public Figure figurePrefab;

    public int currentFigure = 0;

    public Transform figuresContainer;

    public List<Figure> figureInstances;

    public FigureImages figureImages;

    public struct FigureImages
    {
        public Sprite bell;
        public Sprite cherry;
        public Sprite berry;
        public Sprite orange;
        public Sprite watermelon;
        public Sprite lemon;
        public Sprite grapes;
    }

    private void Start()
    {
        InitializeImages();
        SetFigures();
    }

    protected void InitializeImages()
    {
        figureImages.bell = Resources.Load<Sprite>("Figures/bell");
        figureImages.cherry = Resources.Load<Sprite>("Figures/cherry");
        figureImages.berry = Resources.Load<Sprite>("Figures/berry");
        figureImages.orange = Resources.Load<Sprite>("Figures/orange");
        figureImages.watermelon = Resources.Load<Sprite>("Figures/watermelon");
        figureImages.lemon = Resources.Load<Sprite>("Figures/lemon");
        figureImages.grapes = Resources.Load<Sprite>("Figures/grapes");
    }

    public void SetFigures()
    {
        foreach(Roller.Figure f in roller.figures)
        {
            Figure figureInstance = Instantiate(figurePrefab);
            figureInstances.Add(figureInstance);
            figureInstance.transform.SetParent(figuresContainer);
            figureInstance.transform.localPosition = new Vector3(0, -220 * currentFigure, 0);
            currentFigure++;

            switch (f)
            {
                case Roller.Figure.Bell:
                    figureInstance.figureImage.sprite = figureImages.bell;
                    break;
                case Roller.Figure.Cherry:
                    figureInstance.figureImage.sprite = figureImages.cherry;
                    break;
                case Roller.Figure.Berry:
                    figureInstance.figureImage.sprite = figureImages.berry;
                    break;
                case Roller.Figure.Orange:
                    figureInstance.figureImage.sprite = figureImages.orange;
                    break;
                case Roller.Figure.Watermelon:
                    figureInstance.figureImage.sprite = figureImages.watermelon;
                    break;
                case Roller.Figure.Lemon:
                    figureInstance.figureImage.sprite = figureImages.lemon;
                    break;
                case Roller.Figure.Grapes:
                    figureInstance.figureImage.sprite = figureImages.grapes;
                    break;
            }
        }
    }
}
