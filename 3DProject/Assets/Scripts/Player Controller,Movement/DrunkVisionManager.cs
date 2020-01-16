using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkVisionManager : MonoBehaviour
{
    public GameObject Player;
    PlayerController Controller;
    public Camera PlayerCam;
    public List<GameObject> NPCs;
    List<Transform> NPCMeshs;
    public Transform Scenery;

    public float drunkness;
    float distance;
    float shake;
    // Start is called before the first frame update
    void Start()
    {
        Controller = Player.GetComponent<PlayerController>();
        NPCMeshs = new List<Transform>();
        foreach (GameObject NPC in NPCs)
        {
            NPCMeshs.Add(NPC.transform.Find("Skin"));
        }



    }

    float calcAngle(Vector3 targetPos)
    {
        Vector3 camPos = PlayerCam.transform.position;
        camPos.z += 1;
        Vector3 ObjPos = targetPos;
        float XZ_Angle = Mathf.Acos(((camPos.x * ObjPos.x) + (camPos.z * ObjPos.z)) / (Mathf.Sqrt((Mathf.Pow(camPos.x, 2) + Mathf.Pow(camPos.z, 2)) * (Mathf.Pow(ObjPos.x, 2) + Mathf.Pow(ObjPos.z, 2)))));
        return (XZ_Angle * (180/ Mathf.PI));
    }

    // Update is called once per frame
    void Update()
    {
        //print(calcAngle());

        //drunkness = Controller.Drunkness;

        foreach (Transform Skin in NPCMeshs)
        {
            var Materials = Skin.GetComponent<SkinnedMeshRenderer>().materials;
            int i = 0;
            foreach (Material item in Materials)
            {
                if (item.shader.name == "Custom/DrunkVisionShader")
                {
                    if (Materials.Length == 3)
                    {
                        int DisMod = 1;
                        if (i == 0)
                        {
                            DisMod = -1;
                        }

                        item.SetFloat("_Ang", calcAngle(Skin.GetComponentInParent<Transform>().position));

                        if (drunkness < 10)
                        {
                            item.SetFloat("_Distance", 0);
                            item.SetFloat("_ShakeAmount", 0);
                            //print("No Drunkness");
                        }
                        else if (drunkness < 20)
                        {
                            item.SetFloat("_Distance", 0.15f * DisMod);
                            item.SetFloat("_ShakeAmount", 0);
                            //print("Very Low Drunkness");
                        }
                        else if (drunkness < 30)
                        {
                            item.SetFloat("_Distance", 0.15f * DisMod);
                            item.SetFloat("_ShakeAmount", 0.2f);
                            //print("Low Drunkness");
                        }
                        else if (drunkness < 40)
                        {
                            item.SetFloat("_Distance", 0.3f * DisMod);
                            item.SetFloat("_ShakeAmount", 0.2f);
                            //print("Medium Drunkness");
                        }
                        else if (drunkness < 50)
                        {
                            item.SetFloat("_Distance", 0.3f * DisMod);
                            item.SetFloat("_ShakeAmount", 0.3f);
                            //print("High Drunkness");
                        }
                        else if (drunkness < 60)
                        {
                            item.SetFloat("_Distance", 0.4f * DisMod);
                            item.SetFloat("_ShakeAmount", 0.3f);
                            //print("High Drunkness");
                        }
                        else if (drunkness < 70)
                        {
                            item.SetFloat("_Distance", 0.4f * DisMod);
                            item.SetFloat("_ShakeAmount", 0.5f);
                            //print("High Drunkness");
                        }
                        else if (drunkness < 80)
                        {
                            item.SetFloat("_Distance", 0.5f * DisMod);
                            item.SetFloat("_ShakeAmount", 0.5f);
                            //print("High Drunkness");
                        }
                        else if (drunkness < 90)
                        {
                            item.SetFloat("_Distance", 0.5f * DisMod);
                            item.SetFloat("_ShakeAmount", 0.8f);
                            //print("High Drunkness");
                        } 
                        else
                        {
                            item.SetFloat("_Distance", 0.6f * DisMod);
                            item.SetFloat("_ShakeAmount", 1);
                            //print("Very High Drunkness");
                        }
                    }

                }
                i++;
            }
        }

        foreach (Transform child in Scenery)
        {
            var Materials = child.GetComponent<MeshRenderer>().materials;
            int i = 0;
            foreach (Material item in Materials)
            {
                if (item.shader.name == "Custom/DrunkVisionShader")
                {
                    if (Materials.Length == 3)
                    {
                        int DisMod = 1;
                        if (i == 0)
                        {
                            DisMod = -1;
                        }

                        if (drunkness < 10)
                        {
                            item.SetFloat("_Distance", 0);
                            item.SetFloat("_ShakeAmount", 0);
                            //print("No Drunkness");
                        }
                        else if (drunkness < 20)
                        {
                            item.SetFloat("_Distance", 0.001f * DisMod);
                            item.SetFloat("_ShakeAmount", 500);
                            //print("Very Low Drunkness");
                        }
                        else if (drunkness < 30)
                        {
                            item.SetFloat("_Distance", 0.0015f * DisMod);
                            item.SetFloat("_ShakeAmount", 350);
                            //print("Low Drunkness");
                        }
                        else if (drunkness < 40)
                        {
                            item.SetFloat("_Distance", 0.002f * DisMod);
                            item.SetFloat("_ShakeAmount", 200);
                            //print("Medium Drunkness");
                        }
                        else if (drunkness < 50)
                        {
                            item.SetFloat("_Distance", 0.002f * DisMod);
                            item.SetFloat("_ShakeAmount", 200);
                        }

                        {
                            //print("Does not exsist");
                        }
                    }

                }
                i++;
            }
        }
    }
}
