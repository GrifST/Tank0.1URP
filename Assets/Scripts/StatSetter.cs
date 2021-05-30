using UnityEngine;
using UnityEngine.UI;

public class StatSetter : MonoBehaviour
{
    [SerializeField] private Slider hpLabel;
    [SerializeField] private Slider shieldLabel;
    private float currentHP;
    private float currentSP;
    public void SetHP(float hp, float maxHp)
    {
        hpLabel.maxValue = maxHp;
        currentHP = hp;
    }
    public void SetSP(float sp, float maxSp)
    {
        shieldLabel.maxValue = maxSp;
        currentSP = sp;
    }

    private void FixedUpdate()
    {
       if(hpLabel.value != currentHP) hpLabel.value = Mathf.MoveTowards(hpLabel.value, currentHP, 1f);
      if(shieldLabel.value != currentSP) shieldLabel.value = Mathf.MoveTowards(shieldLabel.value, currentSP, 1f);
    }
}