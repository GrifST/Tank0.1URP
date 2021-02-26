using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Croshire : MonoBehaviour
{
    [Header("Прицел")]
    [SerializeField] public Slider croshire;
    private Vector3 croshirePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.mousePosition);
        croshire.transform.position = Input.mousePosition;
    }
}
