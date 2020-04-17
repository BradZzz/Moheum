using Battle.UI.Board;
using Battle.UI.Jewel.UiJewelData;
using Battle.UI.Jewel.UiJewelStateMachine;
using Battle.UI.Utils;
using Battle.UI.Utils.Tools.UiTransform;
using UnityEngine;

namespace Battle.UI.Jewel
{
  [RequireComponent(typeof(Collider))]
  [RequireComponent(typeof(Rigidbody))]
  [RequireComponent(typeof(IMouseInput))]
  [RequireComponent(typeof(IUiJewelData))]
  public class UiJewelComponent : MonoBehaviour, IUiJewel
  {
    //--------------------------------------------------------------------------------------------------------------

    #region Initialization and Unity Callbacks

    private void Awake()
    {
      //data
      Data = GetComponent<IUiJewelData>();

      //components
      MyTransform = transform;
      MyCollider = GetComponent<Collider>();
      MyRigidbody = GetComponent<Rigidbody>();
      MyInput = GetComponent<IMouseInput>();
      MyRenderers = GetComponentsInChildren<SpriteRenderer>();
      MyRenderer = GetComponent<SpriteRenderer>();
      MyMRenderers = GetComponentsInChildren<MeshRenderer>();
      MyMRenderer = GetComponent<MeshRenderer>();

      //transform
      Motion = new UiMotion(this);

      //fsm
      Fsm = new UiJewelBoardFsm(MainCamera, cardConfigsParameters, this);
    }

    /// <summary>
    ///     Initialize the component.
    /// </summary>
    public void Initialize()
    {
      if (IsInitialized)
        return;

      Board = transform.parent.GetComponentInChildren<IUiBoard>();
      IsInitialized = true;
    }

    private void Update()
    {
      if (!IsInitialized)
        return;

      Fsm?.Update();
      Motion?.Update();
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Properties
    public UiMotion Motion { get; private set; }
    SpriteRenderer[] IUiJewelComponents.Renderers => MyRenderers;
    SpriteRenderer IUiJewelComponents.Renderer => MyRenderer;
    MeshRenderer[] IUiJewelComponents.MRenderers => MyMRenderers;
    MeshRenderer IUiJewelComponents.MRenderer => MyMRenderer;
    Collider IUiJewelComponents.Collider => MyCollider;
    Rigidbody IUiJewelComponents.Rigidbody => MyRigidbody;
    IMouseInput IUiJewelComponents.Input => MyInput;
    IUiBoard IUiJewel.Board => Board;
    public string Name => gameObject.name;
    [SerializeField] public Battle.UI.Jewel.UiJewelParameters.UiJewelParameters cardConfigsParameters;
    private UiJewelBoardFsm Fsm { get; set; }
    private Transform MyTransform { get; set; }
    private Collider MyCollider { get; set; }
    private SpriteRenderer[] MyRenderers { get; set; }
    private SpriteRenderer MyRenderer { get; set; }
    private MeshRenderer[] MyMRenderers { get; set; }
    private MeshRenderer MyMRenderer { get; set; }
    private Rigidbody MyRigidbody { get; set; }
    private IMouseInput MyInput { get; set; }
    private IUiBoard Board { get; set; }
    public IUiJewelData Data { get; private set; }
    public MonoBehaviour MonoBehavior => this;
    public Camera MainCamera => Camera.main;
    //public bool IsDragging => Fsm.IsCurrent<UiCardDrag>();
    //public bool IsHovering => Fsm.IsCurrent<UiCardHover>();
    //public bool IsDisabled => Fsm.IsCurrent<UiCardDisable>();
    public bool IsDragging => false;
    public bool IsHovering => false;
    public bool IsDisabled => false;
    public bool IsInitialized { get; private set; }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Card Operations

    public void Hover()
    {
      Fsm.Hover();
    }

    public void Disable()
    {
      Fsm.Disable();
    }

    public void Enable()
    {
      Fsm.Enable();
    }

    public void Select()
    {
      //Board.SelectJewel(this);
      Fsm.Select();
    }

    public void Unselect()
    {
      Fsm.Unselect();
    }

    public void Draw()
    {
      Fsm.Draw();
    }

    public void Discard()
    {
      Fsm.Discard();
    }

    public void Play()
    {
      Fsm.Play();
    }

    public void Restart()
    {
      IsInitialized = false;
    }

    public void Target()
    {
      Fsm.Target();
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------
  }
}