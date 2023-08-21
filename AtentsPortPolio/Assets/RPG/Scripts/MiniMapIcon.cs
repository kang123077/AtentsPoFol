using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapIcon : MonoBehaviour
{
    public Image myImage;
    Transform myTarget = null;
    float size = 0.0f;
    void Start()
    {
        // RectTransform�� Transform�� ��ӹް� �����Ƿ� as�� ���ؼ� ���� ����ȯ�� �����ϴ�.
        // null�� �˻簡 �����ϱ� ������ �θ� > �ڽ��϶��� as�� ���ִ� ���� ����.
        RectTransform rt = SceneData.inst.miniMap as RectTransform;
        if (rt != null)
        {
            // x, y�� ���� ������ ������ ���� �ص� �������.
            size = rt.sizeDelta.x;
        }
    }

    void Update()
    {
        // 1,1,1 ������ü rdc ���� > 1, 1 �簢 ��� ����Ʈ > ����� ũ�⿡ �°� �ø���
        // allCameras�� ��� ī�޶� �迭�� ��� �ְ�, depth�� -1�� ����ī�޶� 0��, �̴ϸ�ī�޶� 1���̴�.
        // ���� �̴ϸ��� 150 �������̹Ƿ� 150�� �����ش�.
        if (myTarget != null)
        {
            Vector2 pos = Camera.allCameras[1].WorldToViewportPoint(myTarget.position) * size;
            (transform as RectTransform).anchoredPosition = pos;
        }
    }

    public void Initialize(Transform target, Color color)
    {
        myTarget = target;
        myImage.color = color;
    }
}
