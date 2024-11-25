using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement1 : MonoBehaviour
{
    public DiceRoll dice; // ��ҧ�ԧ�֧ DiceRoll script
    public Transform[] pathNodes; // Node ����繨ش���鹷ҧ�ͧ��ҹ
    public int currentNodeIndex = 0; // ���˹觻Ѩ�غѹ�ͧ Player
    public bool isMoving = false;
    public int steps;
    public int x;
    public int y;
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
        
    }


    public System.Collections.IEnumerator MovePlayer()
    {
        steps = dice.diceFaceNum;
        Debug.LogError(steps);
        for (int i = 0; i < steps; i++)
            {
            // ���� Index ����������ѧ Node �Ѵ�
            if (currentNodeIndex >= pathNodes.Length - 1)
            {
                currentNodeIndex = pathNodes.Length - 1; // ��͡�����˹��ش������������͡�ش����
                Debug.Log("Player has reached the finish line!");
                break; // ��ش����Թ
            }
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

    public IEnumerator MoveSteps(int stepsToMove)
    {
        int direction = stepsToMove > 0 ? 1 : -1; // ��˹���ȷҧ (�Թ˹�� = 1, �����ѧ = -1)
        stepsToMove = Mathf.Abs(stepsToMove); // �����ӹǹ�����繺ǡ

        for (int i = 0; i < stepsToMove; i++)
        {
            // ����/Ŵ Index �����ȷҧ
            if (direction > 0 && currentNodeIndex < pathNodes.Length - 1)
            {
                currentNodeIndex++;
            }
            else if (direction < 0 && currentNodeIndex > 0)
            {
                currentNodeIndex--;
            }
            else if (currentNodeIndex > pathNodes.Length - 1)
            {
                Debug.Log("Cannot move further in this direction.");
                break; // ��ش�ҡ�������ö�Թ�����
            }

            Vector3 targetPosition = pathNodes[currentNodeIndex].position;

            // ����͹�����ѧ�˹�����
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            Debug.Log($"Player moved to Node {currentNodeIndex}");
        }

        yield return null; // ����� Coroutine ��
        Debug.Log("��");
    }

}
