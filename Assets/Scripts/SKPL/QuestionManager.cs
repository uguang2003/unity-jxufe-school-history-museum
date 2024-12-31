using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager instance { get; private set; }

    private GameObject MuseumGroup;

    private GameObject FPEInteractionManager;
    private GameObject UIManager;
    private GameObject FPEInputManager;
    private GameObject PlayInputManager;

    private GameObject ��ĿCanvas;

    private void Start()
    {
        instance = this;

        Cursor.visible = false;

        ��ĿCanvas = GameObject.Find("��Ŀ��(Clone)");

        GameObject[] gameObjects = getDontDestroyOnLoadGameObjects();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name == "SKPLHUD(Clone)")
            {
                UIManager = gameObjects[i];
            }
            if (gameObjects[i].name == "FPEInputManager(Clone)")
            {
                FPEInputManager = gameObjects[i];
            }
            if (gameObjects[i].name == "FPEInteractionManager(Clone)")
            {
                FPEInteractionManager = gameObjects[i];
            }
            if (gameObjects[i].name == "FPEPlayerController(Clone)")
            {
                PlayInputManager = gameObjects[i];
            }
            if (gameObjects[i].name == "FPECore")
            {
                MuseumGroup = gameObjects[i];
            }
        }
    }

    private void Update()
    {
        if (��ĿCanvas)
        {
            if (��ĿCanvas.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                ToGame();
            }
        }
    }

    //ǰ�����ӽ���
    public void LookItem()
    {
        ��ĿCanvas.SetActive(true);
        ��ĿCanvas.transform.GetChild(1).GetComponent<QuestionUI>().ShowSelectPanel(true);
        MuseumGroup.SetActive(false);
        UIManager.SetActive(false);
        FPEInputManager.SetActive(false);
        PlayInputManager.SetActive(false);
    }

    //�����������
    public void ToGame()
    {
        ��ĿCanvas.SetActive(false);
        MuseumGroup.SetActive(true);
        UIManager.SetActive(true);
        FPEInputManager.SetActive(true);
        PlayInputManager.SetActive(true);

        Time.timeScale = 1.0f;
        setCursorVisibility(false);

    }

    private void setCursorVisibility(bool visible){
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


    private GameObject[] getDontDestroyOnLoadGameObjects()
    {
        var allGameObjects = new List<GameObject>();
        allGameObjects.AddRange(FindObjectsOfType<GameObject>());
        //�Ƴ����г��������Ķ���
        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            var objs = scene.GetRootGameObjects();
            for (var j = 0; j < objs.Length; j++)
            {
                allGameObjects.Remove(objs[j]);
            }
        }
        //�Ƴ�������Ϊnull�Ķ���
        int k = allGameObjects.Count;
        while (--k >= 0)
        {
            if (allGameObjects[k].transform.parent != null)
            {
                allGameObjects.RemoveAt(k);
            }
        }
        return allGameObjects.ToArray();
    }
}
