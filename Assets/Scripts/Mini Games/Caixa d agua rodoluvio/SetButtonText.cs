using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class SetButtonText : MonoBehaviour
{
    public Button m_Button;
    private void Awake()
    {
        m_Button = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // SerializedObject so = new SerializedObject(m_Button);
        // SerializedProperty persistentCalls = so.FindProperty("m_OnClick.m_PersistentCalls.m_Calls");
        // for (int i = 0; i < persistentCalls.arraySize; ++i)
        //     Debug.Log(persistentCalls.GetArrayElementAtIndex(i).FindPropertyRelative("m_Arguments.m_IntArgument").intValue);

        // m_Button.GetComponentInChildren<Text>().text = "Comprar: " + persistentCalls.GetArrayElementAtIndex(0).FindPropertyRelative("m_Arguments.m_IntArgument").intValue + " ml\n" + "Preço: " + persistentCalls.GetArrayElementAtIndex(1).FindPropertyRelative("m_Arguments.m_IntArgument").intValue;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
