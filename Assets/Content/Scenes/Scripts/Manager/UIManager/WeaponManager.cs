using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static bool isChangeWeapon = false;  // ���� ��ü �ߺ� ���� ����. (True�� ���ϰ�) // ��� ������� �˰� static

    [SerializeField]
    private float changeweaponDelayTime;  // ���� ��ü ������ �ð�. Į�� ������ ���� �����̴� �ð�. �뷫 Weapon_Out �ִϸ��̼� �ð�.
    [SerializeField]
    private float changeweaponEndDelayTime;  // ���� ��ü�� ������ ���� �� �뷫 Weapon_In �ִϸ��̼� �ð�.
    
    [SerializeField]
    private Blade[] blades;  // ��� ������ Į �� ���⸦ ������ �迭
     
    //���� �������� �̸����� ���� ���� ������ �����ϵ��� Dictionary �ڷ� ���� ���.
    private Dictionary<string, Blade> bladeDictionary = new Dictionary<string, Blade>(); // ���� ����� ���� x

    [SerializeField]
    private string currentWeaponType;  // ���� ������ Ÿ�� (Į, �� ���)
    public static Transform currentWeapon;  // ���� ����. static���� �����Ͽ� ���� ��ũ��Ʈ���� Ŭ���� �̸����� �ٷ� ������ �� �ְ� ��.
    public static Animator currentWeaponAnim; // ���� ������ �ִϸ��̼�. static���� �����Ͽ� ���� ��ũ��Ʈ���� Ŭ���� �̸����� �ٷ� ������ �� �ְ� ��. ���� ���ϸ��̼� ���� ����.

    [SerializeField] // ���߿� ���� ���� �������� �߰��ؾ���
    private BladeController bladeController; // Į �϶� BladeController.cs Ȱ��ȭ, �ƴϸ� BladeController.cs ��Ȱ��ȭ

    void Start()
    {
        for (int i = 0; i < blades.Length; i++)
        {
            bladeDictionary.Add(blades[i].bladeName, blades[i]);
        }
    }

    void Update()
    {
        if (!isChangeWeapon)
        {
            /*if (Input.GetKeyDown(KeyCode.Alpha1)) // 1 ������ '�Ǽ�'���� ���� ��ü ����
            {
                StartCoroutine(ChangeWeaponCoroutine("HAND", "�Ǽ�")); // �Ǽ��϶� ���� �������� �̼��� �������� ���ڴ�. // ���� �Ǽ� ��� x 
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) // 2 ������ 'Blade'���� ���� ��ü ����
            {
                StartCoroutine(ChangeWeaponCoroutine("BLADE", "The Bloodthirster")); // �ϴ� �ǹٶ�� 
            }*/
        }
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name)
    {
        isChangeWeapon = true;
        // currentWeaponAnim.SetTrigger("Weapon_Out"); �ִϸ��̼� ���� x

        yield return new WaitForSeconds(changeweaponDelayTime);

        CancelPreWeaponAction();
        WeaponChange(_type, _name);

        yield return new WaitForSeconds(changeweaponEndDelayTime);

        currentWeaponType = _type;
        isChangeWeapon = false;
    }

    void CancelPreWeaponAction()
    {
        switch (currentWeaponType)
        {
            case "BLADE":
                BladeController.isActivate = false;
                break;
        }
    }
    
    void WeaponChange(string _type, string _name) // type�� �ϴ� blade �ϳ���
    {
        if (_type == "BLADE")
        {
            bladeController.BladeChange(bladeDictionary[_name]);
        }
    }
}
