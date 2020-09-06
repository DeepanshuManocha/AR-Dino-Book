using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Retcile : MonoBehaviour
{
    public float value, duration;
    public Image retcileImage;
    public bool active;
    public Color startColor, endColor;

    // Start is called before the first frame update
    void Start()
    {
        value=0;
        duration =1;
        if(retcileImage == null)
            retcileImage = GetComponent<Image>();
        retcileImage.fillAmount = 0;
        active=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            retcileImage.enabled=true;
            value += Time.deltaTime / duration;
            retcileImage.fillAmount = value;
            retcileImage.color = Color.Lerp(startColor, endColor, value);
        }
        if(value>1)
        {
            active=false;   
            retcileImage.enabled=false;
        }
    }

    public void SetActive(bool active)
    {
        retcileImage.enabled = (this.active=active);
    }
}
