using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] //����ȭ
public class Data
{
    public float exp;
    public float maxExp;
    public float curHP;
    public float curMP;
    public float maxHP;
    public float maxMP;

    public int level;
    public int coin;
    
    public string saveSceneName;

    //public Vector3 savePlayerPos;
    //public Vector3 savePlayerRot;

    // ������ ����ȭ�� �Ұ���. ����ȭ�� �Ұ����� �ֵ��� �ִ�.
    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();
    // ������ ����
    public List<int> quickSlotArrayNumber = new List<int>();
    public List<string> quickSlotItemName = new List<string>();
    public List<int> quickSlotItemNumber = new List<int>();
}




