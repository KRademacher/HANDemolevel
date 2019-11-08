using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    private Camera cam;
    private NavMeshAgent agent;

    private RaycastHit hit;
    private bool cuttingTree;
    private bool miningStone;

    [SerializeField]
    float remainingDistance;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        cuttingTree = false;
        miningStone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            if (cuttingTree)
            {
                cuttingTree = false;
                gameManager.ChopTree(hit.transform.gameObject);
            }
            else if (miningStone)
            {
                miningStone = false;
                gameManager.MineStone(hit.transform.gameObject);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                print(hit.transform.tag);
                if (hit.transform.tag == "Tree")
                {
                    cuttingTree = true;
                }
                else if (hit.transform.tag == "Stone")
                {
                    miningStone = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            gameManager.BuildHouse(transform.position + (transform.forward * 5));
        }
    }
}