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
            new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z),
            Time.deltaTime * 1f);
    }
}