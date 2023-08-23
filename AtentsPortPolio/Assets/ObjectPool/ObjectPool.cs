using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 어디서나 자주 사용하고 싶기 때문에 싱글톤 패턴으로 구성한다.
public class ObjectPool : MonoBehaviour
{
    static ObjectPool _inst = null;
    public static ObjectPool inst
    {
        get
        {
            if (_inst == null)
            {
                // 싱글톤 패턴은 메모리에 단 하나의 인스턴스만 허용 되는 패턴이다.
                _inst = FindObjectOfType<ObjectPool>();
                if (_inst == null)
                {
                    // 메모리 상에도 없으면 생성
                    GameObject obj = new GameObject();
                    obj.name = "ObjectPool";
                    _inst = obj.AddComponent<ObjectPool>();
                }
            }
            return _inst;
        }
    }

    // 오브젝트들을 저장할 자료구조
    // 리스트, 배열 등은 구조상 무거우므로
    // 이왕이면 큐, 스택으로 처리하는 것이 좋은데
    // 이 경우는 선입선출의 큐가 적당하다.

    Dictionary<string, Queue<GameObject>> myPool = new Dictionary<string, Queue<GameObject>>();
    Dictionary<string, Transform> myExplore = new Dictionary<string, Transform>();

    // 이름으로 할 경우 문제가 생길 수 있어 제네릭 타입으로 변경
    // p의 디폴트는 null이고, 전달하면 해당 값이 된다.
    public GameObject GetObject<T>(GameObject org, Transform p = null)
    {
        // T라는 불명의 클래스의 이름을 검색한다
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
