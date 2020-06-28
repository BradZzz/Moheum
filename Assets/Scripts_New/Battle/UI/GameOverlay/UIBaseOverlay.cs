using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseOverlay : MonoBehaviour, IUIBaseOverlay
{
    public MonoBehaviour MonoBehaviour => monoBehaviour;

    private MonoBehaviour monoBehaviour;

    void Awake()
    {
      monoBehaviour = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
