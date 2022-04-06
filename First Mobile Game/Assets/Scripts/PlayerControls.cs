using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour 
{
    public GameObject pauseMenu;
    public GameObject gameOver;
    public GameObject player;
    public Rigidbody2D rb;

    public Buttons buttonScript;
    public Boost boostScript;

    private bool triggered = false;
    public bool lockMovement = false;

    public float speed = 25f;
    private float origSpeed;
    public float rotationOffset = 270f;

    private int tapCount;
    private float doubleTapTimer;

    public Animator moveAnimator;

    void Start()
    {
        origSpeed = speed;
        InvokeRepeating("resetVelocity", 0, 0.5f);

    }
    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && !pauseMenu.activeSelf && !gameOver.activeSelf && player.activeSelf && !lockMovement) 
        {
            tapCount++;
        }
        if (tapCount > 0)
        {
            doubleTapTimer += Time.deltaTime;
        }
        if (tapCount >= 2)
        {
            //Debug.Log("Double tap!");

            buttonScript.brake = !buttonScript.brake;
            buttonScript.brakeIcon.SetActive(buttonScript.brake);
            buttonScript.brakeToggle.Play(0);

            doubleTapTimer = 0.0f;
            tapCount = 0;
        }
        if (doubleTapTimer > 0.25f)
        {
            doubleTapTimer = 0f;
            tapCount = 0;
        }

        if (Input.touchCount > 0 && !pauseMenu.activeSelf && !gameOver.activeSelf && player.activeSelf && !lockMovement)
        {
            moveShipTowardsCursor();
            /*
            
            */
            if (!buttonScript.brake)
            moveAnimator.SetBool("isMoving", true);
        }
        else
        {
            moveAnimator.SetBool("isMoving", false);
        }
        if (boostScript.isBoosted && !triggered)
        {
            speed *= 1.5f;
            triggered = true;
        }
        else
        {
            speed = origSpeed;
            triggered = false;
        }

    }

    /*
    private bool isPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    */


    void resetVelocity()
    {
        rb.velocity = new Vector2(0, 0);
    }
    void moveShipTowardsCursor()
    {
        
        Vector3 mousePos = Input.GetTouch(0).position;
        
        
            mousePos.z = 0;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        //Debug.Log(mousePos);
        //if (!((mousePos.x >= 0 && mousePos.x <= Screen.width && mousePos.y >= 0 && mousePos.y <= 120)))
        
            

            mousePos.x -= objectPos.x;
            mousePos.y -= objectPos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;


            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            targetPos.z = 0;


            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));
            if (!buttonScript.brake)
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        

    }



}
