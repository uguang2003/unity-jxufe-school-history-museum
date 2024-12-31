using UnityEngine;
using Whilefun.FPEKit;

public class ShowItem : FPEInteractableBaseScript
{
    //�����Ӧ��ģ����ţ����ڼ����Ӧ��Ҫ�ļ�����ģ��
    public int ItemNum;
    [Header("��Ʒ����")]
    [Tooltip("����д��Ʒ������")]
    public string ItemName;
    [Header("��Ʒ����")]
    [Tooltip("����д��Ʒ�Ľ���")]
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
