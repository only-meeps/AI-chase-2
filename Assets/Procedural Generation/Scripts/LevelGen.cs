using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelItem
{
    public Rect Base;
    public float height;
    public int X;
    public int Y;
    public GameObject Type;
}
public class Stair
{
    public Vector3 size;
    public Vector3 position;
}

public class Levels
{
    public List<LevelItem> levelItems = new List<LevelItem>();
}
public class LevelGen : MonoBehaviour
{
    public bool BuildNavMesh;
    public float MinGameX;
    public float MinGameY;
    public float MaxGameX;
    public float MaxGameY;
    public Material StairMat;
    public GameObject Step;
    public int MaxPlatforms;
    public int MinPlatforms;
    public float MaxWidth;
    public float MaxHeight;
    public float MaxDepth;
    public float MinWidth;
    public float MinHeight;
    public float MinDepth;
    public int Level;
    public int Seed;
    public int AIAmount;
    public List<GameObject> AIPrefabs = new List<GameObject>();
    public float PercentShrink;
    public List<GameObject> LevelItems = new List<GameObject>();
    System.Random rnd;
    public GameObject Ground;
    public GameObject ObjectivePrefab;
    // Start is called before the first frame update
    void Awake()
    {
        Ground.transform.localScale = new Vector3(Mathf.Abs(MinGameX) + MaxGameX + 15, 0.5f, Mathf.Abs(MinGameY) + MaxGameY + 15);
        Ground.transform.position = new Vector3(0, 0, 0);
        MaxGameX = PlayerPrefs.GetFloat("MaxGameWidth");
        MinGameX = PlayerPrefs.GetFloat("MinGameWidth");
        MaxGameY = PlayerPrefs.GetFloat("MaxGameLength");
        MinGameY = PlayerPrefs.GetFloat("MinGameLength");
        MaxWidth = PlayerPrefs.GetFloat("MaxWidth");
        MinWidth = PlayerPrefs.GetFloat("MinWidth");
        MaxHeight = PlayerPrefs.GetFloat("MaxHeight");
        MinHeight = PlayerPrefs.GetFloat("MinHeight");
        MaxDepth = PlayerPrefs.GetFloat("MaxDepth");
        MinDepth = PlayerPrefs.GetFloat("MinDepth");
        MaxPlatforms = PlayerPrefs.GetInt("MaxPlats");
        MinPlatforms = PlayerPrefs.GetInt("MinPlats");
        Level = PlayerPrefs.GetInt("Score");
        Seed = PlayerPrefs.GetInt("Seed");
        rnd = new System.Random(Seed + Mathf.RoundToInt((Level * 45 / 16) - 30));
        UnityEngine.Random.InitState(Seed + Mathf.RoundToInt((Level * 45 / 16) - 30));
        AIAmount = rnd.Next(PlayerPrefs.GetInt("AIMin"), PlayerPrefs.GetInt("AIMax"));
        GenerateWorld();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (SceneManager.GetActiveScene().name == "OutdoorsScene")
        {
            GameObject[] AllObjectives = GameObject.FindGameObjectsWithTag("Objective");
            if (AllObjectives.Length == 0)
            {
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
                if(PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("ObjectiveHighScore") & PlayerPrefs.GetInt("Difficulty") > 1 & PlayerPrefs.GetFloat("Difficulty") < 7)
                {
                    PlayerPrefs.SetInt("ObjectiveHighScore", PlayerPrefs.GetInt("Score"));
                    PlayerPrefs.SetString("ObjectiveHighScoreSeed", PlayerPrefs.GetString("SeedString"));
                }
                SceneManager.LoadScene("OutdoorsScene");
            }
        }
        
    }
    void CombineMesh(MeshFilter[] meshFilters)
    {
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            Destroy(meshFilters[i].gameObject);
            i++;
        }
        GameObject CombinedOBJs = Instantiate(new GameObject());
        CombinedOBJs.AddComponent<MeshFilter>();
        CombinedOBJs.AddComponent<MeshRenderer>();
        CombinedOBJs.GetComponent<MeshRenderer>().material = StairMat;
        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        CombinedOBJs.transform.GetComponent<MeshFilter>().sharedMesh = mesh;
        CombinedOBJs.AddComponent<MeshCollider>();
        CombinedOBJs.GetComponent<MeshCollider>().sharedMesh = mesh;
        CombinedOBJs.transform.gameObject.SetActive(true);
        CombinedOBJs.gameObject.name = "Platform";
    }

    public void GenerateWorld()
    {
        GameObject[] gameobjs;
        gameobjs = GameObject.FindGameObjectsWithTag("Level");
        if (gameobjs != null)
        {
            for (int i = 0; i < gameobjs.Length; i++)
            {
                Destroy(gameobjs[i]);
            }
        } 
        int Platforms = rnd.Next(MinPlatforms, MaxPlatforms);
        int Objectives = rnd.Next(1, 10);
        List<Vector3> ObjectivePositions = new List<Vector3>();
        List<int> ObjectivePlatforms = new List<int>();
        int AISpawns = new int();
        for (int i = 0; i < Objectives; i++)
        {
            int PlatNum = rnd.Next(1, Platforms);
            while (ObjectivePlatforms.Contains(PlatNum))
            {
                PlatNum = rnd.Next(1, Platforms);
            }
            ObjectivePlatforms.Add(PlatNum);
        }
        if(PlayerPrefs.GetInt("Difficulty") == 6)
        {
            AISpawns = rnd.Next(10, 20);
        }
        else
        {
            AISpawns = rnd.Next(4, 10);
        }

        List<Vector3> AISpawnPositions = new List<Vector3>();
        List<int> AISPawnPoints = new List<int>();
        int PlayerSpawnPlatform = rnd.Next(1, Platforms);
        for (int i = 0; i < AISpawns; i++)
        {
            int PlatNum = rnd.Next(1, Platforms);
            while (AISPawnPoints.Contains(PlatNum) || PlatNum == PlayerSpawnPlatform)
            {
                PlatNum = rnd.Next(1, Platforms);
            }
            AISPawnPoints.Add(PlatNum);
        }
        List<LevelItem> platform = new List<LevelItem>();

        Vector3 PlayerSpawnPoint = new Vector3();
        while (platform.Count < Platforms)
        {
            float PosX = UnityEngine.Random.Range(MinGameX, MaxGameX);
            float PosY = UnityEngine.Random.Range(MinGameY, MaxGameY );

            Rect Base = new Rect(PosX, PosY, UnityEngine.Random.Range(MinWidth, MaxWidth), UnityEngine.Random.Range(MinDepth, MaxDepth));
            /*
            bool FoundOverlap = false;
            for (int j = 1; j < platform.Count; ++j)
            {
                if (Base.Overlaps(platform[j].Base))
                {
                    FoundOverlap = true;
                }
            }

            if (FoundOverlap)
            {
                Debug.Log("Overlap found");
                continue;
            }
            */
            LevelItem levelItem = new LevelItem();
            levelItem.Base = Base;
            levelItem.height = UnityEngine.Random.Range(MinHeight, MaxHeight);

            levelItem.Type = LevelItems[rnd.Next(0, LevelItems.Count)];

            platform.Add(levelItem);
        }
        int k = 0;
        List<GameObject> InstantiatedGameOBJS = new List<GameObject>();
        foreach (var p in platform)
        {
            GameObject InstatiatedObj = Instantiate(p.Type, new Vector3(p.Base.x, 0, p.Base.y), Quaternion.identity);
            float width = p.Base.width * (1.0F - PercentShrink / 100.0F);
            float depth = p.Base.height * (1.0F - PercentShrink / 100.0F);
            InstatiatedObj.transform.localScale = new Vector3(width, p.height, depth);
            InstatiatedObj.transform.position = new Vector3(p.Base.x, p.height / 2, p.Base.y);
            InstantiatedGameOBJS.Add(InstatiatedObj);
            if (ObjectivePlatforms.Contains(k) && SceneManager.GetActiveScene().name == "OutdoorsScene")
            {
                ObjectivePositions.Add(new Vector3(p.Base.x, 0, p.Base.y));
            }
            if (k == PlayerSpawnPlatform)
            {
                PlayerSpawnPoint = new Vector3(p.Base.x, 0, p.Base.y);
            }
            if (AISPawnPoints.Contains(k))
            {
                AISpawnPositions.Add(new Vector3(p.Base.x, 0, p.Base.y));
            }
            if (p.height > Step.transform.lossyScale.y * 2)
            {
                GenerateStairs(InstatiatedObj, Step);
            }
            k++;
        }
        GameObject[] foundplats = GameObject.FindGameObjectsWithTag("Platform");
        if (SceneManager.GetActiveScene().name == "OutdoorsScene")
        {
            for (int i = 0; i < Objectives; i++)
            {
                    for (int l = 0; l < foundplats.Length; l++)
                    {
                        var p = foundplats[l];
                        if (BoundingBoxCheck(p.transform, new Vector2( ObjectivePositions[i].x, ObjectivePositions[i].z)))
                        {
                            ObjectivePositions[i] = new Vector3(ObjectivePositions[i].x, MathF.Max(ObjectivePositions[i].y, p.transform.lossyScale.y), ObjectivePositions[i].z);
                        }
                    }
                

                GameObject Objective = Instantiate(ObjectivePrefab, new Vector3(ObjectivePositions[i].x, ObjectivePositions[i].y + 1, ObjectivePositions[i].z), Quaternion.identity);
                Objective.name = "Objective";
                Objective.tag = "Objective";
            }
        }
        for (int i = 0; i < AISpawns; i++)
        {
                for (int l = 0; l < foundplats.Length; l++)
                {
                    var p = foundplats[l];
                    if (BoundingBoxCheck(p.transform, new Vector2(AISpawnPositions[i].x, AISpawnPositions[i].z)))
                    {
                        Debug.Log("BoundingBoxCheck");
                        Debug.Log(p.transform.lossyScale.y);
                        AISpawnPositions[i] = new Vector3(AISpawnPositions[i].x, Mathf.Max(AISpawnPositions[i].y, p.transform.lossyScale.y), AISpawnPositions[i].z);
                    }
                }
            

            GameObject AISpawn = Instantiate(new GameObject(), new Vector3(AISpawnPositions[i].x, AISpawnPositions[i].y + 2, AISpawnPositions[i].z), Quaternion.identity);
            AISpawn.name = "AI Spawn Point";
            AISpawn.tag = "AI Spawn Point";
        }
            for (int l = 0; l < foundplats.Length; l++)
            {
                var p = foundplats[l];
                if (BoundingBoxCheck(p.transform, new Vector2(PlayerSpawnPoint.x, PlayerSpawnPoint.z)))
                {
                    PlayerSpawnPoint.y = MathF.Max(PlayerSpawnPoint.y, p.transform.lossyScale.y);
                }
            }
        

        GameObject PlayerSpawn = Instantiate(new GameObject(), new Vector3(PlayerSpawnPoint.x, PlayerSpawnPoint.y + 2, PlayerSpawnPoint.z), Quaternion.identity);
        PlayerSpawn.name = "Spawn Point";
        PlayerSpawn.tag = "Spawn Point";
        
            for (int i = 0; i < AIAmount; i++)
            {
                Instantiate(AIPrefabs[rnd.Next(0, AIPrefabs.Count)], GameObject.FindGameObjectsWithTag("AI Spawn Point")[rnd.Next(0, GameObject.FindGameObjectsWithTag("AI Spawn Point").Length)].transform).SetActive(true);
                Debug.Log("Spawned An AI");
            }
         
        //CheckForNavMeshLink();
        if (BuildNavMesh)
        {
            NavMeshSurface navMeshSurface = GameObject.Find("Navmesh Updater").GetComponent<NavMeshSurface>();
            navMeshSurface.BuildNavMesh();
        }

    }
    public bool IsInbetween(float num, float LowerVal, float HigherVal)
    {
        return num >= LowerVal && num <= HigherVal;
    }
    public bool BoundingBoxCheck(Transform Main, Vector2 Point)
    {
        float MainWidth = Main.lossyScale.x;
        float MainDepth = Main.lossyScale.z;
        float MainHeight = Main.lossyScale.y;
        float MainX = Main.position.x;
        float MainY = Main.position.z;
        return IsInbetween(Point.x, MainX - MainWidth / 2, MainX + MainWidth / 2) && IsInbetween(Point.y, MainY - MainDepth / 2, MainY + MainDepth / 2);
    }

    public void GenerateStairs(GameObject TargetOBJ, GameObject Step)
    {
        float TargetHeight = TargetOBJ.transform.lossyScale.y;
        float TargetWidth = TargetOBJ.transform.lossyScale.x;
        float TargetLength = TargetOBJ.transform.lossyScale.z;
        float TargetX = TargetOBJ.transform.position.x;
        float TargetY = TargetOBJ.transform.position.y;
        float TargetZ = TargetOBJ.transform.position.z;

        List<Stair> Stairs = new List<Stair>();

        const float StepDepthDesired = 1.0F;
        const float StepLength = 4.0F;
        const float StepHeight = 1.0F;

        double StepCountX = Math.Floor(TargetWidth / StepDepthDesired);
        float StepDepthX = StepDepthDesired + Convert.ToSingle(((TargetWidth % StepDepthDesired) / StepCountX));

        double StepCountZ = Math.Floor(TargetLength / StepDepthDesired);
        float StepDepthZ = StepDepthDesired + Convert.ToSingle(((TargetLength % StepDepthDesired) / StepCountZ));

        float StepPosX = (TargetWidth / 2.0F) - (StepDepthX / 2.0F) + TargetX;
        float StepPosY = (TargetHeight / 2.0F) - (StepHeight / 2.0F) - TargetY;
        float StepPosZ = (TargetLength / 2.0F) + (StepLength / 2.0F) + TargetZ;

        while (StepPosY < TargetHeight)
        {
            StepPosX = (TargetWidth / 2.0F) - (StepDepthX / 2.0F) + TargetX;
            int count = 0;
            while ((count++ < StepCountX) && (StepPosY < TargetHeight))
            {
                Stair Stair = new Stair();
                Stair.size = new Vector3(StepDepthX, StepHeight, StepLength);
                Stair.position = new Vector3(StepPosX, StepPosY, StepPosZ);
                Stairs.Add(Stair);

                StepPosY += StepHeight;
                StepPosX -= StepDepthX;
            }

            // landing
            if (StepPosY < TargetHeight)
            {
                StepPosX -= (StepLength / 2.0F) - (StepDepthX / 2.0F);

                Stair Stair = new Stair();
                Stair.size = new Vector3(StepLength, StepHeight, StepLength);
                Stair.position = new Vector3(StepPosX, StepPosY, StepPosZ);
                Stairs.Add(Stair);

                StepPosY += StepHeight;
            }

            StepPosZ = (TargetLength / 2.0F) - (StepDepthZ / 2.0F) + TargetZ;
            count = 0;
            while ((count++ < StepCountZ) && (StepPosY < TargetHeight))
            {
                Stair Stair = new Stair();
                Stair.size = new Vector3(StepLength, StepHeight, StepDepthZ);
                Stair.position = new Vector3(StepPosX, StepPosY, StepPosZ);
                Stairs.Add(Stair);

                StepPosY += StepHeight;
                StepPosZ -= StepDepthZ;
            }

            // landing
            if (StepPosY < TargetHeight)
            {
                StepPosZ -= (StepLength / 2.0F) - (StepDepthZ / 2.0F);

                Stair Stair = new Stair();
                Stair.size = new Vector3(StepLength, StepHeight, StepLength);
                Stair.position = new Vector3(StepPosX, StepPosY, StepPosZ);
                Stairs.Add(Stair);

                StepPosY += StepHeight;
            }


            StepPosX = ((TargetWidth / 2.0F) + (StepDepthX / 2.0F) + TargetX) - TargetWidth;
            count = 0;
            while ((count++ < StepCountX) && (StepPosY < TargetHeight))
            {
                Stair Stair = new Stair();
                Stair.size = new Vector3(StepDepthX, StepHeight, StepLength);
                Stair.position = new Vector3(StepPosX, StepPosY, StepPosZ);
                Stairs.Add(Stair);

                StepPosY += StepHeight;
                StepPosX += StepDepthX;
            }

            //landing
            if (StepPosY < TargetHeight)
            {
                StepPosX += (StepLength / 2.0F) - (StepDepthX / 2.0F);

                Stair Stair = new Stair();
                Stair.size = new Vector3(StepLength, StepHeight, StepLength);
                Stair.position = new Vector3(StepPosX, StepPosY, StepPosZ);
                Stairs.Add(Stair);

                StepPosY += StepHeight;
            }

            StepPosZ = ((TargetLength / 2.0F) + (StepDepthZ / 2.0F) + TargetZ) - TargetLength;
            count = 0;
            while ((count++ < StepCountZ) && (StepPosY < TargetHeight))
            {
                Stair Stair = new Stair();
                Stair.size = new Vector3(StepLength, StepHeight, StepDepthZ);
                Stair.position = new Vector3(StepPosX, StepPosY, StepPosZ);
                Stairs.Add(Stair);

                StepPosY += StepHeight;
                StepPosZ += StepDepthZ;
            }

            //landing
            if (StepPosY < TargetHeight)
            {
                StepPosZ += (StepLength / 2.0F) - (StepDepthX / 2.0F);

                Stair Stair = new Stair();
                Stair.size = new Vector3(StepLength, StepHeight, StepLength);
                Stair.position = new Vector3(StepPosX, StepPosY, StepPosZ);
                Stairs.Add(Stair);

                StepPosY += StepHeight;
            }

        }

        MeshFilter[] Meshes = new MeshFilter[Stairs.Count];
        for (int t = 0; t < Stairs.Count; t++)
        {
            Meshes[t] = GameObject.CreatePrimitive(PrimitiveType.Cube).GetComponent<MeshFilter>();
            Meshes[t].gameObject.transform.localScale = Stairs[t].size;
            Meshes[t].gameObject.transform.position = Stairs[t].position;
        }
        CombineMesh(Meshes);

    }


    public void CheckForNavMeshLink()
    {
        GameObject[] currentObj;
        currentObj = GameObject.FindGameObjectsWithTag("Level");

        for (int k = 0; k < currentObj.Length; k++)
        {
            GameObject[] gameObject;
            gameObject = GameObject.FindGameObjectsWithTag("Level");
            int CurrentConnections = new int();
            GameObject NearestObj = new GameObject();
            if (gameObject.Length > 0)
            {
                for (int i = 0; i < gameObject.Length; i++)
                {
                    if (Vector3.Distance(currentObj[k].transform.position, gameObject[i].transform.position) < 50 & gameObject[i].transform.localScale.y < currentObj[k].transform.localScale.y)
                    {
                        CurrentConnections++;
                    }
                    else if (Vector3.Distance(gameObject[i].transform.position, currentObj[k].transform.position) < Vector3.Distance(currentObj[k].transform.position, NearestObj.transform.position) || NearestObj.transform.position == null)
                    {
                        NearestObj = gameObject[i];
                    }
                }
                if (CurrentConnections < 1)
                {
                    if (NearestObj.transform.localScale.y > currentObj[k].transform.localScale.y)
                    {
                        NearestObj.transform.localScale = new Vector3(UnityEngine.Random.Range(0.1f, 1.5f), currentObj[k].transform.localScale.y - .05f, UnityEngine.Random.Range(0.1f, 1.5f));
                    }
                    else if (Vector3.Distance(NearestObj.transform.position, currentObj[k].transform.position) > 50)
                    {
                        if (currentObj[k].transform.position.x > NearestObj.transform.position.x)
                        {
                            NearestObj.transform.Translate(Vector3.left);
                        }
                        else if (currentObj[k].transform.position.x < NearestObj.transform.position.x)
                        {
                            NearestObj.transform.Translate(Vector3.right);
                        }
                        else if (currentObj[k].transform.position.y > NearestObj.transform.position.y)
                        {
                            NearestObj.transform.Translate(Vector3.forward);
                        }
                        else if (currentObj[k].transform.position.y < NearestObj.transform.position.y)
                        {
                            NearestObj.transform.Translate(Vector3.back);
                        }
                    }
                    CurrentConnections += 1;
                }

            }
            Debug.Log(CurrentConnections);

        }
    }
}
