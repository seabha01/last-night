using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    Transform LightSet;
    float timer = 0;
    float timer2 = 0;
    bool increase = true;
    public bool pulse = false;
    public bool change = false;
    public bool spin = false;


    // Start is called before the first frame update
    void Start()
    {
        LightSet = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        if (spin)
        {
            foreach (Transform child in LightSet)
            {
                child.transform.Rotate(0, 30 * Time.deltaTime, 0);
            }
        }

        if (change)
        {
            if (timer > 5)
            {
                timer = 0;
                foreach (Transform child in LightSet)
                {
                    if (child.GetComponent<Light>().color == Color.red)
                    {
                        child.GetComponent<Light>().color = Color.blue;
                    }
                    else if (child.GetComponent<Light>().color == Color.blue)
                    {
                        child.GetComponent<Light>().color = Color.green;
                    }
                    else if (child.GetComponent<Light>().color == Color.green)
                    {
                        child.GetComponent<Light>().color = Color.yellow;
                    }
                    else if (child.GetComponent<Light>().color == Color.yellow)
                    {
                        child.GetComponent<Light>().color = Color.red;
                    }
                    else
                    {
                        child.GetComponent<Light>().color = Color.red;
                    }
                }
            }
        }

        if (pulse)
        {
            foreach (Transform child in LightSet)
            {
                if (child.GetComponent<Light>().intensity == 30)
                {
                    increase = false;
                }
                if (child.GetComponent<Light>().intensity == 5)
                {
                    increase = true;
                }
                if (increase)
                {
                    child.GetComponent<Light>().intensity += 1;
                }
                else
                {
                    child.GetComponent<Light>().intensity -= 1;
                }

            }
        }

    }
}
