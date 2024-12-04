using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineFieldInteract : MonoBehaviour,IInteractable,ICanCancel
{
    //引用ScriptableObject
    public PackageItemList packagedata;
    public ItemInfoList iteminfolist;

    public CharacterInfoList cil;

    //引用GameObject和GetComponent
    private GameObject aimobj;
    private GameObject aiminput;
    public GameObject holditem;
    public GameObject cancelInputButton;
    private SpriteRenderer icon;
    public GameObject audioobj;
    public AudioSource sound;
    public GameObject pickaudioobj;
    public AudioSource picksound;
    private Animator anim;

    //变量
    private float _time;
    public float time 
    {
        get => _time;
        set => _time =10.0f;
    }
    public bool _keepactions;
    public bool keepactions
    {
        get => _keepactions;
        set => _keepactions = true;
    }
    public float holdtime;
    public bool canGet; 
    public float offsettime;

    //变量赋值
    void Awake()
    {
        time = 10.0f;
        keepactions = true;
        holdtime=1.0f;
        canGet = true;
        offsettime = 1.0f;
    }
    //执行挖矿事件 从IInteract接口继承
    public void interact(GameObject obj,GameObject input)
    {
        for(int index=1;index<10;index++)
        {
            
            //检测手持稿子
            if(packagedata.packageList[index-1].enable==true && packagedata.packageList[index-1].id==1)
            {
                //检测体力是否足够
                if(cil.power>=6)
                {
                aimobj=obj;
                aiminput=input;

                //禁止控制器
                aiminput.SetActive(false);
                
                //生成取消按钮
                cancelInputButton.SetActive(true);

                //映射audio
                sound = audioobj.GetComponent<AudioSource>();
                sound.Stop();
                //audio依序播放 （待修改成 变量 参数0.8f是挖矿效率 1.5f是动画时间 10.0f是挖矿时间$$$
                for(float i=0;i<10.0f-1.5f/0.8f;i+=1.5f/0.8f)
                {
                    Invoke("PlayAudio",i);
                }
                //挖矿完成事件->执行获得物品事件（待修改成 变量 参数10.0f是挖矿时间$$$
                Invoke("getItem",10.0f);

                //映射动画
                anim = aimobj.GetComponent<Animator>();
                //动画速度乘于挖矿效率 动画状态改为挖矿状态
                anim.speed = iteminfolist.itemlist[packagedata.packageList[index-1].id-1].efficiency;
                anim.SetBool("OnMine",true);

                //当检测到有稿子之后 不再执行后续方法
                return;
                }
                //体力不够时发送TextMeshProUGUI（未制作$$$
                else{
                    print("体力不足");
                    return;
                }
            }
        }
        //未检测到稿子发送TextMeshProUGUI（未制作$$$
        print ("没有选中镐子");
    }
    //播放声音方法 参数offsettime为偏移
    private void PlayAudio()
    {
        sound.PlayDelayed(offsettime);
    }
    //完成挖矿事件执行获得物品事件
    private void getItem()
    {  
        //关闭取消按钮
        cancelInputButton.SetActive(false);
        //动画重置速度
        Animator anim = aimobj.GetComponent<Animator>(); 
        anim.speed =1;
        //获取物品概率  
        float[] chance = new float[3]{6,3,1};
        int randomItemId = switchItemId(random(chance));
        //扣除体力
        cil.power -= 6;
//================================================================================================================
    //每次情况需要遍历背包一次

        //情况一：物品有同类在物品栏中（待修改 debug：未设置上限 物品超过上限应该从空物品转变为物品
        for(int index=1;index<10;index++)
        {   
            if(packagedata.packageList[index-1].id==randomItemId)
            {
                //物品增加数量（部分重合
                packagedata.packageList[index-1].quantity+=1;
                //执行获得物品动画
                CreateHold(index);
                //此情况符合 跳过后续情况
                return;
            }
        }
        //情况二：物品没有该物品时 同时背包有空物品
        for(int index=1;index<10;index++)
        {
            if(packagedata.packageList[index-1].id==0)
            {
                //物品增加数量（部分重合
                packagedata.packageList[index-1].quantity+=1;
                //需要给空物品转变为物品
                packagedata.packageList[index-1].id=randomItemId;
                //执行获得物品动画
                CreateHold(index);
                //此情况符合 跳过后续情况
                return;
            }
        }
        //情况三：物品没有该物品，同时背包没有空物品$$$（需要执行丢下物品方法
        print("背包已经装不下了");        
        //改变动画状态
        anim.SetBool("OnMine",false);

//================================================================================================================
    }
    //随机数方法 参数为概率列表
    private int random(float[] chance)
    {
        float total = 0;
        foreach (float list in chance)
        {
            total += list;
        }
        float randompoint = Random.value*total;
        for(int i=0;i<chance.Length;i++)
        {
            if(randompoint<=chance[i])
            {
                return i;
            }
            else
            {
                randompoint-=chance[i];
            }
        }
        return chance.Length-1;
    } 
    //转换 将列表一一对应物品ID
    private int switchItemId(int index)
    {
         switch(index)
         {
            case 0:
            return 2;
            case 1:
            return 3;
            case 2:
            return 4;
         }
         return 4;       
    }
    //执行生成举起物品方法
    private void CreateHold(int index)
    {
        //执行拾取动画（物品塞入背包动画（部分重合
        anim.SetBool("GetItem",true);
        Invoke("ItemToBag",1.0f);
        //生成头顶物品 并且在holdtime后去除
        icon = holditem.GetComponent<SpriteRenderer>();
        icon.sprite = Resources.Load<Sprite>("Image/Item/"+packagedata.packageList[index-1].id);
        holditem.SetActive(true); 
        Invoke("ClearHold",holdtime);
    }
    //清除举起的物品 播放塞入背包的音效
    private void ClearHold()
    {
       holditem.SetActive(false); 
       picksound = pickaudioobj.GetComponent<AudioSource>();
       picksound.Play();
    }
    //执行拾取动画方法（物品塞入背包动画方法
    private void ItemToBag()
    {
        //映射变量
        Animator anim = aimobj.GetComponent<Animator>();
        //转变为初始状态
        anim.SetBool("GetItem",false);
        anim.SetBool("OnMine",false);
        //开启控制器
        aiminput.SetActive(true);
    }
    //可挖矿提示框 从IInteract接口继承
    public string getInteractText()
    {
        return "按F挖矿";
    } 
    //取消时执行该方法 从ICancel接口继承
    public void Cancel(){
        CancelInvoke();
        cancelInputButton.SetActive(false);
    }
}
