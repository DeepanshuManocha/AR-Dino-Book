using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustSlider : MonoBehaviour
{
    public GameObject objectToRotate;
    public Slider slider;

    private float previousValue;

    void Awake()
    {
        this.slider.onValueChanged.AddListener(this.OnSliderChanged);

        this.previousValue = this.slider.value;
    }

    public void OnSliderChanged(float value)
    {
        float delta = value - this.previousValue;
        this.objectToRotate.transform.Rotate(Vector3.down * delta * 360);
        //this.objectToRotate.transform.Rotate(new Vector3(0 , -delta*360, 0));
        this.previousValue = value;
    }
}
