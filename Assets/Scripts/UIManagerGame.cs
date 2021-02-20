using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerGame : MonoBehaviour
{
    public static UIManagerGame instance = null;
    public Slider shieldSlider;
    public Slider healthSlider;
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
        shieldSlider.value = value;
    }

    public void UpdateHealthSlider(float value)
    {
        print(value);
        Image image = healthSlider.GetComponentsInChildren<Image>()[1];
        healthSlider.value = value;
        if (healthSlider.value > .5f)
        {
            image.color = new Color(2 - 2 * value, 1, 0);
        }
        else if (healthSlider.value <= .5f)
        {
            image.color = new Color(1, (2 * value), 0);
        }
    }
}
