using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //数量变量
    public Vector2 movement;
    public float speed = 12.0f;
    public Vector3 toward;
    bool move;
   
    //对象、组件
    Rigidbody2D rb;
    public GameObject InteractText;
    public GameObject PlayerInput;
    public GameObject CancelInput;
    public Collider2D aimcollider;
    public PackageItemList packageitemdata;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    // void OnEnable()
    // {
    //     // if(ih == null)
    //     // {
    //     //     ih = new InputHandle();
    //     //     ih.PC.Move.performed += m => movement = m.ReadValue<Vector2>();
    //     // }
    //     // ih.Enable();
    // }

    void Start()
    {  
        toward = Vector3.down;
    }

#region 输入方法状态机
    void InputActiononEnable()
    {
        print("开始控制器");
        PlayerInput.SetActive(true);        
    }
    void InputActiononDisable()
    {
        print("停止控制器");
        PlayerInput.SetActive(false);
    }
    #endregion

#region 输入映射事件


    public void onMove(InputAction.CallbackContext ctx)
    {
        if(ctx.phase==InputActionPhase.Performed)
        {
            movement = ctx.ReadValue<Vector2>();
            //朝向
            if (movement != Vector2.zero)
            {
                toward = movement;
                toward = toward.normalized;
            }
            if (movement.x != 0 && movement.y != 0)
            {
                //斜向朝向
                if (movement.x > 0)
                {
                    toward = Vector2.right;
                }
                else
                {
                    toward = Vector2.left;
                }
            }
        }
    }
    public void onInteract(InputAction.CallbackContext ctx)
    {
        if(ctx.phase==InputActionPhase.Performed)
        {
            if(aimcollider != null)
            {
                if (aimcollider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.interact(transform.gameObject,PlayerInput);
                };    
            }
        }
    }
    public void onSelect(InputAction.CallbackContext ctx)
    {
        if(ctx.phase==InputActionPhase.Performed)
        {
            string text = ctx.control.ToString();
            int bagpos = int.Parse(text.Substring(14,1));//Key:/Keyboard/1
            for(int index=1;index<10;index++)
            {
                if(index == bagpos)
                {
                    packageitemdata.packageList[index-1].enable = true;              
                }
                else
                {
                    packageitemdata.packageList[index-1].enable = false;
                }
            }
        }    
    }
    public void onCancel(InputAction.CallbackContext ctx)
    {
        if(ctx.phase==InputActionPhase.Performed)
        {   
            Animator animator = GetComponent<Animator>();
            animator.SetBool("OnMine",false);
            InputActiononEnable();
            if(aimcollider != null)
            {
                if (aimcollider.TryGetComponent(out ICanCancel cancancel))
                {
                    cancancel.Cancel();
                };    
            }
            print("执行取消");
        }    
    }
#endregion 

    //逻辑每帧刷新
    void Update()
    {
        if(movement != Vector2.zero)
        {
            move = true;
        }
        else
        {
            move=false;
        }
        //动画执行
        Animator animator = GetComponent<Animator>();
        animator.SetBool("move", move);
        animator.SetFloat("toward.x", toward.x);
        animator.SetFloat("toward.y", toward.y);


        //检测碰撞箱
        Vector2 interactRange = new Vector2(0.01f,0.01f);

        BoxCollider2D thiscollider = GetComponent<BoxCollider2D>();
            
        Vector3 offset = new Vector3(transform.position.x+thiscollider.offset.x+toward.x*1.5f, transform.position.y + thiscollider.offset.y*10+toward.y*1.1f, -5);

        aimcollider = Physics2D.OverlapBox(offset, interactRange,0f, UnityEngine.Physics2D.DefaultRaycastLayers);


        if(aimcollider != null)
        {
            if (aimcollider.TryGetComponent(out IInteractable interactable))
            {
                string text = interactable.getInteractText();
                TextMeshProUGUI tmp = InteractText.GetComponent<TextMeshProUGUI>();
                tmp.text = text;
            };    
        }
        else
        {
            TextMeshProUGUI tmp = InteractText.GetComponent<TextMeshProUGUI>();
            tmp.text = "";  
        }
    }

    //物理每帧刷新
    void FixedUpdate() 
    {
        //位移执行
        if (movement != Vector2.zero)
        {
        Vector2 position = rb.position;
        position += movement * speed * Time.fixedDeltaTime;
        rb.MovePosition(position);
        }
    }


    //碰撞箱演示绘制
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        BoxCollider2D collider2 = GetComponent<BoxCollider2D>();
        Vector3 offset =new Vector3(transform.position.x+collider2.offset.x+toward.x*1.5f, transform.position.y + collider2.offset.y*10+toward.y*1.1f, -5);
        Gizmos.DrawWireCube(offset , new Vector3(0.5f, 0.5f, 0));
    }
}
