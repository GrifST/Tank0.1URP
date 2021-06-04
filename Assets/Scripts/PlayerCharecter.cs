using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharecter : Character
{
    protected override void Start()
    {
        base.Start();
    }
    void Update()
    {
        if (tower)
        {
            tower.targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Input.GetButton("Fire1"))
            {
                tower.Shot();
            } 
        }
        if (tank)
        {
            tank.vertical = Input.GetAxis("Vertical"); //вперед/назад
            tank.horizontal = Input.GetAxis("Horizontal"); //лево/право
        }
    }
    public override void Kill()
    {
        GameManager.main.OnPlayerDead(this);
        base.Kill();
    }
}
