//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Battle.Model.Jewel;
//using Battle.Model.Player;
//using Battle.UI.Jewel;
//using Battle.UI.Utils;
//using UnityEngine;

//namespace Battle.UI.Board
//{
//  public class UiBoardComponent : UiListener, IUiBoard
//  {
//    void Awake()
//    {
//      MyTransform = transform;
//      MyCollider = GetComponent<Collider>();
//      MyRigidbody = GetComponent<Rigidbody>();
//      MyInput = GetComponent<IMouseInput>();
//      MyRenderers = GetComponentsInChildren<SpriteRenderer>();
//      MyRenderer = GetComponent<SpriteRenderer>();
//      MyMRenderers = GetComponentsInChildren<MeshRenderer>();
//      MyMRenderer = GetComponent<MeshRenderer>();
//    }

//    public List<IUiJewel> Jewels => throw new NotImplementedException();

//    public bool IsInitialized => false;

//    MeshRenderer[] IUiBoardComponent.MRenderers => MyMRenderers;
//    MeshRenderer IUiBoardComponent.MRenderer => MyMRenderer;
//    SpriteRenderer[] IUiBoardComponent.Renderers => MyRenderers;
//    SpriteRenderer IUiBoardComponent.Renderer => MyRenderer;
//    Collider IUiBoardComponent.Collider => MyCollider;
//    Rigidbody IUiBoardComponent.Rigidbody => MyRigidbody;
//    IMouseInput IUiBoardComponent.Input => MyInput;

//    private Transform MyTransform { get; set; }
//    private Collider MyCollider { get; set; }
//    private SpriteRenderer[] MyRenderers { get; set; }
//    private SpriteRenderer MyRenderer { get; set; }
//    private MeshRenderer[] MyMRenderers { get; set; }
//    private MeshRenderer MyMRenderer { get; set; }
//    private Rigidbody MyRigidbody { get; set; }
//    private IMouseInput MyInput { get; set; }
//    public Camera MainCamera => Camera.main;
//    public MonoBehaviour MonoBehavior => this;

//    public void OnStartGame(IPlayer starter)
//    {
//      Debug.Log("OnStartGame");
//      OnRestart();
//    }

//    public void OnRestart()
//    {
//      Debug.Log("OnRestart");
//      // TODO: Reset the jewels in the board model

//      // TODO: Collect the jewels from the pool and add them to the board

//    }



//    public void DisableBoard()
//    {
//      throw new NotImplementedException();
//    }

//    public void DisableJewels()
//    {
//      throw new NotImplementedException();
//    }

//    public void EnableBoard()
//    {
//      throw new NotImplementedException();
//    }

//    public void EnableJewels()
//    {
//      throw new NotImplementedException();
//    }

//    public void SelectJewel(IUiJewel jewel)
//    {
//      throw new NotImplementedException();
//    }

//    public void SwapSelected()
//    {
//      throw new NotImplementedException();
//    }

//    public void UnselectJewel(IUiJewel jewel)
//    {
//      throw new NotImplementedException();
//    }
//  }
//}
