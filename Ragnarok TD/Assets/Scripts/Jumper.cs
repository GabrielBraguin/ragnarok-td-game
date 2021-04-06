using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : Melee
{    
    protected override void CheckOpponent(GameObject otherObject)
    {
        //base.CheckOpponent(otherObject);

        if (otherObject.GetComponent<Tank>())
        {
            animator.SetTrigger("jumpTrigger");
        }
        else if (otherObject.GetComponent<Defender>())
        {
            Attack(otherObject);
        }
    }
}
