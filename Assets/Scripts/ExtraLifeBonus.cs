using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifeBonus : Bonus
{
    protected override void OnTake(Character character)
    {
        GameManager.main.AddPlayerLive();
        base.OnTake(character);
    }
}
