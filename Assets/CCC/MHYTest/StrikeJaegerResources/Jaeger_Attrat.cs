using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Jaeger_Attrat : MonoBehaviour
{
    public Animator animator;
    public ActionState currentState;
    //移动相关
    public IdleState idleState;
    public NormalAttact normalAttact;
    public NormalAttact02 normalAttact02;
    public SpecialAttact specialAttact;
    //闪避相关
    public Dodge dodge;
    public GameObject dodgePrefab;

    //判定
    internal bool leftMouse_KeyDown;
    internal bool rightMouse_KeyDown;
    internal bool Space_KeyDown;
    public AnimatorStateInfo info;
    public string stateName;
    public void IsTouched()
    {
        Debug.Log("Touched!!!!!!cccccccsc");
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        idleState = new IdleState() { playerC = this };
        normalAttact = new NormalAttact() { playerC = this }; ;
        normalAttact02 = new NormalAttact02() { playerC = this }; ;
        specialAttact = new SpecialAttact() { playerC = this }; ;
        dodge = new Dodge() { playerC = this }; ;
        currentState = idleState;
        currentState.Enter();

    }

    void Update()
    {
        leftMouse_KeyDown = Input.GetMouseButtonDown(0);
        //TODO!  ccc左边ctrl键坏了哈哈
        rightMouse_KeyDown = Input.GetMouseButtonDown(1);
        Space_KeyDown = Input.GetKeyDown(KeyCode.Space);

        //leftControl_KeyDown = Input.GetKeyDown(KeyCode.LeftControl);
        bool input_attact = rightMouse_KeyDown | leftMouse_KeyDown | Space_KeyDown;
        info = this.animator.GetCurrentAnimatorStateInfo(0);
        //if(currentState != idleState && !animator.IsInTransition(0) && info.IsName("Move"))
            bool ifa = info.IsName("Move");
        Debug.Log("currentState != idleState IS " + ifa);
        if((currentState.GetType().ToString() != "idleState") && !animator.IsInTransition(0) && info.IsName("Move"))
        {
            Debug.Log("change idle");
            idleState.Enter();

        }
        if (input_attact)
        {
            currentState.HandleInput();
        }
    }

    
    public bool CanAttact(float start, float end)
    {
        return info.normalizedTime >= start && info.normalizedTime <= end;
    }
    
    //闪避判定
    public bool CanDodge(float start, float end)
    {
        return true;
        //return info.normalizedTime >= start && info.normalizedTime <= end;
    }
    
}
public class Dodge : ActionState
{

    public override void Enter()
    {
        base.Enter();
        playerC.animator.CrossFade("Dodge", 0.1f);
        GameObject dodgePrefab = playerC.dodgePrefab.gameObject;
        Transform spawnPoint = playerC.transform;
        GameObject dodgeClone = Instantiate(dodgePrefab, spawnPoint.position, spawnPoint.rotation);

        Animator dodgeAnimator = dodgeClone.GetComponent<Animator>();
        if (dodgeAnimator != null)
        {
            //dodgeAnimator.Play("Dodge", 0, playerC.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }

        // **等待动画播放完毕后销毁**
        float dodgeAnimLength = playerC.animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(dodgeClone, dodgeAnimLength);

    }
    public override void HandleInput()
    {
        base.HandleInput();
        if (playerC.Space_KeyDown)
            playerC.dodge.Enter();
    }

}

public class SpecialAttact : ActionState
{
    public override void Enter()
    {
        base.Enter();
        playerC.animator.CrossFade("Attact04", 0.1f);
    }

}

public class NormalAttact : ActionState
{
    public override void Enter()
    {
        base.Enter();
        playerC.animator.CrossFade("Attact01", 0.1f);
    }
    public override void HandleInput()
    {
        if (playerC.CanAttact(0.6f, 0.9f))
        {
            base.HandleInput();
            if (playerC.leftMouse_KeyDown)
                playerC.normalAttact02.Enter();
            else if (playerC.rightMouse_KeyDown)
                playerC.specialAttact.Enter();

        }
    }
}

public class NormalAttact02 : ActionState
{
    public override void Enter()
    {
        base.Enter();
        playerC.animator.CrossFade("Attact01_2", 0.1f);
    }
    public override void HandleInput()
    {
        base.HandleInput();
        if (playerC.CanAttact(0.7f, 0.9f))
        {
            if (playerC.rightMouse_KeyDown)
                playerC.specialAttact.Enter();
        }

    }
}


public class IdleState: ActionState
{
    public override void Enter()
    {
        base.Enter();
        playerC.animator.CrossFade("Move", 0.01f);
    }
    public override void HandleInput()
    {
        base.HandleInput();
        if (playerC.leftMouse_KeyDown)
            playerC.normalAttact.Enter();
        else if (playerC.rightMouse_KeyDown)
            playerC.specialAttact.Enter();
        if (playerC.Space_KeyDown && playerC.CanDodge(0,0))
        {
            playerC.dodge.Enter();
        }
    }
}


public class ActionState:MonoBehaviour
{
    public Jaeger_Attrat playerC;
    public virtual void Enter()
    {
        OnEnter();
    }
    protected void OnEnter()
    {
        this.playerC.currentState = this;
        this.playerC.stateName = this.GetType().ToString();
    }
    public virtual void HandleInput()
    {
    }
   
}