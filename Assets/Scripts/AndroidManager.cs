using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class AndroidManager : MonoBehaviour
{
	private AndroidJavaObject m_JavaObject;

    void Start()
    {
		// 현재 Activity 정보를 얻어온다.
		var jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		m_JavaObject = jc.GetStatic<AndroidJavaObject>("currentActivity");
    }

	void OnGUI()
	{
		if (GUI.Button(new Rect(0.0f, 0.0f, 200.0f, 200.0f), "Test Method 1"))
		{
			// 현재 Activity에 존재하는 TestMethod를 호출한다.
			m_JavaObject.Call("TestMethod");
		}
		
		if (GUI.Button(new Rect(0.0f, 300.0f, 200.0f, 200.0f), "Test Method 2"))
		{
			// 현재 Activity에 존재하는 TestMethod를 호출하고 int 타입의 값을 반환 받는다.
			var result = m_JavaObject.Call<int>("TestMethod", 777);
			Debug.Log("Result = " + result);
		}
		
		if (GUI.Button(new Rect(0.0f, 600.0f, 200.0f, 200.0f), "Test Method 3"))
		{
			// 현재 Activity에 존재하는 TestMethod에 파라미터를 함께 전달한다.
			m_JavaObject.Call("TestMethod", "Hello Native Plugin");
		}
	}

	private void PrintMessage(string msg)
	{
		Debug.Log("AndroidManager::PrintMessage - " + msg);
	}
}