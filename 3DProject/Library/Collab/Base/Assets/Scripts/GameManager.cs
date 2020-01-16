using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject objUI;
    public StepParser stepParser;

    //enforces singleton pattern
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        objUI = GameObject.Find("ObjectNum");

    }

    void Update()
    {

        objUI.GetComponent<Text>().text = "Items left to collect: " + ItemCollect.objects.ToString();


        if (ItemCollect.objects == 0)
        {
            if (stepParser.billPayed == true)
            {
                objUI.GetComponent<Text>().text = "All objects collected";

                SceneManager.LoadScene(2); //load final screen, game is over
            }
            else
            {
                objUI.GetComponent<Text>().text = "You still have to pay your bill!";
            }

        }
    }

}
