using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreShieldBonus : Bonus
{
    protected override void OnTake(Character character)
    {
        character.ResetShielPoint();
        base.OnTake(character);
    }
}
