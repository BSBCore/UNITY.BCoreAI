/**
 * 
 *  Modules Generator
 *  
 *  How MODULES place on PLANE.
 *
 *  Abstract Level: 
 *  Primitives - Entites - [Modules] - Buildings
 *
 *  By t,t
 *
 *  --04-21-2020 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModulesGenerator : MonoBehaviour
{
    public GameObject Module;
    private List<GameObject> modules = new List<GameObject>();

    // these values should be standardized
    // import the modules by id from [DATABASE].
    // also compute them from transform data and meta data(prsm)
    public float ModuleWidth = 4.2f;
    public float ModuleLength = 12.1f;
    public float ModuleHeight = 2.77f;
    public float BedsPerModule = 2;
    public float BuildingInterval = 5;

    private GameObject plane;       // the plane where build the modules on.
    private float planeWidth;
    private float planeLength;
    private float planeRedLineTop;
    private float planeRedLineLeft;

    void Start()
    {

    }

    public void Generate(int requiredBeds)
    {
        // Initialize Values
        this.plane = GameObject.Find("Plane");
        this.planeWidth = float.Parse(GameObject.Find("InputPlaneWidth").GetComponent<InputField>().text);
        this.planeLength = float.Parse(GameObject.Find("InputPlaneLength").GetComponent<InputField>().text);
        this.planeRedLineTop = float.Parse(GameObject.Find("InputPlaneRedLineLeft").GetComponent<InputField>().text);
        this.planeRedLineLeft = float.Parse(GameObject.Find("InputPlaneRedLineTop").GetComponent<InputField>().text);

        // Clear all modules before making a new building.
        ClearModules();

        // Update stats bar
        GameObject.Find("StatUnits").GetComponent<Text>().text = (requiredBeds / 2).ToString();

        // Plane的宽度可容纳多少个Unit
        var unitsPerWidth = Mathf.FloorToInt((planeWidth - planeRedLineLeft * 2) / ModuleWidth);
        // Plane的长度可容纳多少个Unit
        var unitsPerLength = Mathf.FloorToInt((planeLength - planeRedLineTop * 2) / (ModuleLength + BuildingInterval));

        var curUnit = 0;
        while (curUnit <= Mathf.CeilToInt(requiredBeds / BedsPerModule))
        {


            //添加一个新的Unit, 放入Plane
            this.modules.Add(Instantiate(Module));
            var lastUnit = this.modules[this.modules.Count - 1];
            lastUnit.transform.parent = this.plane.transform;

            // plane.tranform.position 为plane的中点
            // + (planeWidth/2) 为plane的最右边
            // - planeRedLineLeft 为红线
            // - 2.05f 为Unit的一半宽度，之后请改成【变量】
            // 4.1f 为Unit的宽度，之后请改成【变量】
            lastUnit.transform.position = new Vector3(
                plane.transform.position.x + (planeWidth / 2) - planeRedLineLeft - ModuleWidth / 2 - (ModuleWidth * (curUnit % unitsPerWidth)),
                Mathf.FloorToInt(curUnit / (unitsPerWidth * unitsPerLength)) * ModuleHeight,
                plane.transform.position.z + (planeLength / 2) - planeRedLineTop - ModuleLength / 2 - Mathf.FloorToInt(curUnit / unitsPerWidth) % unitsPerLength * (ModuleLength + BuildingInterval));

            //每个模块增加2张床
            curUnit++;
        }
    }

    public void ClearModules()
    {
        foreach (GameObject g in modules)
        {
            GameObject.Destroy(g);
        }
        this.modules.Clear();
    }
}
