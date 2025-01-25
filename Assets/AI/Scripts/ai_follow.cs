using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class ai_follow : MonoBehaviour
{

    public Transform Target;
    public NavMeshAgent Agent;
    public int nextPoint;
    public Vector2 point;
    
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name != "TitleScreen")
        {
            Target = GameObject.Find("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "TitleScreen")
        {
            Agent.SetDestination(Target.position);
        }
        else if(SceneManager.GetActiveScene().name == "TitleScreen" & nextPoint > 1)
        {
            Agent.SetDestination(point);
            nextPoint--;
        }
        else if(SceneManager.GetActiveScene().name == "TitleScreen" & nextPoint < 1)
        {
            point = new Vector3(Random.Range(PlayerPrefs.GetInt("MinGameWidth"), PlayerPrefs.GetInt("MaxGameWidth")), 0, Random.Range(PlayerPrefs.GetInt("MinGameLength"), PlayerPrefs.GetInt("MaxGameLength")));
            nextPoint = 10000;
        }

    }
}
