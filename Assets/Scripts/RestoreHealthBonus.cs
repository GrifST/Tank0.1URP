using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealthBonus : Bonus
{
    protected override void OnTake(Character character)
    {
        character.ResetHelthPoint();
        base.OnTake(character);
    }
}
