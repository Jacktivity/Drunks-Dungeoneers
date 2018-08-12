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
        GetComponent<PatronManager>().MakePatron();
    }
}
