using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    public Button button;


    // Use this for initialization
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick() {
        Vector2[] path = { new Vector2(0, 0) };
        GetComponent<PatronManager>().MakePatron(path);
    }
}
