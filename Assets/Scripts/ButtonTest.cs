using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    public Button button;
    Patron patron;

    // Use this for initialization
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);

        patron = GameObject.Find("EventSystem").GetComponent<Patron>(); 
    }

    void TaskOnClick() {
        GameObject empty = new GameObject();
        empty.AddComponent<Patron>();
        empty.GetComponent<Patron>();
    }
}
