using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime); 
                break;
        }

        //Test Code
        if (Input.GetButtonDown("Jump"))
        {
            Levelup(20, 5);
        }
    }

    public void Levelup(float damage , int count)
    {
        this.damage = damage;
        this.count += count;

        if(id == 0)
        {
            Setting();
        }

    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Setting();
                break;
        }
    }

    void Setting()
    {
        for ( int index = 0; index < count; index++ )
        {
            Transform bullet;
            if(index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = Gamemanager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }
             

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity; 

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1);    //-1 is Infinity Per.

        }
    }
}
