using System;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.UiJewelData
{
  //--------------------------------------------------------------------------------------------------------------

  /// <summary>
  ///     RuntimeData stored inside the an UI card.
  /// </summary>
  public interface IUiJewelData
  {
    IRuntimeJewel RuntimeData { get; }
    IJewelData StaticData { get; }
    Action<IRuntimeJewel> OnSetData { get; set; }
    Action<IRuntimeJewel> OnAfterSetData { get; set; }
    void SetData(IRuntimeJewel card);
  }

  //--------------------------------------------------------------------------------------------------------------

  public class UIJewelDataComponent : MonoBehaviour, IUiJewelData
  {
    /// <summary>
    ///     Set a jewel.
    /// </summary>
    /// <param name="jewel"></param>
    public void SetData(IRuntimeJewel jewel)
    {
      RuntimeData = jewel;
      OnSetData?.Invoke(RuntimeData);
      OnAfterSetData?.Invoke(RuntimeData);
    }

    /// <summary>
    ///     Static jewel data reference.
    /// </summary>
    public IJewelData StaticData => RuntimeData.Data;

    /// <summary>
    ///     Fired when a jewel model is assigned to this uijewel.
    /// </summary>
    public Action<IRuntimeJewel> OnSetData { get; set; }
    public Action<IRuntimeJewel> OnAfterSetData { get; set; }

    /// <summary>
    ///     Jewel correspondent in the game model.
    /// </summary>
    public IRuntimeJewel RuntimeData { get; private set; }
  }
}