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
    public CurrentDis curDis;
    public OutDistance outDistance;
    public InMidDistance inMidDistance;
    public InDistance inDistance;
    //判定
    internal bool left_KeyDown;
    internal bool right_KeyDown;
    //internal bool Q_KeyDown;

    public string stateName;


    private void Awake()
    {
        //todo！
        //如果需要切换anim的话，目前无效果
        anim_Hope = Hope.GetComponent<Animator>();
        anim_Player = Player.gameObject.GetComponent<Animator>();
        outDistance = new OutDistance() { p01 = this }; ;
        inMidDistance = new InMidDistance() { p01 = this }; ;
        inDistance = new InDistance() { p01 = this }; ;
        curDis = outDistance;
        curDis.Enter();
        

    }

    private void Update()
    {
        // todo
        // 左右的判断
        left_KeyDown = Input.GetMouseButtonDown(0);
        left_KeyDown = Input.GetMouseButtonDown(1);

        bool input = left_KeyDown | left_KeyDown;
        if (input)
        {
            curDis.Interact();
        }

    }

}



// 近距离状态
// Hope趋向Aim
// Player自由移动
public class InDistance: CurrentDis
{
}

//中距离状态
//Hope趋向Player
public class InMidDistance: CurrentDis
{
}

//距离最远状态下
//单纯左右移动
public class OutDistance: CurrentDis
{
}

public class CurrentDis: MonoBehaviour
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