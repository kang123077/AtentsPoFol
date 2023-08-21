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
        // RectTransform은 Transform을 상속받고 있으므로 as를 통해서 강제 형번환이 가능하다.
        // null값 검사가 가능하기 때문에 부모 > 자식일때는 as로 해주는 것이 좋다.
        RectTransform rt = SceneData.inst.miniMap as RectTransform;
        if (rt != null)
        {
            // x, y가 같기 때문에 지금은 뭘로 해도 상관없다.
            size = rt.sizeDelta.x;
        }
    }

    void Update()
    {
        // 1,1,1 정육면체 rdc 압축 > 1, 1 사각 평면 뷰포트 > 모니터 크기에 맞게 늘린것
        // allCameras는 모든 카메라를 배열에 담고 있고, depth가 -1인 메인카메라가 0번, 미니맵카메라가 1번이다.
        // 이후 미니맵이 150 사이즈이므로 150을 곱해준다.
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
