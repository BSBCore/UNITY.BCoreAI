/**
 * Global UI Events
 * 
 * By tt
 * 
 * 20200422
 */
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour
{
    private float planeWidth = 30f;
    private float planeLength = 30f;

    private bool isSunlightAnalyse = false;

    public GameObject Plane;
    public GameObject Sky;
    public GameObject Sun;
    public GameObject SunlightAnalyseTime;
    public GameObject ModulesGenerator;
    public GameObject MainCamera;
    public GameObject FPSCamera;

    public enum ViewMode
    {
        AERIAL,
        PLANE,
        ANIMATION,
        FPS
    };
    private ViewMode viewMode;


    void Start()
    {
        //默认为鸟瞰
        SetViewMode(ViewMode.AERIAL);
        //关闭日照
        SetSunlightAnalyse(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSunlightAnalyse)
        {
            //Update Time
            var hour = Mathf.FloorToInt(this.Sky.GetComponent<AzureSky_Controller>().TIME_of_DAY);
            var mins = Convert.ToInt32((this.Sky.GetComponent<AzureSky_Controller>().TIME_of_DAY - hour) * 60);
            GameObject.Find("TimeOfDay").GetComponent<Text>().text = hour.ToString() + ":" + mins.ToString();
        }
    }

    /*======================== Input Fields ========================*/

    /// <summary>
    /// 改成病床数
    /// </summary>
    public void OnBedsChanged()
    {
        var bedsCount = Convert.ToInt32(GameObject.Find("InputBeds").GetComponent<InputField>().text);
        this.ModulesGenerator.GetComponent<ModulesGenerator>().Generate(bedsCount);
        OnBuildingLightChanged();
        SetStats();
    }

    /// <summary>
    /// 改变地面宽度
    /// </summary>
    public void OnPlaneWidthChanged()
    {
        this.planeWidth = float.Parse(GameObject.Find("InputPlaneWidth").GetComponent<InputField>().text);
        this.Plane.transform.localScale = new Vector3(planeWidth / 10, this.Plane.transform.localScale.y, this.Plane.transform.localScale.z);
        this.ModulesGenerator.GetComponent<ModulesGenerator>().ClearModules();
        SetStats();
    }

    /// <summary>
    /// 改变地面长度
    /// </summary>
    public void OnPlaneLengthChanged()
    {
        this.planeLength = float.Parse(GameObject.Find("InputPlaneLength").GetComponent<InputField>().text);
        this.Plane.transform.localScale = new Vector3(this.Plane.transform.localScale.x, this.Plane.transform.localScale.y, planeLength / 10);
        this.ModulesGenerator.GetComponent<ModulesGenerator>().ClearModules();
        SetStats();
    }

    /// <summary>
    /// 改变每日周期时间
    /// </summary>
    public void OnDayCycleMinsChanged()
    {
        this.Sky.GetComponent<AzureSky_Controller>().SetTime(float.Parse(GameObject.Find("InputTimeOfDay").GetComponent<InputField>().text), float.Parse(GameObject.Find("InputDayCycleMins").GetComponent<InputField>().text));
    }

    /// <summary>
    /// 改变时间
    /// </summary>
    public void OnTimeOfDayChanged()
    {
        this.Sky.GetComponent<AzureSky_Controller>().SetTime(float.Parse(GameObject.Find("InputTimeOfDay").GetComponent<InputField>().text), float.Parse(GameObject.Find("InputDayCycleMins").GetComponent<InputField>().text));
    }

    /*======================== View Mode ========================*/
    public void OnButtonViewModePlane()
    {
        SetViewMode(ViewMode.PLANE);
    }

    public void OnButtonViewModeFPS()
    {
        SetViewMode(ViewMode.FPS);
    }

    public void OnButtonViewModeNormal()
    {
        SetViewMode(ViewMode.AERIAL);
    }

    public void OnButtonViewModeAnimation()
    {
        SetViewMode(ViewMode.ANIMATION);
    }

    /*======================== Toggles ========================*/
    /// <summary>
    /// 全栋建筑灯光
    /// </summary>
    public void OnBuildingLightChanged()
    {
        SetBuildingLight(GameObject.Find("BuildingLight").GetComponent<Toggle>().isOn);
    }

    /// <summary>
    /// 日照系统
    /// </summary>
    public void OnSunlightAnalyseChanged()
    {
        SetSunlightAnalyse(GameObject.Find("SunlightAnalyse").GetComponent<Toggle>().isOn);
    }

    /*======================== SETS ========================*/
    public void SetViewMode(ViewMode vm)
    {
        this.viewMode = vm;

        var allUnits = GameObject.Find("Plane").GetComponentsInChildren<MeshRenderer>();

        //阴影处理
        if (this.viewMode == ViewMode.PLANE)
        {
            foreach (MeshRenderer g in allUnits)
            {
                g.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }
        }
        else
        {
            foreach (MeshRenderer g in allUnits)
            {
                g.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }

        //摄像机处理
        if (this.viewMode == ViewMode.FPS)
        {
            MainCamera.SetActive(false);
            FPSCamera.SetActive(true);
        }
        else
        {
            MainCamera.SetActive(true);
            FPSCamera.SetActive(false);
        }

        switch (viewMode)
        {
            case ViewMode.PLANE:
                GameObject.Find("Main Camera").GetComponent<SmoothCameraOrbit>().enabled = false;
                GameObject.Find("Main Camera").GetComponent<Camera>().orthographic = true;
                GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = planeWidth - 10;
                GameObject.Find("Main Camera").transform.position = new Vector3(0, planeWidth + 10, 0);
                GameObject.Find("Main Camera").transform.eulerAngles = new Vector3(90f, 0, 0);
                break;

            case ViewMode.ANIMATION:
                break;

            case ViewMode.FPS:

                break;

            case ViewMode.AERIAL:
                GameObject.Find("Main Camera").transform.position = new Vector3(0, 30f, -40f);
                GameObject.Find("Main Camera").transform.eulerAngles = new Vector3(37f, 0, 0);
                GameObject.Find("Main Camera").GetComponent<SmoothCameraOrbit>().enabled = true;
                GameObject.Find("Main Camera").GetComponent<Camera>().orthographic = false;
                break;
        }
    }


    public void SetStats()
    {
        GameObject.Find("StatArea").GetComponent<Text>().text = this.planeWidth + "m x " + this.planeLength + "m = " + this.planeWidth * this.planeLength + "m²";
    }

    public void SetBuildingLight(bool value)
    {
        foreach (Light l in GameObject.Find("Plane").GetComponentsInChildren<Light>())
        {
            l.enabled = value;
        }
    }

    public void SetSunlightAnalyse(bool value)
    {
        this.isSunlightAnalyse = value;
        this.Sky.SetActive(value);
        this.Sun.SetActive(!value);
        this.SunlightAnalyseTime.SetActive(value);
    }
}
