using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{
    // �������� ���� �� ������ �ֱ�� �� ���� �ʿ�

    [SerializeField]
    private Blade currentBlade; // ���� ������ Į �� Ÿ�� ���� (Į) 
    
    private bool isAttack = false;  // ���� ���� ������ 
    private bool isSwing = false;  // ���� �ֵθ��� ������. isSwing = True �� ���� �������� ������ ���̴�.

    private RaycastHit hitInfo;  // ���� ����(Hand)�� ���� �͵��� ����.

    public static bool isActivate = true;  // Ȱ��ȭ ����

    void Update()
    {
        if (isActivate == true)
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
            {
                // StartCoroutine(AttackCoroutine());
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentBlade.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentBlade.attackDelayA);
        isSwing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentBlade.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentBlade.attackDelay - currentBlade.attackDelayA - currentBlade.attackDelayB);
        isAttack = false;
    }

    IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }

    private bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentBlade.range))
        {
            return true;
        }

        return false;
    }

    public void BladeChange(Blade _blade)
    {
        if (WeaponManager.currentWeapon != null)
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        }

        currentBlade = _blade;
        WeaponManager.currentWeapon = currentBlade.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentBlade.anim; // ���ϸ��̼� �־����

        currentBlade.transform.localPosition = Vector3.zero;
        currentBlade.gameObject.SetActive(true);

        isActivate = true;
    }
}
