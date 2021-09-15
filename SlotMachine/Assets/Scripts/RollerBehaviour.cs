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

    public List<Transform> rollerTransforms;

    protected bool canRoll = false;

    protected RollerStatus rollerStatus = RollerStatus.Idle;

    public float velocity = 1000; //this should go with the roller manager

    protected enum RollerStatus
    {
        Idle,
        Rolling
    }

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
            for(int i = 0; i < rollerTransforms.Count-1; i++)
            { 
                Figure figureInstance = Instantiate(figurePrefab);
                figureInstances.Add(figureInstance);
                figureInstance.transform.SetParent(rollerTransforms[i]);
                figureInstance.transform.localPosition = Vector3.zero;
                currentFigure++;

                CheckFigureTypeToSpawn(roller.figures[i], figureInstance);
            
        }
    }

    protected void CheckFigureTypeToSpawn(Roller.Figure figure, Figure figureInstance)
    {
        switch (figure)
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

    public void AdvanceRoller()
    {
        float rollTime = Random.Range(2, 4);
        StartCoroutine(AllowRoll(rollTime));
    }

    protected IEnumerator AllowRoll(float rollTime)
    {
        canRoll = true;
        yield return new WaitForSeconds(rollTime);
        canRoll = false;
    }

    protected void SpawnNewFigure()
    {
        Figure figureInstance = Instantiate(figurePrefab);
        CheckFigureTypeToSpawn(roller.figures[currentFigure], figureInstance);
        figureInstances.Insert(0, figureInstance);
        figureInstance.transform.SetParent(rollerTransforms[0]);
        figureInstance.transform.localPosition = Vector3.zero;
        if (currentFigure < roller.figures.Count - 1)
        {
            currentFigure++;
        }
        else
        {
            currentFigure = 0;
        }
    }

    protected void DestroyEndFigure()
    {
        Destroy(rollerTransforms[rollerTransforms.Count - 1].GetChild(0).gameObject);
        figureInstances.RemoveAt(figureInstances.Count - 1);
    }

    private void Update()
    {
        if (canRoll == true || rollerStatus == RollerStatus.Rolling)
        {
            rollerStatus = RollerStatus.Rolling;
            figureInstances[0].transform.SetParent(rollerTransforms[1].transform);
            figureInstances[0].transform.localPosition = Vector2.MoveTowards(figureInstances[0].transform.localPosition, Vector2.zero, Time.deltaTime * velocity);
            figureInstances[1].transform.SetParent(rollerTransforms[2].transform);
            figureInstances[1].transform.localPosition = Vector2.MoveTowards(figureInstances[1].transform.localPosition, Vector2.zero, Time.deltaTime* velocity);
            figureInstances[2].transform.SetParent(rollerTransforms[3].transform);
            figureInstances[2].transform.localPosition = Vector2.MoveTowards(figureInstances[2].transform.localPosition, Vector2.zero, Time.deltaTime * velocity);
            figureInstances[3].transform.SetParent(rollerTransforms[4].transform);
            figureInstances[3].transform.localPosition = Vector2.MoveTowards(figureInstances[3].transform.localPosition, Vector2.zero, Time.deltaTime * velocity);

            if (Vector2.Distance(figureInstances[0].transform.localPosition, Vector2.zero) == 0)
            {
                SpawnNewFigure();

                DestroyEndFigure();

                if (canRoll == false)
                {
                    rollerStatus = RollerStatus.Idle;
                }
            }
        }
    }
}
