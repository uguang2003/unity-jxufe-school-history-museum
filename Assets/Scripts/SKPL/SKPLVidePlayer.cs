using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class SKPLVidePlayer : MonoBehaviour
{
    private VideoPlayer Vp;

    public GameObject 专题;
    void Awake()
    {
        专题 = GameObject.Find("专题厅空气墙组");
        Vp = GetComponent<VideoPlayer>();
    }
    void Start()
    {

        Vp.loopPointReached += VideoEnd;

        Vp.Play();//播放视频
        Vp.Pause();//暂停视频
        //Vp.Stop();//停止视频
        //Vp.playbackSpeed = 1;//播放速度
    }
    /// <summary>
    /// 监听视频是否播放结束，结束时调用
    /// </summary>
    /// <param name="vp"></param>
    void VideoEnd(VideoPlayer vp)
    {
        Vp.Play();//重新播放视频
        if (专题)
        {
            专题.SetActive(false);
        }
    }

}
