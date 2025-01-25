using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create3DOBJ : MonoBehaviour
{
    public GameObject Target;
    public Material OBJMat;
    // Start is called before the first frame update
    void Start()
    {
        GenerateStairs(Target);
    }
    public void GenerateStairs(GameObject TargetOBJ)
    {
        List<float> StairsX = new List<float>();
        List<float> StairsY = new List<float>();
        List<float> StairsZ = new List<float>();
        List<float> StairWidths = new List<float>();
        List<float> StairLengths = new List<float>();
        int StairAmount = Mathf.RoundToInt(TargetOBJ.transform.lossyScale.y);
        float TargetHeight = TargetOBJ.transform.lossyScale.y;
        float TargetWidth = TargetOBJ.transform.lossyScale.x;
        float TargetLength = TargetOBJ.transform.lossyScale.z;
        float StepHeight = 1;
        float StepWidth = 1;
        float StepLength = 1;
        float TargetX = TargetOBJ.transform.position.x;
        float TargetY = TargetOBJ.transform.position.y;
        float TargetZ = TargetOBJ.transform.position.z;
        //Debug.Log(TargetLength + " " + TargetHeight + " " + TargetWidth);
        Debug.Log(StairAmount);
        int Side = 0;
        int Loops = 0;
        int i = 0;
        int StairCountWidth = Mathf.RoundToInt(TargetWidth / StepWidth);
        int StairCountLength = Mathf.RoundToInt(TargetLength / StepLength);
        while ((i * StepHeight) < TargetHeight)
        {
            if (Side == 0)
            {
                if ((i - Loops) > StairCountWidth)
                {
                    Side++;
                }
                else if ((i - Loops) == StairCountWidth)
                {
                    StairLengths.Add(4);
                    StairWidths.Add((StepWidth * StairCountWidth) / (StairCountWidth + (StepWidth * (StairCountWidth / 2))));
                    StairsX.Add(((TargetWidth / 2) + TargetX) - (StepWidth * (i - Loops)));
                    StairsY.Add((StepHeight * i) + ((-TargetHeight / 2) + TargetY));
                    StairsZ.Add(((TargetLength / 2) + TargetZ));
                    i++;
                }
                else
                {
                    StairLengths.Add(4);
                    StairWidths.Add((StepWidth * StairCountWidth) / StairCountWidth);
                    StairsX.Add(((TargetWidth / 2) + TargetX) - (StepWidth * (i - Loops)));
                    StairsY.Add((StepHeight * i) + ((-TargetHeight / 2) + TargetY));
                    StairsZ.Add(((TargetLength / 2) + TargetZ));
                    i++;
                }

            }
            else if (Side == 1)
            {
                if (((i - StairCountWidth) - Loops) > StairCountLength)
                {
                    Side++;
                }
                else if (((i - StairCountWidth) - Loops) == StairCountLength)
                {
                    StairWidths.Add(4);
                    StairLengths.Add((StepLength * StairCountLength) / (StairCountLength + (StepLength * (StairCountLength / 2))));
                    StairsX.Add(StairsX[i - 1]);
                    StairsY.Add((StepHeight * i) + ((-TargetHeight / 2) + TargetY));
                    StairsZ.Add((TargetLength / 2) + TargetZ - (StepLength * ((i - Loops) - StairCountWidth)));
                    i++;
                }
                else
                {
                    StairWidths.Add(4);
                    StairLengths.Add((StepLength * StairCountLength) / StairCountLength);
                    StairsX.Add(StairsX[i - 1]);
                    StairsY.Add((StepHeight * i) + ((-TargetHeight / 2) + TargetY));
                    StairsZ.Add((TargetLength / 2) + TargetZ - (StepLength * ((i - Loops) - StairCountWidth)));
                    i++;
                }

            }
            else if (Side == 2)
            {
                if (((i - StairCountLength - StairCountWidth) - Loops) > StairCountWidth)
                {
                    Side++;
                }
                else if (((i - StairCountLength - StairCountWidth) - Loops) == StairCountWidth)
                {
                    StairWidths.Add((StepWidth * StairCountWidth) / (StairCountWidth + (StepWidth * (StairCountWidth / 2))));
                    StairLengths.Add(4);
                    StairsX.Add((TargetX - (TargetWidth / 2)) + (StepWidth * ((i - Loops) - StairCountWidth - StairCountLength)));
                    StairsY.Add((StepHeight * i) + ((-TargetHeight / 2) + TargetY));
                    StairsZ.Add(StairsZ[i - 1]);
                    i++;
                }
                else
                {
                    StairWidths.Add((StepWidth * StairCountWidth) / StairCountWidth);
                    StairLengths.Add(4);
                    StairsX.Add((TargetX - (TargetWidth / 2)) + (StepWidth * ((i - Loops) - StairCountWidth - StairCountLength)));
                    StairsY.Add((StepHeight * i) + ((-TargetHeight / 2) + TargetY));
                    StairsZ.Add(StairsZ[i - 1]);
                    i++;
                }

            }
            else if (Side == 3)
            {
                if (((i - StairCountLength - (StairCountWidth * 2)) - Loops) > StairCountWidth)
                {
                    Side++;
                }
                else if (((i - StairCountLength - (StairCountWidth * 2)) - Loops) == StairCountWidth)
                {
                    StairLengths.Add((StepLength * StairCountLength) / (StairCountLength + (StepLength * (StairCountLength / 2))));
                    StairWidths.Add(4);
                    StairsX.Add(StairsX[i - 1]);
                    StairsY.Add((StepHeight * i) + ((-TargetHeight / 2) + TargetY));
                    StairsZ.Add(TargetZ - (TargetLength / 2) + (StepLength * ((i - Loops) - StairCountLength - StairCountWidth - StairCountWidth)));
                    i++;
                }
                else
                {
                    StairLengths.Add((StepLength * StairCountLength) / StairCountLength);
                    StairWidths.Add(4);
                    StairsX.Add(StairsX[i - 1]);
                    StairsY.Add((StepHeight * i) + ((-TargetHeight / 2) + TargetY));
                    StairsZ.Add(TargetZ - (TargetLength / 2) + (StepLength * ((i - Loops) - StairCountLength - StairCountWidth - StairCountWidth)));
                    i++;
                }

            }
            if (Side > 3)
            {
                Side = 0;
                Loops += (StairCountWidth * 2) + (StairCountLength * 2);
            }
        }
        MeshFilter[] meshFilters = new MeshFilter[i];
        for (int s = 0; s < i; s++)
        {
            meshFilters[s] = GameObject.CreatePrimitive(PrimitiveType.Cube).GetComponent<MeshFilter>();
            meshFilters[s].gameObject.transform.localScale = new Vector3(StairWidths[s], 1, StairLengths[s]);
            meshFilters[s].gameObject.transform.position = new Vector3(StairsX[s], StairsY[s], StairsZ[s]);
        }
        CombineMesh(meshFilters);
        GC.Collect();
        GC.WaitForPendingFinalizers();
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
        CombinedOBJs.GetComponent<MeshRenderer>().material = OBJMat;
        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        CombinedOBJs.transform.GetComponent<MeshFilter>().sharedMesh = mesh;
        CombinedOBJs.AddComponent<MeshCollider>();
        CombinedOBJs.GetComponent<MeshCollider>().sharedMesh = mesh;
        CombinedOBJs.transform.gameObject.SetActive(true);
        CombinedOBJs.gameObject.name = "Platform";
    }
}
