using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{


    [Header("Управление Танком")]
    public string keyMoveForward;
    public string keyMoveReverse;
    public string keyRotateRight;
    public string keyRotateLeft;

    [Header("Ссылка на пушку")]
    [SerializeField] private Shoting _shoting;

    [Header("Скорость танка")]
    public float moveAcceleration = 0.1f;
    public float moveSpeedMax = 2.5f;
    // Скрытые скоростные характеристики
    private float moveSpeed = 0f;
    float moveSpeedReverse = 0f;
    float moveDeceleration = 0.20f;

    // Скрытые характеристики поворота танка
    private float rotateSpeedRight = 0f;
    private float rotateSpeedLeft = 0f;
    private float rotateAcceleration = 4f;
    private float rotateDeceleration = 10f;
    private float rotateSpeedMax = 130f;

    [Header("Траки")]
    public Track trackLeft;
    public Track trackRight;

    private bool moveForward = false;
    private bool moveReverse = false;
    private bool rotateRight = false;
    private bool rotateLeft = false;
    
    
    
   
    


    private void Start()
    {
      
       
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _shoting.Shoot();
        }

        rotateLeft = (Input.GetKeyDown(keyRotateLeft)) ? true : rotateLeft;
        rotateLeft = (Input.GetKeyUp(keyRotateLeft)) ? false : rotateLeft;
        if (rotateLeft)
        {
            rotateSpeedLeft = (rotateSpeedLeft < rotateSpeedMax) ? rotateSpeedLeft + rotateAcceleration : rotateSpeedMax;
        }
        else
        {
            rotateSpeedLeft = (rotateSpeedLeft > 0) ? rotateSpeedLeft - rotateDeceleration : 0;
        }
        transform.Rotate(0f, 0f, rotateSpeedLeft * Time.deltaTime);

        rotateRight = (Input.GetKeyDown(keyRotateRight)) ? true : rotateRight;
        rotateRight = (Input.GetKeyUp(keyRotateRight)) ? false : rotateRight;
        if (rotateRight)
        {
            rotateSpeedRight = (rotateSpeedRight < rotateSpeedMax) ? rotateSpeedRight + rotateAcceleration : rotateSpeedMax;
        }
        else
        {
            rotateSpeedRight = (rotateSpeedRight > 0) ? rotateSpeedRight - rotateDeceleration : 0;
        }
        transform.Rotate(0f, 0f, rotateSpeedRight * Time.deltaTime * -1f);

        moveForward = (Input.GetKeyDown(keyMoveForward)) ? true : moveForward;
        moveForward = (Input.GetKeyUp(keyMoveForward)) ? false : moveForward;
        if (moveForward)
        {
            moveSpeed = (moveSpeed < moveSpeedMax) ? moveSpeed + moveAcceleration : moveSpeedMax;
        }
        else
        {
            moveSpeed = (moveSpeed > 0) ? moveSpeed - moveDeceleration : 0;
        }
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);

        moveReverse = (Input.GetKeyDown(keyMoveReverse)) ? true : moveReverse;
        moveReverse = (Input.GetKeyUp(keyMoveReverse)) ? false : moveReverse;
        if (moveReverse)
        {
            moveSpeedReverse = (moveSpeedReverse < moveSpeedMax) ? moveSpeedReverse + moveAcceleration : moveSpeedMax;
        }
        else
        {
            moveSpeedReverse = (moveSpeedReverse > 0) ? moveSpeedReverse - moveDeceleration : 0;
        }
        transform.Translate(0f, moveSpeedReverse * Time.deltaTime * -1f, 0f);

        if (moveForward | moveReverse | rotateRight | rotateLeft)
        {
            trackStart();
        }
        else
        {
            trackStop();
        }

    }

    // Анимация гусянок

    void trackStart()
    {
        trackLeft.animator.SetBool("IsMoving", true);
        trackRight.animator.SetBool("IsMoving", true);
    }

    void trackStop()
    {
        trackLeft.animator.SetBool("IsMoving", false);
        trackRight.animator.SetBool("IsMoving", false);
    }

}
