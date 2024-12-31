using UnityEngine;
using Whilefun.FPEKit;

public class ShowItem : FPEInteractableBaseScript
{
    //物件对应的模型序号，用于激活对应需要的检视用模型
    public int ItemNum;
    [Header("物品名字")]
    [Tooltip("这里写物品的名字")]
    public string ItemName;
    [Header("物品介绍")]
    [Tooltip("这里写物品的介绍")]
    public string ItemInfo;

    public AudioClip audioClip;

    protected bool canInteractWithWhileHoldingObject = true;

    public override void Awake()
    {
        base.Awake();
        interactionType = eInteractionType.STATIC;
        gameObject.tag = "ShowItem";
    }

    public override bool interactionsAllowedWhenHoldingObject()
    {
        return canInteractWithWhileHoldingObject;
    }
}
