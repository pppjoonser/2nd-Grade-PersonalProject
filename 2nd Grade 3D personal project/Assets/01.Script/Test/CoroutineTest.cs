using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

delegate void MyFunc(int a);

public class Student
{
    public int myNumber;

    public Student(int number)
    {
        myNumber = number;
    }

    public void DoStuff(int a)
    {
        Debug.Log(a + myNumber);
    }
}

//public class MyClass : IEnumerable
//{
//    private int[] _numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
//    private int _index;
//    public IEnumerator GetEnumerator()
//    {
//        while (_index < _numbers.Length)
//        {
//            yield return _numbers[_index];
//            _index++;
//        }
//    }
//}
public class CoroutineTest : MonoBehaviour
{
    //private MyClass _myClass;
    //IEnumerator codeSet;
    //[ContextMenu("Test log")]
    //void Start()
    //{
    //    _myClass = new MyClass();

    //     codeSet = _myClass.GetEnumerator();



    //    foreach (var Item in _myClass)//MoveNext\
    //    {
    //        Debug.Log(Item);
    //    }

    //}

    //[ContextMenu("Print")]
    //private void Print()
    //{
    //    Debug.Log(codeSet.Current);
    //    codeSet.MoveNext();
    //}
    //private void Start()
    //{
    //    StartCoroutine(DelayCoroutine());
    //}

    //private IEnumerator DelayCoroutine()
    //{
    //    yield return new WaitForSeconds(1f);//monobehaver에서 큐로 만들어서 가지고 있음
    //    Debug.Log("1");
    //    yield return new WaitForSeconds(1f);
    //    Debug.Log("2");
    //    yield return new WaitForSeconds(1f);
    //    Debug.Log("3");
    //}

    //private void Start()
    //{
    //    if(Thread.CurrentThread.Name == null)
    //    {
    //        Thread.CurrentThread.Name = "MainThread";
    //    }
    //}
    //private void Update()
    //{
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        Debug.Log("점프");
    //    }

    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        JobSequence();
    //    }
    //}
    //private async Task JobSequence()
    //{
    //    await Awaitable.WaitForSecondsAsync(1f, destroyCancellationToken);
    //}
    //private void DoSomeJob()
    //{
    //    Debug.Log(Thread.CurrentThread.Name);
    //    ulong timer = 0;
    //    while (true)
    //    {
    //        timer++;
    //        if (timer > 9000000000L)
    //        {
    //            break;
    //        }
    //    }
    //    Debug.Log("my Job Complete!");
    //}


    //델리게이트 : 변수인데 함수를 담고 싶다.
    //변수는 값을 저장하는것 근데 이제 큰 아가들은 주소로 저장하는...
    //그렇다면 함수의 주소를 알고있다면 << 넣을 수 있겠지.
    public void Test(int a)
    {
        Debug.Log(a);
    }
    private List<MyFunc> myFuncs = new List<MyFunc> ();
    [ContextMenu("TTT")]
    private void TestDelegate()
    {

        for (int i = 0; i < 10; i++)
        {
            int number = i;
            myFuncs[i] = (a) => Debug.Log(a + number);
        }

        for(int j =0; j < myFuncs.Count; j++)
        {
            myFuncs[j]?.Invoke(0);
        }
        
        //Student s = new Student(20);
        //mf = s.DoStuff;
        //mf?.Invoke(10);
    }

    private void Print(int number)
    {
        Debug.Log(number);
    }
}
