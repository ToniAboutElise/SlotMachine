using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineManager : MonoBehaviour
{
    public List<RollerBehaviour> rollersList;

    public void Roll()
    {
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
