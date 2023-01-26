using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatUI : MonoBehaviour
{
    // �̱������� ���� ���� ��
    private static CombatUI instance;
    public static CombatUI Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<CombatUI>();
            return instance;
        }
    }

    // �ʿ��� �̹��� �迭
    [SerializeField]
    private Image[] enemyImages;

    //[SerializeField]
   //private TMP_Text enemyName;
    [SerializeField]
    private TMP_Text enemyHP_Text;

    // E_ �� �ٿ��� �� ���� index�� Ȯ����
    private const int E_HP = 0, E_FACE = 1; // ���� index�� �ο��ؾ����� �����
    private Dragon dragon;
    // �� ü��
    public int enemyMaxHp;  // �ִ� ü��. ����Ƽ ���� ����
    public int enemyCurrentHp;
    private Color color;


    private void Start()
    {
        dragon = GameObject.FindGameObjectWithTag("Dragon").GetComponent<Dragon>();
        enemyCurrentHp = enemyMaxHp =1000;
    }
    private void Update()
    {
        
    }
    public void EnemyHPSetting(int hp, int mp = 0)
    {
        enemyCurrentHp = hp;
        enemyMaxHp = hp;
    }

    public void IncreaseEnemyHP(int _count)
    {
        if (enemyCurrentHp + _count < enemyMaxHp)
        {
            enemyCurrentHp += _count;
        }
        else
        {
            enemyCurrentHp = enemyMaxHp;
        }
        enemyHP_Text.text = $"{enemyCurrentHp} / {enemyMaxHp}";
    }

    public void DecreaseEnemyHP(int _count)
    {
        if (enemyCurrentHp <= 0)
            return;

        enemyCurrentHp -= _count;
        enemyImages[E_HP].fillAmount = (float)enemyCurrentHp / enemyMaxHp;

        enemyHP_Text.text = $"{enemyCurrentHp} / {enemyMaxHp}";

        if (enemyCurrentHp <= 0)
        {
            StartCoroutine(dragon.Die());
            Debug.Log("������ �׾���~ (���� �״� �Լ� ����)");
        }
    }
}

