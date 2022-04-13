using MQTT2MVC4WebApp1.Common;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MQTT2MVC4WebApp1.Models;

/// <summary>
/// 检测记录项实体类
/// </summary>
[Serializable]
public sealed class CheckItemsModel
{
    /// <summary>
    /// 时间戳
    /// </summary>
    [JsonPropertyName("ts")]
    [JsonConverter(typeof(JsonTimestampConverter))]
    public DateTime TimestampKey { get; set; }

    /// <summary>
    /// 设备编号
    /// </summary>
    [JsonPropertyName("device_id")]
    public string? DeviceId { get; set; }

    /// <summary>
    /// 产品编号
    /// </summary>
    [JsonPropertyName("product_id")]
    public int ProductId { get; set; }

    /// <summary>
    /// 模型编号
    /// </summary>
    [JsonPropertyName("model_id")]
    public int ModelId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("good_result")]
    public int GoodResult { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("bad_result")]
    public int BadResult { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("exception_result")]
    public int ExceptionResult { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("note")]
    public string? Note { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("save_path")]
    public string? SavePath { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d1")]
    public int Data1 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d2")]
    public int Data2 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d3")]
    public int Data3 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d4")]
    public int Data4 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d5")]
    public int Data5 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d6")]
    public int Data6 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d7")]
    public int Data7 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d8")]
    public int Data8 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d9")]
    public int Data9 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d10")]
    public int Data10 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d11")]
    public int Data11 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d12")]
    public int Data12 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d13")]
    public int Data13 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d14")]
    public int Data14 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d15")]
    public int Data15 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d16")]
    public int Data16 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d17")]
    public int Data17 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d18")]
    public int Data18 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d19")]
    public int Data19 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("d20")]
    public int Data20 { get; set; }

    #region 成员方法

    /// <summary>
    /// Converts an object or value to or from JSON.
    /// </summary>
    /// <remarks>The type of object or value handled by the converter.</remarks>
    private sealed class JsonTimestampConverter : JsonConverter<DateTime>
    {
        /// <summary>
        /// Reads and converts the JSON to type T.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        /// <returns>The converted value.</returns>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.Number)
            {
                return default;
            }

            DateTime result = DateTime.MinValue;

            if (reader.TryGetInt64(out long value))
            {
                result = DateTimeHelper.GetDateTimeAsync(value).GetAwaiter().GetResult();
            }

            return result;
        }

        /// <summary>
        /// Writes a specified value as JSON.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to convert to JSON.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{DateTimeHelper.GetTimestampAsync(value).GetAwaiter().GetResult()}");
        }
    }

    #endregion

}
