using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    private Slider sliderBed;
    private Slider sliderLength;
    private Slider sliderWidth;

    private Text textBed;
    private Text textLength;
    private Text textWidth;

    private GameObject plane;

    private UnitGenerator unitGenerator;
    // Start is called before the first frame update
    void Start()
    {
        this.unitGenerator = GameObject.Find("UnitGenerator").GetComponent<UnitGenerator>();

        this.sliderBed = GameObject.Find("SliderBed").GetComponent<Slider>();
        this.sliderLength = GameObject.Find("SliderLength").GetComponent<Slider>();
        this.sliderWidth = GameObject.Find("SliderWidth").GetComponent<Slider>();

        this.textBed = GameObject.Find("TextBed").GetComponent<Text>();
        this.textLength = GameObject.Find("TextLength").GetComponent<Text>();
        this.textWidth = GameObject.Find("TextWidth").GetComponent<Text>();

        this.plane = GameObject.Find("Plane");

        this.sliderBed.onValueChanged.AddListener((float value) => OnSliderBedValueChanged(value, this.sliderBed));
        this.sliderLength.onValueChanged.AddListener((float value) => OnSliderLengthValueChanged(value, this.sliderLength));
        this.sliderWidth.onValueChanged.AddListener((float value) => OnSliderWidthValueChanged(value, this.sliderWidth));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSliderBedValueChanged(float value, Slider EventSender)
    {
        int v = Convert.ToInt32(value);
        this.textBed.text = "Beds: " + v;

        // Generator Prefabs
        unitGenerator.Generate(v);
    }
    void OnSliderLengthValueChanged(float value, Slider EventSender)
    {
        this.textLength.text = "Length: " + value;
        var size = this.plane.GetComponent<MeshFilter>().mesh.bounds.size;
        print(size);
        size.x = value;
    }
    void OnSliderWidthValueChanged(float value, Slider EventSender)
    {
        this.textWidth.text = "Width: " + value;

        this.plane.transform.localScale = new Vector3(this.plane.transform.localScale.x, 1f, value);
    }
}
