using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject treeStump;
    [SerializeField]
    private GameObject house;

    private NavMeshSurface navMeshSurface;

    private int logCount = 0;
    private int rockCount = 0;
    private int goldCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        navMeshSurface = GameObject.FindGameObjectWithTag("Ground").GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChopTree(GameObject tree)
    {
        Instantiate(treeStump, tree.transform.position, tree.transform.rotation);
        ChangeLogs(5);
        Destroy(tree);
    }

    public void MineStone(GameObject stone)
    {
        ChangeRocks(10);
        Destroy(stone);
    }

    public void ChangeLogs(int amount)
    {
        logCount += amount;
        Text logText = GameObject.Find("TextLogs").GetComponent<Text>();
        logText.text = "Logs: " + logCount.ToString();
    }

    public void ChangeRocks(int amount)
    {
        rockCount += amount;
        Text rockText = GameObject.Find("TextRocks").GetComponent<Text>();
        rockText.text = "Stone: " + rockCount.ToString();
    }

    public void ChangeGold(int amount)
    {
        goldCount += amount;
        Text goldText = GameObject.Find("TextGold").GetComponent<Text>();
        goldText.text = "Gold: " + goldCount.ToString();
    }

    public void BuildHouse(Vector3 position)
    {
        int rocksNeeded = 10;
        int logsNeeded = 20;
        if (rockCount >= rocksNeeded && logCount >= logsNeeded)
        {
            ChangeRocks(-rocksNeeded);
            ChangeLogs(-logsNeeded);
            Instantiate(house, new Vector3(position.x, 0, position.z), Quaternion.identity);
            StartCoroutine(TickGold(10));
        }
    }

    private IEnumerator TickGold(float delay)
    {
        for(;;)
        {
            ChangeGold(1);
            yield return new WaitForSeconds(delay);
        }
    }
}