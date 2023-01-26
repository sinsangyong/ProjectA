using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questID; // questid ������ ����
    public int questActionIndex; // ���� �������� ����Ʈ �ε����� ������ ����
    public GameObject[] questObjects; // ����Ʈ�� �ʿ��� ������Ʈ���� ������ ����

    Dictionary<int, QuestData> questList; // ����Ʈ ������ ������ ����

    void Start()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("������ ��ȭ", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("�������� ��ƶ�~", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("����Ʈ Ŭ����", new int[] { 0 }));
    }
    
    void NextQuest() // ���� ������ id�� ���������ְ� �ε����� 0���� �ʱ�ȭ�Ͽ� ���� ����Ʈ�� �̾����� ���ش�.
    {
        questID += 10;
        questActionIndex = 0;
    }
    
    void ControlObject() // questobjects�� �ִ� �������� ��Ʈ�� �ϰ� ���� id�� 10�̰� �ε����� 2��� ù���� ���� Ȱ��ȭ���ְ� �ε����� 1�̸� ù���� �� ��Ȱ��ȭ
    {
        switch (questID)
        {
            case 10:
                if (questActionIndex == 2)
                    questObjects[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 1)
                    questObjects[0].SetActive(false);
                break;
        }
    }

    public int GetQuestTalkIndex(int id) // questid�� �ε����� ������ �� ��ȯ
    {
        return questID + questActionIndex;
    }

    public string CheckQuest(int id) // npc�� �÷��̾ ��ȭ�ϴ� ������ ������ �ε����� ���������ְ� �������̶�� �� �� �������ִ� �Լ�
    {
        if (id == questList[questID].npcID[questActionIndex])
        {
            questActionIndex++;
        }

        ControlObject();

        if (questActionIndex == questList[questID].npcID.Length)
        {
            NextQuest();
            Debug.Log(questID);
        }

        return questList[questID].questName;
    }

    public string CheckQuest() // �Ű����� �޶�
    {
        return questList[questID].questName;
    }
}
