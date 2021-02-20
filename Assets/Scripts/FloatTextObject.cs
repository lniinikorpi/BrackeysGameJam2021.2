using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatTextObject : MonoBehaviour
{
    public TMP_Text numberText;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int value, Color color)
    {
        if (value > 0)
        {
            numberText.text = "+" + value.ToString();
        }
        else
        {
            numberText.text = value.ToString();
        }
        numberText.color = color;
    }
}
