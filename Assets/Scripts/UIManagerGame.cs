using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerGame : MonoBehaviour
{
    public static UIManagerGame instance = null;
    public Slider shieldSlider;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        shieldSlider.value = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateShieldSlider(float value)
    {
        Image image = shieldSlider.GetComponentsInChildren<Image>()[1];
        shieldSlider.value = value;
        if(shieldSlider.value > .5f)
        {
            image.color = new Color(2-2*value, 1, 0);
        }
        else if(shieldSlider.value <= .5f)
        {
            image.color = new Color(1, (2*value), 0);
        }
    }
}
