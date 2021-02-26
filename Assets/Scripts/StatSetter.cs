using UnityEngine;
using UnityEngine.UI;

public class StatSetter : MonoBehaviour
{
    [SerializeField] private Slider hpLabel;
    [SerializeField] private Slider shieldLabel;

    public void SetHP(float hp, float maxHp)
    {
        hpLabel.maxValue = maxHp;
        hpLabel.value = hp;
    }
    public void SetSP(float sp, float maxSp)
    {
        shieldLabel.maxValue = maxSp;
        shieldLabel.value = sp;
    }
}