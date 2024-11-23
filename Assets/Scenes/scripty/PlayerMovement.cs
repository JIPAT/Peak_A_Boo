using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public DiceRoll dice; // ��ҧ�ԧ�֧ DiceRoll script
    public Transform[] pathNodes; // Node ����繨ش���鹷ҧ�ͧ��ҹ
    private int currentNodeIndex = 0; // ���˹觻Ѩ�غѹ�ͧ Player
    private bool isMoving = false;
    public DiceRoll diceRollScript; // �������ҧ�ԧ�֧ʤ�Ի�� DiceRoll
    [SerializeField] private float moveSpeed = 2f; // ��������㹡������͹���

    private void Start()
    {
        // ��駤�ҵ��˹�������鹢ͧ Player
        if (pathNodes.Length > 0)
        {
            transform.position = pathNodes[0].position;
        }
    }

    private void Update()
    {
 
        // ���������͹�������ͼ��١����͡
        if (Input.GetMouseButtonDown(0))
        {
            diceRollScript.RollDice();
            // ����� DiceRoll �ӹǳ˹�����
            if (dice.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                isMoving = true;
                StartCoroutine(MovePlayer(dice.diceFaceNum));
            }
        }
    }

    private System.Collections.IEnumerator MovePlayer(int steps)
    {
        if (isMoving = true)
        {
            for (int i = 0; i < steps; i++)
            {
                // ���� Index ����������ѧ Node �Ѵ�
                currentNodeIndex = (currentNodeIndex + 1) % pathNodes.Length;

                Vector3 targetPosition = pathNodes[currentNodeIndex].position;

                // ����͹�����ѧ���˹觢ͧ Node �Ѩ�غѹ
                while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
            isMoving = false; // ��ش����͹�������Ͷ֧�������
        }

            
    }
}
