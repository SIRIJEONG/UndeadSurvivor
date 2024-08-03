using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    //prefab���� 
    public GameObject[] prefabs;

    //pool��� List
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

        //������ Ǯ�� ��Ȱ��ȭ�� ������Ʈ ���� 
        foreach (GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                //�߰��ϸ� Select ������ �Ҵ� 
                Select = item;
                Select.SetActive(true);
                break;
            }
        }

        //�� ã�Ҵٸ�
        if(!Select)
        {
            //���Ӱ� �����ϰ� select ������ �Ҵ� 
            Select = Instantiate(prefabs[index],transform);
            pools[index].Add(Select);

        }

        return Select;
    }
}
