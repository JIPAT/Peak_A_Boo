// using UnityEngine;
// [RequireComponent(typeof(Rigidbody))]
// public class DiceRoll : MonoBehaviour
// {
//     Rigidbody body;
//     [SerializeField] private float maxRandomForceValue, startRollingForce;
//     private float forceX, forceY, forceZ;
//     public int diceFaceNum;
//     private void Awake()
//     {
//         Initialize();
//     }
//     private void Update()
//     {
//         if (body != null)
//         {
//             if (Input.GetMouseButtonDown(0))
//             {
//                 isTurnActive = false;
//                 RollDice();
//             }
//         }
//     }
//     private void RollDice()
//     {
//         body.isKinematic = false;

//         forceX = Random.Range(0, maxRandomForceValue);
//         forceY = Random.Range(0, maxRandomForceValue);
//         forceZ = Random.Range(0, maxRandomForceValue);

//         body.AddForce(Vector3.up * startRollingForce);
//         body.AddTorque(forceX, forceY, forceZ);
//     }
//     private void Initialize()
//     {
//         body = GetComponent<Rigidbody>();
//         body.isKinematic = true;
//         transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 0);
//     }
// }



using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class DiceRoll : MonoBehaviour
{
    Rigidbody body;
    [SerializeField] private float maxRandomForceValue, startRollingForce;
    private float forceX, forceY, forceZ;
    public int diceFaceNum;
    private PlayerTurn playerTurn; // เพิ่มการอ้างอิงถึง PlayerTurn

    private void Awake()
    {
        Initialize();
        playerTurn = FindObjectOfType<PlayerTurn>(); // ค้นหา PlayerTurn ใน Scene
        if (playerTurn == null)
        {
            Debug.LogError("PlayerTurn component not found in the scene.");
        }
    }

    private void Update()
    {
        if (body != null)
        {
            if (Input.GetMouseButtonDown(0)) // ตรวจสอบการคลิกเมาส์
            {
                playerTurn.isTurnActive = false;  // เริ่มต้นเทิร์นเมื่อคลิก
                RollDice();  // เริ่มการทอยลูกเต๋า
            
            }
        }
    }

    private void RollDice()
    {
        body.isKinematic = false;

        forceX = Random.Range(0, maxRandomForceValue);
        forceY = Random.Range(0, maxRandomForceValue);
        forceZ = Random.Range(0, maxRandomForceValue);

        body.AddForce(Vector3.up * startRollingForce);
        body.AddTorque(forceX, forceY, forceZ);
    }

    private void Initialize()
    {
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 0);
    }
}
