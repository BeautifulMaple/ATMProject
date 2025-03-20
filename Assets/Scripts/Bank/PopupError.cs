using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupError : MonoBehaviour
{
    public GameObject error;
    public Button errerBtn;

    // Start is called before the first frame update
    void Start()
    {
        errerBtn.onClick.AddListener(OnErrorButton);
    }

    // Update is called once per frame
    void OnErrorButton()
    {
        error.gameObject.SetActive(false);
    }
}
