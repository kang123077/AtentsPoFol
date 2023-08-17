using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CommandPattern이란 명령을 인스턴스화 시키는 것.
// 즉 명령을 Class화 한다고 생각할 수 있다.
// ex )

public interface ICommand
{
    void Do(Transform tr);
    void Undo(Transform tr);
}
public class MoveLeft : ICommand
{
    public void Do(Transform tr)
    {
        tr.Translate(Vector3.left);
    }
    public void Undo(Transform tr)
    {
        tr.Translate(Vector3.right);
    }
}
public class MoveRight : ICommand
{
    public void Do(Transform tr)
    {
        tr.Translate(Vector3.right);
    }
    public void Undo(Transform tr)
    {
        tr.Translate(Vector3.left);
    }
}
public class MoveForward : ICommand
{
    public void Do(Transform tr)
    {
        tr.Translate(Vector3.forward);
    }
    public void Undo(Transform tr)
    {
        tr.Translate(Vector3.back);
    }
}
public class MoveBack : ICommand
{
    public void Do(Transform tr)
    {
        tr.Translate(Vector3.back);
    }
    public void Undo(Transform tr)
    {
        tr.Translate(Vector3.forward);
    }
}

// 다음과 같이 명령들을 클래스로 만들어서
// 인스턴스화 시킨 후 객체로 사용하면 된다.

public class StudyCommandPattern : MonoBehaviour
{
    Stack<ICommand> commands = new Stack<ICommand>();

    ICommand AKey = null;
    ICommand SKey = null;
    ICommand DKey = null;
    ICommand WKey = null;

    void Start()
    {
        AKey = new MoveLeft();
        SKey = new MoveBack();
        DKey = new MoveRight();
        WKey = new MoveForward();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // AKey에 어떤 명령이 담긴지는 모르지만 사용
            AKey.Do(transform);
            commands.Push(AKey);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // SKey에 어떤 명령이 담긴지는 모르지만 사용
            SKey.Do(transform);
            commands.Push(SKey);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // DKey에 어떤 명령이 담긴지는 모르지만 사용
            DKey.Do(transform);
            commands.Push(DKey);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            // WKey에 어떤 명령이 담긴지는 모르지만 사용
            WKey.Do(transform);
            commands.Push(WKey);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ICommand temp;
            temp = WKey;
            WKey = SKey;
            SKey = temp;
        }

        // 컨트롤 Z를 누르면 이전 명령을 되돌리는 것을 Stack 구조를 이용해서 구현
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (commands.Count > 0) commands.Pop().Undo(transform);
        }
    }
}
