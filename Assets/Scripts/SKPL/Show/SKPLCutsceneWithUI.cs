using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Whilefun.FPEKit;
using System;
using System.Collections;

namespace UG666.SKPL
{
    public class SKPLCutsceneWithUI : MonoBehaviour
    {
        public float speed = 10f;
        public bool isPlay = false;
        private GameObject cutsceneCanvas;
        private GameObject ItemNameText;
        private GameObject ItemInfoText;
        private GameObject myButton;

        private ShowItem showItemObj;
        private AudioSource audioSource;

        private static SKPLCutsceneWithUI _instance;

        public static SKPLCutsceneWithUI Instance{  get { return _instance; }}


        private void Awake()
        {
            _instance = this;

            audioSource = GetComponent<AudioSource>();
            cutsceneCanvas = transform.Find("作品展示Canvas").gameObject;
            myButton = cutsceneCanvas.transform.Find("Background/OKButton").gameObject;
            ItemNameText = cutsceneCanvas.transform.Find("Background/ItemName").gameObject;
            ItemInfoText = cutsceneCanvas.transform.Find("Background/ItemInfo").gameObject;
            cutsceneCanvas.SetActive(false);
        }

        private void Start()
        {
        }

        public void startCutscene(ShowItem showItem)
        {
            showItemObj = showItem;
            FPEInteractionManagerScript.Instance.BeginCutscene(true);
            cutsceneCanvas.SetActive(true);
            FPEEventSystem.Instance.gameObject.GetComponent<EventSystem>().SetSelectedGameObject(myButton);

            ItemNameText.GetComponent<Text>().text = showItem.ItemName;
            Run(showItem.ItemInfo, ItemInfoText.GetComponent<Text>());

            if (showItem.audioClip)
            {
                audioSource.clip = showItem.audioClip;
                audioSource.Play();
            }

            isPlay = true;
        }

        public void stopCutscene()
        {
            FPEInteractionManagerScript.Instance.EndCutscene(true);
            cutsceneCanvas.SetActive(false);

            if (showItemObj.audioClip)
            {
                audioSource.Stop();
            }
            isPlay = false;
        }

        public void Run(string textToType, Text textLabel)
        {
            StartCoroutine(TypeText(textToType, textLabel));
        }
        IEnumerator TypeText(string textToType, Text textLabel)
        {
            float t = 0;//经过的时间
            int charIndex = 0;//字符串索引值
            while (charIndex < textToType.Length)
            {
                t += Time.deltaTime * speed;//简单计时器赋值给t
                charIndex = Mathf.FloorToInt(t);//把t转为int类型赋值给charIndex
                charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);
                textLabel.text = textToType.Substring(0, charIndex);

                yield return null;
            }
            textLabel.text = textToType;
        }
    }

}
