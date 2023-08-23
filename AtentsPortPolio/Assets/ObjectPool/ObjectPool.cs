using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��𼭳� ���� ����ϰ� �ͱ� ������ �̱��� �������� �����Ѵ�.
public class ObjectPool : MonoBehaviour
{
    static ObjectPool _inst = null;
    public static ObjectPool inst
    {
        get
        {
            if (_inst == null)
            {
                // �̱��� ������ �޸𸮿� �� �ϳ��� �ν��Ͻ��� ��� �Ǵ� �����̴�.
                _inst = FindObjectOfType<ObjectPool>();
                if (_inst == null)
                {
                    // �޸� �󿡵� ������ ����
                    GameObject obj = new GameObject();
                    obj.name = "ObjectPool";
                    _inst = obj.AddComponent<ObjectPool>();
                }
            }
            return _inst;
        }
    }

    // ������Ʈ���� ������ �ڷᱸ��
    // ����Ʈ, �迭 ���� ������ ���ſ�Ƿ�
    // �̿��̸� ť, �������� ó���ϴ� ���� ������
    // �� ���� ���Լ����� ť�� �����ϴ�.

    Dictionary<string, Queue<GameObject>> myPool = new Dictionary<string, Queue<GameObject>>();
    Dictionary<string, Transform> myExplore = new Dictionary<string, Transform>();

    // �̸����� �� ��� ������ ���� �� �־� ���׸� Ÿ������ ����
    // p�� ����Ʈ�� null�̰�, �����ϸ� �ش� ���� �ȴ�.
    public GameObject GetObject<T>(GameObject org, Transform p = null)
    {
        // T��� �Ҹ��� Ŭ������ �̸��� �˻��Ѵ�
        string Name = typeof(T).Name;

        if (myPool.ContainsKey(Name))
        {
            if (myPool[Name].Count > 0)
            {
                GameObject obj = myPool[Name].Dequeue();
                obj.SetActive(true);
                obj.transform.SetParent(p);
                return obj;
            }
        }
        return Instantiate(org, p);
    }

    public void ReleaseObject<T>(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.SetActive(false);

        string Name = typeof(T).Name;
        if (!myPool.ContainsKey(Name))
        {
            myPool[Name] = new Queue<GameObject>();
            GameObject dir = new GameObject(Name + "Pool");
            dir.transform.SetParent(transform);
            myExplore[Name + "Pool"] = dir.transform;
        }
        obj.transform.SetParent(myExplore[Name + "Pool"]);
        myPool[Name].Enqueue(obj);
    }
}
