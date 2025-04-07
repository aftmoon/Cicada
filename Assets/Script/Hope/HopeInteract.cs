using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopeInteract : MonoBehaviour
{
    // AB的交互
    public GameObject Hope;
    public Animator anim_Hope;
    public GameObject Player;
    public Animator anim_Player;
    public List<GameObject> Aims;
    // 距离状态判断
    internal CurrentDis curDis;
    internal OutDistance outDistance;
    internal InMidDistance inMidDistance;
    internal InDistance inDistance;
    //判定
    internal bool left_KeyDown;
    internal bool right_KeyDown;

    public string stateName;
    public float dis;
    public float disV3;
    public float playerSpeed = 5f;
    public float hopeSpeed = 3f;
    public bool startGame;

    public float anixForce;
    public float upForce;
    public float playUp;
    internal Rigidbody HopeRig;
    internal Rigidbody PlayRig;

    void Awake()
    {
        //todo！
        //如果需要切换anim的话，目前无效果
        anim_Hope = Hope.GetComponent<Animator>();
        anim_Player = Player.gameObject.GetComponent<Animator>();
        // 初始化
        HopeRig = Hope.GetComponent<Rigidbody>();
        PlayRig = Player.GetComponent<Rigidbody>();
        outDistance = new OutDistance() { p01 = this }; ;
        inMidDistance = new InMidDistance() { p01 = this }; ;
        inDistance = new InDistance() { p01 = this }; ;
        curDis = outDistance;
        startGame = false;
        curDis.Enter();


    }

    void Update()
    {
        dis = Mathf.Abs(Hope.transform.position.x - Player.transform.position.x);
        disV3 = Vector3.Distance(Hope.transform.position, Player.transform.position);
        float distance = Mathf.Max(dis, disV3);
        CurrentDis newDis = curDis;
        Debug.Log("dis = " + dis);
        if (10 < distance )
        {
            Debug.Log("state = " + inMidDistance);
            curDis = inMidDistance;
        }
        else if (0 <= distance )
        {
            curDis = inDistance;
        }


        if (curDis != newDis) // 仅在状态变化时调用 Enter()
        {
            curDis = newDis;
            curDis.Enter();
        }
        // todo
        // 左右的判断
        left_KeyDown = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        right_KeyDown = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        Vector2 force_R = Vector2.right * anixForce;
        Vector2 force_L = Vector2.left * anixForce;

        if (left_KeyDown)
        {
            //Player.transform.position += Vector3.left * playerSpeed * Time.deltaTime;
            PlayRig.AddForce(force_L);
        }
        if (right_KeyDown)
        {
            //Player.transform.position -= Vector3.left * playerSpeed * Time.deltaTime;
            PlayRig.AddForce(force_R);

        }
        curDis.Enter();
        curDis.Interact();
    }

}


// 近距离状态
// Hope趋向Aim
// Player自由移动
public class InDistance : CurrentDis
{
    public override void Interact()
    {
        Vector2 direction = p01.Aims[0].transform.position - p01.Hope.transform.position;
        direction.Normalize();
        p01.HopeRig.AddForce(direction * p01.upForce);
        float y = p01.Hope.transform.position.y;
        float newY = Mathf.MoveTowards(p01.Player.transform.position.y, y, p01.playUp*Time.deltaTime);
        p01.Player.transform.position = new Vector3(p01.Player.transform.position.x, newY, p01.Player.transform.position.z);


    //    float hopeSpeedX = 5f; // Hope 在 X 轴上的速度
    //float hopeSpeedY = 6f; // Hope 在 Y 轴上的速度

        //        Vector3 targetPos = p01.Aims[0].transform.position;
        //        Vector3 currentPos = p01.Hope.transform.position;
        //        // 计算 Hope 在 X 和 Y 方向的目标位置
        //        float newX = Mathf.MoveTowards(currentPos.x, targetPos.x, hopeSpeedX * Time.deltaTime);
        //        float newY = Mathf.MoveTowards(currentPos.y, targetPos.y, hopeSpeedY * Time.deltaTime);

        //        // 只在 X 轴或 Y 轴上分别移动
        //        p01.Hope.transform.position = new Vector3(newX, newY, currentPos.z);
        //        p01.Player.transform.position += Vector3.up * hopeSpeedY * Time.deltaTime;
    }
}

//中距离状态
//Hope趋向Player
public class InMidDistance : CurrentDis
{
    public override void Interact()
    {
        Vector2 direction = p01.Player.transform.position - p01.Hope.transform.position;
        direction.Normalize();
        p01.HopeRig.AddForce(direction * p01.upForce);
        //p01.PlayRig.AddForce(direction * p01.upForce);
        //float y = p01.Hope.transform.position.y;
        //float newY = Mathf.MoveTowards(p01.Player.transform.position.y, y, Time.deltaTime);

        //p01.Player.transform.position = new Vector3(p01.Hope.transform.position.x, newY, p01.Hope.transform.position.z);

        //float hopeSpeedX = 20f; // Hope 在 X 轴上的速度
        //float hopeSpeedY = 10f; // Hope 在 Y 轴上的速度
        //float FallenSpeedY = 5f; // Player 在 Y 轴上的速度
        //    Vector3 targetPos = p01.Player.transform.position;
        //    Vector3 currentPos = p01.Hope.transform.position;
        //    // 计算 Hope 在 X 和 Y 方向的目标位置
        //    float newX = Mathf.MoveTowards(currentPos.x, targetPos.x, hopeSpeedX * Time.deltaTime);
        //    float newY = Mathf.MoveTowards(currentPos.y, targetPos.y, hopeSpeedY * Time.deltaTime);

        //    // 只在 X 轴或 Y 轴上分别移动
        //    p01.Hope.transform.position = new Vector3(newX, newY, currentPos.z);
        //    p01.Player.transform.position -= Vector3.up * FallenSpeedY * Time.deltaTime;
    }
}

//距离最远状态下
//单纯左右移动
public class OutDistance : CurrentDis
{

    public override void Enter()
    {
        p01.startGame = true;
        base.Enter();


    }

    public override void Interact()
    {
    }
}

public class CurrentDis : MonoBehaviour
{
    public HopeInteract p01;
    public virtual void Enter()
    {
        OnEnter();
    }
    protected void OnEnter()
    {
        this.p01.curDis = this;
        this.p01.stateName = this.GetType().ToString();
    }
    public virtual void Interact()
    {
    }
}