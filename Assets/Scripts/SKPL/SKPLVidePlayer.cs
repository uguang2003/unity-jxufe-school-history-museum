using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class SKPLVidePlayer : MonoBehaviour
{
    private VideoPlayer Vp;

    public GameObject ר��;
    void Awake()
    {
        ר�� = GameObject.Find("ר��������ǽ��");
        Vp = GetComponent<VideoPlayer>();
    }
    void Start()
    {

        Vp.loopPointReached += VideoEnd;

        Vp.Play();//������Ƶ
        Vp.Pause();//��ͣ��Ƶ
        //Vp.Stop();//ֹͣ��Ƶ
        //Vp.playbackSpeed = 1;//�����ٶ�
    }
    /// <summary>
    /// ������Ƶ�Ƿ񲥷Ž���������ʱ����
    /// </summary>
    /// <param name="vp"></param>
    void VideoEnd(VideoPlayer vp)
    {
        Vp.Play();//���²�����Ƶ
        if (ר��)
        {
            ר��.SetActive(false);
        }
    }

}
