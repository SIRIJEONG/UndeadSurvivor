using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    //prefab보관 
    public GameObject[] prefabs;

    //pool담당 List
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject Select = null;

        //선택한 풀의 비활성화된 오브젝트 접근 
        foreach (GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                //발견하면 Select 변수에 할당 
                Select = item;
                Select.SetActive(true);
                break;
            }
        }

        //못 찾았다면
        if(!Select)
        {
            //새롭게 생성하고 select 변수에 할당 
            Select = Instantiate(prefabs[index],transform);
            pools[index].Add(Select);

        }

        return Select;
    }
}
