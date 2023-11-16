using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;


enum PlayerDir
{
    None = 0,
    Left = 1,
    Right = 2,
    Forward = 3,
    Back = 4,
}
public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] GameObject playerBrick;

    [SerializeField] Transform model;


    private string currentAnimName;

    private Vector2 startPos, endPos;

    public LayerMask brickLayer;
    public float speed = 5f;

    private Vector3 targetPos;

    private bool isMoving;

    public int coin = 0;


    private Stack<GameObject> bricks = new Stack<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
        coin = PlayerPrefs.GetInt("coin", 0);
        UIManager.instance.SetCoin(coin);
    }

    public void OnInit()
    {
        transform.position = new Vector3(0.5f, 0, -0.5f);
        targetPos = transform.position;
        transform.rotation = Quaternion.identity;
        RemoveAllBrick();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            if(!isMoving) checkWall(GetVector3Direction(GetDirection()));
        }



        if (Vector3.Distance(transform.position, targetPos) > 0.001f)
        {
            Move();
        }
        else
        {
            isMoving = false;
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        isMoving = true;
    }

    private void Jump()
    {
        ChangeAnim("jump");
        StartCoroutine(Idle());
    }

    private IEnumerator Idle()
    {
        yield return new WaitForSeconds(0.5f);
        ChangeAnim("idle");
    }

    public void Win()
    {
        transform.rotation = new Quaternion(0,180-15,0,0);
        ChangeAnim("win");
    }

    private PlayerDir GetDirection()
    {

        float offsetX = endPos.x - startPos.x;
        float offsetY = endPos.y - startPos.y;
        float distance = 50;

        if(Math.Abs(offsetX) > Math.Abs(offsetY))
        {
            if(offsetX > distance)
            {
                return PlayerDir.Right;
            }
            if(offsetX < -distance)
            {
                return PlayerDir.Left;
            }
        }
        else
        {
            if (offsetY > distance)
            {
                return PlayerDir.Forward;
            }
            if (offsetY < -distance)
            {
                return PlayerDir.Back;
            }
        }
        return PlayerDir.None;
    }

    private Vector3 GetVector3Direction(PlayerDir dir)
    {
        switch (dir)
        {
            case PlayerDir.Left:
                return Vector3.left;

            case PlayerDir.Right:
                return Vector3.right;

            case PlayerDir.Forward:
                return Vector3.forward;

            case PlayerDir.Back:
                return Vector3.back;
        }
        return Vector3.zero;
    } 


    private void OnDrawGizmos()
    {

        //bool isHit = Physics.BoxCast(transform.position, transform.localScale / 2, transform.forward, out hit, transform.rotation, maxDistance, brickLayer);
        bool isHit = Physics.Raycast(targetPos, Vector3.forward, 1f, brickLayer);

        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward);
        }
    }



    private void checkWall(Vector3 dir)
    {
        while (Physics.Raycast(targetPos, dir, 1f, brickLayer))
        {
                targetPos += dir;
        }
    }


    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);

            if (currentAnimName != null)
            {
                anim.ResetTrigger(currentAnimName);
            }

            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void AddBrick()
    {
        GameObject brick = Instantiate(playerBrick, transform.position, playerBrick.transform.rotation);
        
        model.position = model.position + Vector3.up * 0.25f;
        brick.transform.SetParent(model);
        bricks.Push(brick);
        Jump();
    }

    public void RemoveBrick()
    {
        if(bricks.Count > 0) {
            Destroy(bricks.Pop());
            model.position -= Vector3.up * 0.25f;
        }
        else
        {
            targetPos = transform.position;
            GameManager.instance.OnLose();
        }
    }

    public void RemoveAllBrick()
    {
        while(bricks.Count > 0)
        {
           RemoveBrick();
        }
    }

    public void IncreaseCoin(int coin)
    {
        this.coin += coin;
        PlayerPrefs.SetInt("coin", this.coin);
        UIManager.instance.SetCoin(this.coin);
    }
}
