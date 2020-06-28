using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEndGameOverlay : MonoBehaviour, IUIEndGameOverlay
{
    public MonoBehaviour MonoBehaviour => monoBehaviour;

    private MonoBehaviour monoBehaviour;
    private ContinueClick continueClick;

    void Awake()
    {
      monoBehaviour = this;
      continueClick = GetComponentInChildren<ContinueClick>();
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
