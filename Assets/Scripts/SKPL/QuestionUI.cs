using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUI : MonoBehaviour
{
    private GameObject selectionPanel;
    string[] ss = { "1.SKPL�Ŷ��У�˭��˧?����AAAAD��#A.UG|B.jjk|C.lzq|D.qym#A",
        "2.��������ش�ȥ���ԣ���������ʲô����?#A.û����|B.������|C.��ѡ�Ҹ�ɶ|D.��ѡC�ͱ�ѡD#A",
        "3.���̣���꣬���������������š�\nд�֣����飬����ҳ��ҳҳ���ġ���#A.UG��ѧϰ|B.ѧţħ|C.˭ѧѽ|D.�㰮ѧ����#A",
        "4.ÿ���ֲ�����Ҳ��һ�����ɣ�#A.��ʼ����|B.12����|C.2��˯|D.�������#A",
        "5.�Ҳ�������˵��ô���������Ļ���#A.û��ϵ|B.�����˵|C.��������|D.nmsl#D" };
    string[] ��Ŀ = new string[5];
    string[] ѡ�� = new string[5];
    string[] �� = new string[5];
    int indexQuestion = 0;
    Toggle[] toggles = new Toggle[4];
    Dictionary<int, string> dic = new Dictionary<int, string>();//�浥��Toggle��ѡ���
    List<string> answers = new List<string>();//�����е�ѡ����Ĵ�
    string answer = "";
    int score = 0;

    public GameObject nextQuestion;
    public GameObject endQuestion;

    public GameObject oneToTow;

    void Start()
    {
        oneToTow = GameObject.Find("��һ������ǽ��").gameObject;

        Button btn = nextQuestion.GetComponent<Button>();
        btn.onClick.AddListener(() => { this.NextQuestion(); });
        endQuestion.GetComponent<Button>().onClick.AddListener(() => {
            //����������رս���
            QuestionManager.instance.ToGame();
        });

        dic.Add(0,"A");
        dic.Add(1,"B");
        dic.Add(2,"C");
        dic.Add(3,"D");
        selectionPanel = transform.Find("ShowSelection").gameObject;
        for (int i = 0; i < ss.Length; i++)
        {
            string[] sss = ss[i].Split('#'); //sss���õ���ÿһ����� ��Ŀ ѡ�� ��
            ��Ŀ[i] = sss[0];
            ѡ��[i] = sss[1];
            ��[i] = sss[2];
        }
        //���õ�һ��
        selectionPanel.transform.Find("��Ŀ").GetComponent<Text>().text = ��Ŀ[indexQuestion];
        for (int i = 0; i < 4; i++)
        {
            selectionPanel.transform.GetChild(2).GetChild(i).GetChild(1).GetComponent<Text>().text = ѡ��[i].Split('|')[i];
        }

        for (int i = 0; i < 4; i++)
        {
            toggles[i] = selectionPanel.transform.GetChild(2).GetChild(i).GetComponent<Toggle>();
        }

        this.transform.parent.gameObject.SetActive(false);
    }
  
    //��ȡ��---��ѡ�ĸ�ѡ�֮������ĸ���ֵ���ݵ��ֵ��Kֵ��ͨ���ֵ��Kֵ�ҵ���ŵ�ABCD��
    public void Getanswere() {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                answer += dic[i];
            }
        }
       
        //�����һ��������һ���ѡ��
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = false;
        }
       
    }
    //��һ��
    public void NextQuestion() {
        Getanswere();
       
        //û��ѡ��Ͳ����������һ�ε��
        if (string.IsNullOrEmpty(answer))
        {
            return;
        }
        //�Ƚ��𰸴�����
        answers.Add(answer);
        //����գ����ܱ�֤��һ��û��ѡ��ѡ��Ͳ��ܵ����һ��
        answer = "";
        indexQuestion++;
        if (indexQuestion==ss.Length)
        {
            //ͳ�Ƶ÷�
            for (int i = 0; i < 5; i++)
            {
                if (answers[i] == ��[i])
                {
                    score += 20;
                }
            }
            selectionPanel.transform.GetChild(0).gameObject.SetActive(false);
            selectionPanel.transform.GetChild(1).gameObject.SetActive(false);
            selectionPanel.transform.GetChild(2).gameObject.SetActive(false);
            selectionPanel.transform.GetChild(3).gameObject.SetActive(false);
            selectionPanel.transform.GetChild(4).gameObject.SetActive(true);
            selectionPanel.transform.GetChild(5).gameObject.SetActive(true);
            selectionPanel.transform.GetChild(4).GetComponent<Text>().text += score;

            if (score == 100)
            {
                selectionPanel.transform.GetChild(4).GetComponent<Text>().text += "\n�ڶ����ѿ���";
                oneToTow.SetActive(false);
            }

            return;
        }
        else if (indexQuestion==ss.Length-1)
        {
            selectionPanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "�������";
        }
        selectionPanel.transform.Find("��Ŀ").GetComponent<Text>().text = ��Ŀ[indexQuestion];
        for (int i = 0; i < 4; i++)
        {
            selectionPanel.transform.GetChild(2).GetChild(i).GetChild(1).GetComponent<Text>().text = ѡ��[indexQuestion].Split('|')[i];
        }
    }
    public void ShowSelectPanel(bool IsActive) {
        selectionPanel.transform.GetChild(4).GetComponent<Text>().text = "�÷֣�";
        indexQuestion = 0;
        score = 0;
        answer = "";
        answers.Clear();
        selectionPanel.transform.GetChild(0).gameObject.SetActive(true);
        selectionPanel.transform.GetChild(1).gameObject.SetActive(true);
        selectionPanel.transform.GetChild(2).gameObject.SetActive(true);
        selectionPanel.transform.GetChild(3).gameObject.SetActive(true);
        selectionPanel.transform.GetChild(4).gameObject.SetActive(false);
        selectionPanel.transform.GetChild(5).gameObject.SetActive(false);
        selectionPanel.transform.Find("��Ŀ").GetComponent<Text>().text = ��Ŀ[indexQuestion];
        for (int i = 0; i < 4; i++)
        {
            selectionPanel.transform.GetChild(2).GetChild(i).GetChild(1).GetComponent<Text>().text = ѡ��[i].Split('|')[i];
        }
        selectionPanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "��һ��";
        selectionPanel.SetActive(IsActive); 
    }

    void Update()
    {

    }
}
