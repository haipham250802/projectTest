using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Cinemachine;
public class player : Character
{
    const string IdleName = "Idle";
    const string MoveName = "Move";

    public VirtualCamera _virtualcamera;

    public GameObject CanvasPlayer;
    public FloatingJoystick joystick;
    private bool isFacingRight;

    public Button TabToMoveBtn;

    public GameObject CritterFollow;
    public GameObject UI_ShowALlid;

    public List<GameObject> L_enemy = new List<GameObject>();

    public SkeletonAnimation skeleton;

    private void Start()
    {
        skeleton.AnimationState.SetAnimation(0, IdleName, true);
        joystick.gameObject.SetActive(false);
        isFacingRight = true;
    }
    private void Update()
    {
        MoveCharacter();
    }
    bool isMove = false;
    bool changeAnimIdle = false;
    bool changeAnimMove = false;
    public override void MoveCharacter()
    {
        float MoveX = joystick.Horizontal;
        
        isMove = (MoveX != 0);

        if (isMove)
        {
            changeAnimIdle = false;
            if (!changeAnimMove)
            {
                changeAnimMove = true;
                skeleton.AnimationState.SetAnimation(0, MoveName, true);
            }
            
            transform.position += new Vector3(joystick.Horizontal, joystick.Vertical, 0).normalized * Speed * Time.deltaTime;

            if (MoveX < 0 && isFacingRight)
            {
                Flip();
            }
            if (MoveX > 0 && !isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            changeAnimMove = false;

            if (!changeAnimIdle)
            {
                changeAnimIdle = true;
                skeleton.AnimationState.SetAnimation(0, IdleName, true);
            }
            
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public bool IsZoomOut = false;
    public bool IsZoomIn = false;
    public void TabToMove()
    {
        joystick.gameObject.SetActive(true);
        TabToMoveBtn.gameObject.SetActive(false);
        IsZoomOut = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            UI_ShowALlid.SetActive(true);
            CritterFollow.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            UI_ShowALlid.SetActive(false);
            CritterFollow.SetActive(true);
            IsZoomOut = true;
            IsZoomIn = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("CheckPoint"))
        {
            IsZoomIn = true;
            IsZoomOut = false;
        }
    }
    private void OnApplicationPause(bool pause)
    {
        DataPlayer.SetPos(transform.position);
    }
    private void OnApplicationQuit()
    {
        DataPlayer.SetPos(transform.position);
    }
}
