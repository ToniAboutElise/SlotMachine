using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineManager : MonoBehaviour
{
    public List<RollerBehaviour> rollersList;

    public int creditsWon = 0;

    public int velocity = 1000;

    public Button spinButton;

    public MatchChecker matchChecker;

    public void CheckRollIsPossible()
    {
        foreach(RollerBehaviour r in rollersList)
        {
            if(r.rollerStatus != RollerBehaviour.RollerStatus.Idle)
            {
                return;
            }
        }

        matchChecker.CheckAllMatching();
        Debug.Log(creditsWon);
    }

    public void Roll()
    {
        spinButton.interactable = false;
        StartCoroutine(RollSequentially());
    }

    protected IEnumerator RollSequentially()
    {
        foreach(RollerBehaviour r in rollersList)
        {
            r.AdvanceRoller();
            yield return new WaitForSeconds(0.35f);
        }
    }
}
