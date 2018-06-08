using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class ObjectOperate : MonoBehaviour
{
    public enum objectState { over, used }

    Color unableColor { get { return new Color(1, 0, 0, 0.5f); } } //不可摆放时的颜色。
    Color ableColor { get { return new Color(0, 1, 0, 0.5f); } } //可以摆放时的颜色。
    Color overColor { get { return new Color(1, 1, 1, 0.5f); } } //鼠标悬浮时的颜色。

    Material defaultMaterial;
    Material choseMaterial;

    MeshRenderer meshRenderer;
    MeshCollider meshCollider;
    BoxCollider boxCollider;

    private objectState state;
    public objectState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
        }
    }

    private void Awake()
    {
        State = objectState.over;
    }

    private void OnEnable()
    {
        State = objectState.over;
        EditorInitialization.EditorData.MapObject.Add(gameObject);
    }

    private void OnDisable()
    {
        EditorInitialization.EditorData.MapObject.Remove(gameObject);
    }

    void Start ()
    {
        defaultMaterial = GetComponent<MeshRenderer>().material;
        choseMaterial = EditorInitialization.EditorData.ChoseMaterial;
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        if (null == GetComponent<BoxCollider>()) boxCollider = gameObject.AddComponent<BoxCollider>();
        else boxCollider = GetComponent<BoxCollider>();
        boxCollider.center = new Vector3(0, transform.position.z - 2, 0.5f);
        boxCollider.size = new Vector3(meshCollider.bounds.size.x, 5, 1);
	}


    RaycastHit raycastHit;

	void Update ()
    {
        //限制坐标只能为整数。
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);
        int z = Mathf.RoundToInt(transform.position.z);
        transform.position = new Vector3(x, y, z);

        boxCollider.center = new Vector3(0, transform.position.z - 2, 0.5f);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 100, 1 << 2))
        {
            int x1 = Mathf.RoundToInt(raycastHit.point.x);
            int y1 = (int)raycastHit.point.y;

            if (State == objectState.used)
            {
                transform.position = new Vector3(x1, y1, 2);
            }
        }
    }

    RaycastHit needCloseBoxCollider;

    private void OnMouseOver()
    {
        meshRenderer.material = choseMaterial;
        choseMaterial.mainTexture = defaultMaterial.mainTexture;
        choseMaterial.SetTexture("_MainTex", defaultMaterial.mainTexture);

        if (State == objectState.over)
        {
            boxCollider.size = new Vector3(meshCollider.bounds.size.x, 5, 1);
            choseMaterial.color = overColor;
            choseMaterial.SetFloat("_AlphaScale", 0.5f);
        }
        else
        {
            boxCollider.size = new Vector3(10, 10, 2);
            if (Physics.CheckBox(transform.position + boxCollider.center, boxCollider.size / 2, transform.rotation, ~1 << 2))
            {
                choseMaterial.color = unableColor;
                //choseMaterial.SetColor("_EmissionColor", unableColor);
            }
            else
            {
                choseMaterial.color = ableColor;
                //choseMaterial.SetColor("_EmissionColor", ableColor);
            }
        }
    }

    private void OnMouseExit()
    {
        meshRenderer.material = defaultMaterial;
    }

    private void OnMouseDown()
    {
        State = State == objectState.over ? objectState.used : objectState.over;
    }
}
