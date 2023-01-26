using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    // ü��
    public int maxHp;  // �ִ� ü��. ����Ƽ ���� ����
    public int currentHp;

    // ����
    public int maxMp;  // �ִ� ����. ����Ƽ���� ����
    public int currentMp;

    // ����
    public int maxDp;  // �ִ� ����. ����Ƽ ���� ����
    public int currentDp;

    // ���� ������
    [SerializeField]
    private int mpIncreaseSpeed;

    // ���� ��ȸ�� ������ �ð�
    [SerializeField]
    private int mpRechargeTime;
    private int currentMpRechargeTime;

    // ���� ����ġ �� �ִ� ����ġ
    public float currentExp;
    public float maxExp;

    // �÷��̾� ����
    public int level = 1;
    // ���� ���� ����
    private bool mpUsed;

    void Update()
    {
        MPRechargeTime();
        MPRecover();
    }

    int CurrentHP
    { 
        get 
        {
            return currentHp;
        }
        set
        {
            if (value < 0)
            {
                int damage = Mathf.Abs(value);
                currentHp = currentHp - damage;

                if(currentHp <= 0)
                {
                    currentHp = 0;
                }
            }
            else
            {
                currentHp = currentHp + value;
                if (currentHp >= maxHp)
                    currentHp = maxHp;
            }
        } 
    }

    int CurrentMP
    {
        get
        {
            return currentMp;
        }
        set
        {
            if (value < 0)
            {
                currentMp = currentMp - value;
            }
            else
            {
                currentMp = currentMp + value;
            }
        }
    }

    int CurrentDP
    {
        get
        {
            return currentDp;
        }
        set
        {
            if (value < 0)
            {
                currentDp = currentDp - value;
            }
            else
            {
                currentDp = currentDp + value;
            }
        }
    }

    public void GSHP(int damage)
    {
        CurrentHP = damage;

        Debug.Log($"HP: {currentHp} ");
    }

    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < maxHp)
        {
            currentHp += _count;
        }
        else
        {
            currentHp = maxHp;
        }
    }

    public void DecreaseHP(int _count)
    {
        if (currentDp > 0)
        {
            DecreaseDP(_count); // ���� ���� ����
            return;
        }
        currentHp -= _count;

        if (currentHp <= 0)
        {
            Debug.Log("HP�� 0�� �Ǿ���!");
        }
    }

    

    public void IncreaseDP(int _count)
    {
        if (currentDp + _count < maxDp)
        {
            currentDp += _count;
        }
        else
        {
            currentDp = maxDp;
        }
    }

    public void DecreaseDP(int _count)
    {
        currentDp -= _count;

        if (currentDp <= 0)
        {
            Debug.Log("�� ����~");
        }
    }

    // ���� �� ���� ����
    public void IncreaseEXP(int _count)
    {
        if (currentExp + _count < maxExp)
        {
            currentExp += _count;
        } // level up! / level up �Ҷ� ĳ���� �ɷ�ġ ���� �ʿ�, ���� �������� ��� 
        else if (currentExp + _count >= maxExp)
        {
            currentExp -= maxExp;
            Player_LevelUp();
            Player_maxExp();
        }
        else
        {
            currentExp = 0;
            Player_LevelUp();
            Player_maxExp();
        }
    }

    public void DecreaseEXP(int _count)
    {
        currentExp -= _count; 

        /*if ( �÷��̾ �׾��� �� )
        {
            Debug.Log("����ġ�� ���Դϴ� �Ф�");
        }*/
    }

    public void IncreaseMana(int _count)
    {
        if (currentMp + _count < maxMp)
        {
            currentMp += _count;
        }
        else
        {
            currentMp = maxMp;
        }
    }


    public void DecreaseMana(int _count)
    {
        mpUsed = true; // ������ ������ Ȯ��
        currentMpRechargeTime = 0; // ��������

        if (currentMp - _count > 0)
        {
            currentMp -= _count;
        }
        else
        {
            currentMp = 0;
        }
    }

    private void MPRechargeTime() // ����� ������ ���� ������~
    {
        if (mpUsed == true) // ������ ���� ��
        {
            if (currentMpRechargeTime < mpRechargeTime) // ������ �� ���� ���� ������ ������ �� �� �ִ� ������ ���� �ð��� �缭 ������ ȸ���ɼ� �ִ��� Ȯ��
            {
                currentMpRechargeTime++; // ��mp���� Ÿ���� mp����Ÿ�ӿ� ������ �� ���� 1�� ������Ŵ �����ϸ� mpused = false
            }
            else
            {
                mpUsed = false;
            }
        }
    }

    private void MPRecover() // ȸ�� Ÿ�̹��� �Ǿ��� ��
    {
        if (!mpUsed && currentMp < maxMp) // ���� ������ �ִ븦 �ȳѴ´ٸ�
        {
            currentMp += mpIncreaseSpeed; // ���� ������ ��ġ��ŭ �����Ӵ� ȸ��
        }
    }

    public int GetCurrentMP() // �� ���� ����
    {
        return currentMp;
    }

    public void Player_maxExp()
    {
        maxExp = level * 100;
    }

    public void Player_LevelUp()
    {
        // ���Ŀ� ��� �� �߰��ص� �ɵ�
        level++; // ���� ����
        UIManager.Instance.levelTxt.text = "Level : " + level;
    }
}
