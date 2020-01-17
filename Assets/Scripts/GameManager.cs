using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [Tooltip("초당 CP 증가량")]
    [SerializeField] private float cps;
    [SerializeField] private float myCP;

    [SerializeField] private Character curCharacter;
    public Character CurCharacter { set { curCharacter = value; } }

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        myCP = 50;
    }

    private void Update()
    {
        ClickToMove();
        Upgrade();
        
    }

    private void FixedUpdate()
    {
        myCP += cps * Time.fixedDeltaTime;
    }

    private void ClickToMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (curCharacter && Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                curCharacter.GetComponent<NavMeshAgent>().destination = hit.point;
            }
        }
    }

    private void Upgrade()
    {
        if (curCharacter == null) return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (myCP < 20) return;
            curCharacter.status.ChangeStat(StatusType.ATK, 20);
            myCP -= 20;
        }
    }
}
