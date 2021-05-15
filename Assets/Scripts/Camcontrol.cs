using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camcontrol : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        if (player == null) return;//пендальф
        transform.position = Vector3.Lerp(transform.position,
           player.transform.position,
            Time.deltaTime * 1f);
    }
}