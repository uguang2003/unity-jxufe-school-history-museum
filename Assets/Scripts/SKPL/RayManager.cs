using UnityEngine;
using UnityEngine.Video;
using Whilefun.FPEKit;
using UG666.SKPL;

public class RayManager : MonoBehaviour
{
    QuestionUI qm;

    private float screenW;
    private float screenH;
    private Vector2 screenV2;
    void Start()
    {
        screenW = Screen.width / 2;
        screenH = Screen.height / 2;
        screenV2 = new Vector2(screenW, screenH);
        qm = GameObject.FindObjectOfType<QuestionUI>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenV2);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.name == "StartQuestion")
                {
                    Cursor.visible = true;
                    Time.timeScale = 0.0f;
                    FPEInputManager.Instance.LookSensitivity = Vector2.zero;
                    setCursorVisibility(true);
                    QuestionManager.instance.LookItem();
                }

                if (hit.transform.gameObject.GetComponent<VideoPlayer>())
                {
                    if (hit.transform.gameObject.GetComponent<VideoPlayer>().isPaused)
                    {
                        hit.transform.gameObject.GetComponent<VideoPlayer>().Play();
                    }
                    else
                    {
                        hit.transform.gameObject.GetComponent<VideoPlayer>().Pause();
                    }
                }

                if (hit.collider.tag == "ShowItem")
                {
                    if (!SKPLCutsceneWithUI.Instance.isPlay)
                    {
                        SKPLCutsceneWithUI.Instance.startCutscene(hit.collider.GetComponent<ShowItem>());
                    }
                }
            }

        }
    }



    private void setCursorVisibility(bool visible)
    {
        Cursor.visible = visible;
        if (visible)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
