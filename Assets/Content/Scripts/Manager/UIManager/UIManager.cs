using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null) 
                _instance = FindObjectOfType<UIManager>();
            return _instance;
        }
    }

    // �ʿ��� �̹��� �迭
    [SerializeField]
    public Image[] images_Gauge;
    public Text levelTxt;

    // �� ���¸� ��ǥ�ϴ� �ε��� hp �� ü��, mp �� ����, dp�� ����, exp�� ����ġ ����
    public const int HP = 0, MP = 1, DP = 2, EXP = 3;

    private void Start()
    {
        levelTxt.text = "Level : " + GameManager.Instance.statusMgr.level.ToString();
    }

    private void Update()
    {
        GaugeUpdate();
    }

    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)GameManager.Instance.statusMgr.currentHp / GameManager.Instance.statusMgr.maxHp;
        images_Gauge[MP].fillAmount = (float)GameManager.Instance.statusMgr.currentMp / GameManager.Instance.statusMgr.maxMp;
        images_Gauge[DP].fillAmount = (float)GameManager.Instance.statusMgr.currentDp / GameManager.Instance.statusMgr.maxDp;
        images_Gauge[EXP].fillAmount = GameManager.Instance.statusMgr.currentExp / GameManager.Instance.statusMgr.maxExp;
    }
}
