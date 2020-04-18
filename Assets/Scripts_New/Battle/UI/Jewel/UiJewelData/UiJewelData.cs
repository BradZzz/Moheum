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
    Action<IJewelData> OnSetData { get; set; }
    void SetData(IRuntimeJewel card);
  }

  //--------------------------------------------------------------------------------------------------------------

  public class UiJewelDataComponent : MonoBehaviour, IUiJewelData
  {
    /// <summary>
    ///     Set a jewel.
    /// </summary>
    /// <param name="jewel"></param>
    public void SetData(IRuntimeJewel jewel)
    {
      RuntimeData = jewel;
      OnSetData?.Invoke(StaticData);
    }

    /// <summary>
    ///     Static jewel data reference.
    /// </summary>
    public IJewelData StaticData => RuntimeData.Data;

    /// <summary>
    ///     Fired when a jewel model is assigned to this card.
    /// </summary>
    public Action<IJewelData> OnSetData { get; set; } = data => { };

    /// <summary>
    ///     Jewel correspondent in the game model.
    /// </summary>
    public IRuntimeJewel RuntimeData { get; private set; }
  }
}