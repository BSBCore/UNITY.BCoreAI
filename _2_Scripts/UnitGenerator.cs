using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGenerator : MonoBehaviour
{
    public GameObject Unit;

    private List<GameObject> units = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Generate(int beds)
    {
        ClearUnits();

        for (int i = 0; i < Mathf.CeilToInt(beds / 2); i++)
        {
            Unit.transform.position = new Vector3(13.18f - (4.1f * (i % 8)), 12f + Mathf.FloorToInt(i / 8) * 2.77f, 3.17f);
            this.units.Add(Instantiate(Unit));
        }
    }

    private void ClearUnits()
    {
        foreach (GameObject g in units)
        {
            GameObject.Destroy(g);
        }
        this.units.Clear();
    }
}
