using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CommandPattern�̶� ����� �ν��Ͻ�ȭ ��Ű�� ��.
// �� ����� Classȭ �Ѵٰ� ������ �� �ִ�.
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

// ������ ���� ��ɵ��� Ŭ������ ����
// �ν��Ͻ�ȭ ��Ų �� ��ü�� ����ϸ� �ȴ�.

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
            // AKey�� � ����� ������� ������ ���
            AKey.Do(transform);
            commands.Push(AKey);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // SKey�� � ����� ������� ������ ���
            SKey.Do(transform);
            commands.Push(SKey);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // DKey�� � ����� ������� ������ ���
            DKey.Do(transform);
            commands.Push(DKey);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            // WKey�� � ����� ������� ������ ���
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

        // ��Ʈ�� Z�� ������ ���� ����� �ǵ����� ���� Stack ������ �̿��ؼ� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (commands.Count > 0) commands.Pop().Undo(transform);
        }
    }
}
