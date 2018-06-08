using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorInitialization : MonoBehaviour
{
    public class EditorData
    {
        public static Material ChoseMaterial;
        public static List<GameObject> MapObject;
    }

    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnInitialisze()
    {
        EditorData.MapObject = new List<GameObject>();
    }

    private void Awake()
    {
        StartCoroutine(initialization());
    }

    IEnumerator initialization()
    {
        yield return EditorData.ChoseMaterial = Resources.Load<Material>("ChoseMode");
    }

    private void Start()
    {
        
    }
}
