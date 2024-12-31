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

    private GameObject 题目Canvas;

    private void Start()
    {
        instance = this;

        Cursor.visible = false;

        题目Canvas = GameObject.Find("题目组(Clone)");

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
        if (题目Canvas)
        {
            if (题目Canvas.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                ToGame();
            }
        }
    }

    //前往检视界面
    public void LookItem()
    {
        题目Canvas.SetActive(true);
        题目Canvas.transform.GetChild(1).GetComponent<QuestionUI>().ShowSelectPanel(true);
        MuseumGroup.SetActive(false);
        UIManager.SetActive(false);
        FPEInputManager.SetActive(false);
        PlayInputManager.SetActive(false);
    }

    //返回至博物馆
    public void ToGame()
    {
        题目Canvas.SetActive(false);
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
        //移除所有场景包含的对象
        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            var objs = scene.GetRootGameObjects();
            for (var j = 0; j < objs.Length; j++)
            {
                allGameObjects.Remove(objs[j]);
            }
        }
        //移除父级不为null的对象
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
