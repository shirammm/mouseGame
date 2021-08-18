using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    public ThinkGear thinkgear;

    private float medLevel;
    private const float medBarrier = 40f;
    private float attLevel;
    private const float attBarrier = 40f;

    public GameObject cheese1, cheese2, cheese3, cheese4, cheese5, cheese6, cheese7, cheese8, cheese9, cheese10, cheese11, cheese12;
    GameObject[] cheeses;
    public GameObject target = null;
    System.Random rd = new System.Random();
    int rand;
    bool isCollision;

    [SerializeField]
    float speed = 20;

    Vector3 dirNormalized;

    // Start is called before the first frame update
    void Start()
    {
        thinkgear = GameObject.Find("ThinkGear").GetComponent<ThinkGear>();
        thinkgear.UpdateConnectedStateEvent += () => { thinkgear.StartMonitoring(); Debug.Log("Sensor on"); };
        thinkgear.UpdateMeditationEvent += UpdateMeditation;
        thinkgear.UpdateAttentionEvent += UpdateAttention;

        cheeses = new GameObject[12] { cheese1, cheese2, cheese3, cheese4, cheese5, cheese6, cheese7, cheese8, cheese9, cheese10, cheese11, cheese12 };
        for (int i = 0; i < 12; i++)
            cheeses[i].SetActive(false);
        rand = rd.Next(12);
        target = cheeses[rand];
        target.SetActive(true);
        dirNormalized = (target.transform.position - transform.position + new Vector3(0, -1, 0)).normalized;
        isCollision = false;
    }

    private void UpdateMeditation(int value)
    {
        medLevel = value;
    }

    private void UpdateAttention(int value)
    {
        attLevel = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isCollision = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (medLevel >= medBarrier && attLevel >= attBarrier)
            speed = 20;
        else
            speed = 0;

        if (isCollision)
        {
            int newRand = rd.Next(12);
            while (newRand == rand)
            {
                newRand = rd.Next(12);
            }
            target.SetActive(false);
            target = cheeses[newRand];
            target.SetActive(true);
            dirNormalized = (target.transform.position - transform.position + new Vector3(0, -5, 0)).normalized;
            isCollision = false;
        }
        else {
            transform.position = transform.position + dirNormalized * speed * Time.deltaTime;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(this.transform.position), Time.deltaTime * 40f);
        }
    }
}





