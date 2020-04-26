/**
 * Plane Events
 * 
 *  地块移动点击等事件
 *  
 * By T.T
 * 
 * --04-20-2020 
 *     - Move
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneEvents : MonoBehaviour
{
    private Material originMaterial;
    // Start is called before the first frame update
    void Start()
    {
        this.originMaterial = this.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update() { }

    IEnumerator OnMouseEnter()
    {
        //鼠标经过时，更换Material
        this.GetComponent<MeshRenderer>().material = GameObject.Find("Public").GetComponent<PublicMaterials>().HighlightMaterial;
        yield return new WaitForFixedUpdate();
    }

    IEnumerator OnMouseExit()
    {
        //鼠标离开时，复原Material
        this.GetComponent<MeshRenderer>().material = this.originMaterial;
        yield return new WaitForFixedUpdate();
    }

    IEnumerator OnMouseDown()
    {
        //移动地块Plane
        Vector3 ScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));

        while (Input.GetMouseButton(0))
        {
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z);
            Vector3 CurPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            transform.position = new Vector3(CurPosition.x, transform.position.y, CurPosition.z);

            yield return new WaitForFixedUpdate();
        }


    }
}