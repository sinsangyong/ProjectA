using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public string bladeName;  // ��Ŭ�̳� �� �� ����. �̸����� ������ ���̴�.
    public float range; // ���� ����. ���� ������ ������ ������ ������
    public int damage; // ���ݷ�
    public float workSpeed; // �۾� �ӵ�
    public float attackDelay;  // ���� ������. ���콺 Ŭ���ϴ� ���� ���� ������ �� �����Ƿ�.
    public float attackDelayA;  // ���� Ȱ��ȭ ����. ���� �ִϸ��̼� �߿��� Į�� �� �ֵѷ����� �� ���� �������� ���� �Ѵ�.
    public float attackDelayB;  // ���� ��Ȱ��ȭ ����. Į�� �ֵθ��� �ִϸ��̼��� ������ ���� �������� ���� �ȵȴ�.

    public Animator anim;  // �ִϸ����� ������Ʈ
    public BoxCollider boxCollider; // �´� �ݶ��̴� ���� ���ϱ�
}