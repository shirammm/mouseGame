using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MouseyManager : ActivityManager
{
    [SerializeField]
    private float rotationForce = 150;
    private const float mouseySpeed = 20;

    public Animator anim;
    public ThinkGear thinkgear;

    public GameObject cheese1, cheese2, cheese3, cheese4, cheese5, cheese6, cheese7, cheese8, cheese9, cheese10, cheese11, cheese12;
    GameObject[] cheeses;
    public GameObject target = null;
    System.Random rd = new System.Random();
    int rand;
    bool isCollision;

    [SerializeField]
    float speed = 20;

    Vector3 dirNormalized;

    public static float score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        cheeses = new GameObject[12] { cheese1, cheese2, cheese3, cheese4, cheese5, cheese6, cheese7, cheese8, cheese9, cheese10, cheese11, cheese12 };
        for (int i = 0; i < 12; i++)
            cheeses[i].SetActive(false);
        rand = rd.Next(12);
        target = cheeses[rand];
        target.SetActive(true);
        dirNormalized = (target.transform.position - transform.position + new Vector3(0, -1, 0)).normalized;
        isCollision = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isCollision = true;
    }

    protected override void UpdateBehaviour(bool state)
    {
        if (state)
        {
            speed = mouseySpeed;
            score += Time.deltaTime;
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
            }
            dirNormalized = (target.transform.position - transform.position).normalized;
            var rot = Quaternion.LookRotation(dirNormalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, (rotationForce) * Time.deltaTime);
            transform.position += transform.forward * speed * Time.deltaTime; isCollision = false;
        }
        else
        {
            speed = 0;
        }
    }

}





