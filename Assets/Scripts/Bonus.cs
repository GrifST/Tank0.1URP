using System;
using System.Reflection.Emit;
using UnityEngine;

namespace DefaultNamespace
{
    public enum TypeBonus
    {
        destroyAll,
        restorShield,
        restorHealth,
        extraLife

    }


    public class Bonus : MonoBehaviour
    {
        [SerializeField] private TypeBonus typeBonus;
        public Action<Bonus,TypeBonus> MeGet;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var temp = other.GetComponent<TankController>();
            if (temp != null)
            {
                MeGet?.Invoke(gameObject.GetComponent<Bonus>(),typeBonus);
            }
        }
    }
}