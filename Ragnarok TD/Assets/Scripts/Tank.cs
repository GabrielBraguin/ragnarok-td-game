using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Melee
{
    protected override void CheckOpponent(GameObject otherObject)
    {
        //base.CheckOpponent(otherObject);

        if (otherObject.GetComponent<Jumper>())
        {
            return;
        }
        else if (otherObject.GetComponent<Attacker>())
        {
            Attack(otherObject);
        }
    }
}
