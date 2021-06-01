using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camcontrol : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private void Update()
    {
        if (player == null) return;//пендальф
        //transform.position = Vector3.Lerp(transform.position,
        //   player.transform.position,
        //    Time.deltaTime * 5f);

        var rb = player.GetComponent<Rigidbody2D>();
        offset = Vector3.Lerp(offset, rb.velocity / 5f, 5f * Time.deltaTime);
        transform.position = player.transform.position - offset;
    }
}