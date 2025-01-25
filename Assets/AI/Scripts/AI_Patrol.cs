using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AI_Patrol : MonoBehaviour
{
    private int Patroltarget;
    public NavMeshAgent ai;
    List<GameObject> NearGameobjects = new List<GameObject>();
    public List<Transform> PatrolPath = new List<Transform>();
    //private GameObject Target = null;
    public GameObject aiAgent;
    public float DetectDistance = 20;
    private bool foundTarget = false;

    // Start is called before the first frame update
    //[System.Obsolete]
    [System.Obsolete]
    void Start()
    {
        ai.GetComponent<NavMeshAgent>();
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();
        NearGameobjects = new List<GameObject> (allGameObjects);
    }

    // Update is called once per frame
    void Update()
    {
        NearGameobjects.Clear();
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Objective").Length; i++)
        {
            NearGameobjects.Add(GameObject.FindGameObjectsWithTag("Objective")[i]);
        }
        foreach (GameObject g in NearGameobjects)
        {
            float dist = Vector3.Distance(this.gameObject.transform.position, g.transform.position);
            if (dist < DetectDistance & g.gameObject.tag == "Player")
            {
                ai.SetDestination(g.transform.position);
                //Debug.Log("Going to Destination - " + ai.destination);
                foundTarget = true;
            }
            else if (dist > DetectDistance & g.gameObject.tag == "Player")
            {
                foundTarget = false;
                //Debug.Log("lost Target");
            }
            else if (foundTarget == false)
            {
                if (Mathf.Round(aiAgent.transform.position.x) == Mathf.Round(PatrolPath[Patroltarget].transform.position.x) && Mathf.Round(aiAgent.transform.position.z) == Mathf.Round(PatrolPath[Patroltarget].transform.position.z))
                {
                    Patroltarget = (Patroltarget + 1) % PatrolPath.Count;
                }
                ai.SetDestination(PatrolPath[Patroltarget].position);
                //Debug.Log("Going to Patrol - PatrolCount = " + Patroltarget);
            }
        }
    }
}
