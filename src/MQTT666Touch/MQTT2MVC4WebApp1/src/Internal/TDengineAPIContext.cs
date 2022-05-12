namespace MQTT2MVC4WebApp1.Internal;

/// <summary>
/// 类功能说明
/// </summary>
internal sealed class TDengineAPIContext
{

    #region 单例模式

    private static readonly Lazy<TDengineAPIContext> _lazyInstance = new(() => new TDengineAPIContext());

    /// <summary>
    /// 类功能说明
    /// </summary>
    internal static TDengineAPIContext Default { get; } = _lazyInstance.Value;

    #endregion

    #region 成员变量



    #endregion

    #region 成员属性



    #endregion

    #region 成员方法



    #endregion

}
