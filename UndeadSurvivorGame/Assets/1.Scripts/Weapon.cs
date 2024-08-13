using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    PlayerMove player;

    private void Awake()
    {
        player = Gamemanager.instance.player;
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime); 
                break;
            default:
                timer += Time.deltaTime;

                if(timer > speed)
                {
                    timer = 0;
                    Fire();
                }
                break;

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

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        //Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for(int index = 0; index < Gamemanager.instance.pool.prefabs.Length; index++)
        {
            if(data.projectile == Gamemanager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150;
                Setting();
                break;
            default:
                speed = 0.4f;
                break;
        }

        player.BroadcastMessage("ApplyGear" , SendMessageOptions.DontRequireReceiver);  //특정 함수 호출을 모든 자식에게 방송하는 함수 
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

            bullet.GetComponent<Bullet>().Init(damage, -1 , Vector3.zero);    //-1 is Infinity Per.

        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = Gamemanager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up , dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

    }
}
