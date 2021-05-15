//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ExtraLife : MonoBehaviour
//{
//    [SerializeField] private int bonusLive;
//    [SerializeField] private GameManager GameManager;
//    public void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.GetComponent<TankController>() != null)
//        {
//          GameManager.LivesScore(bonusLive);
//          Destroy(gameObject);
//        }
//    }
//}
