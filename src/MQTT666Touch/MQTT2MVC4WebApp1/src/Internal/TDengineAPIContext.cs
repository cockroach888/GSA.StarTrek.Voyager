using Microsoft.Net.Http.Headers;
using MQTT2MVC4WebApp1.Models;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

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

    private const string _dbname = "EdgeDetectionDB";

    private const string _host = "http://192.168.16.221:6041";

    private const string _post = $"rest/sql/{_dbname}";

    private const string _token = "Basic cm9vdDp0YW9zZGF0YQ==";


    private async Task<HttpStatusCode> Execution(string sqlString)
    {
        var client = new RestClient(_host);
        client.Authenticator = new HttpBasicAuthenticator("root", "taosdata");

        var request = new RestRequest(_post, Method.Post);
        request.AddStringBody(sqlString, DataFormat.None);

        var response = await client.PostAsync(request);

        return response.StatusCode;


        //using HttpClient client = new()
        //{
        //    BaseAddress = new Uri(_host),
        //    DefaultRequestHeaders =
        //    {
        //        { HeaderNames.Authorization, _token}
        //    }
        //};

        //using StringContent content = new StringContent(sqlString, Encoding.UTF8);

        //using HttpResponseMessage message = await client.PostAsync(_post, content);

        //return message.EnsureSuccessStatusCode().StatusCode;
    }


    public async Task<short> Insert(CheckItemsModel model)
    {
        if (model == null) return -1;

        StringBuilder sb = new();

        sb.Append($"insert into {_dbname}.{model.DeviceId} values (");
        sb.Append($"'{model.TimestampKey:yyyy-MM-dd HH:mm:ss.fff}',"); // ts timestamp
        sb.Append($"{model.ProductId},"); // product_id int
        sb.Append($"{model.GoodResult},"); // good_result int
        sb.Append($"{model.BadResult},"); // bad_result int
        sb.Append($"{model.ExceptionResult},"); // exception_result int
        sb.Append($"'{model.Note}',"); // note nchar(1000)
        sb.Append($"'{model.SavePath}',"); // save_path nchar(200)
        sb.Append($"{model.Data1},"); // d1 int
        sb.Append($"{model.Data2},"); // d2 int
        sb.Append($"{model.Data3},"); // d3 int
        sb.Append($"{model.Data4},"); // d4 int
        sb.Append($"{model.Data5},"); // d5 int
        sb.Append($"{model.Data6},"); // d6 int
        sb.Append($"{model.Data7},"); // d7 int
        sb.Append($"{model.Data8},"); // d8 int
        sb.Append($"{model.Data9},"); // d9 int
        sb.Append($"{model.Data10},"); // d10 int
        sb.Append($"{model.Data11},"); // d11 int
        sb.Append($"{model.Data12},"); // d12 int
        sb.Append($"{model.Data13},"); // d13 int
        sb.Append($"{model.Data14},"); // d14 int
        sb.Append($"{model.Data15},"); // d15 int
        sb.Append($"{model.Data16},"); // d16 int
        sb.Append($"{model.Data17},"); // d17 int
        sb.Append($"{model.Data18},"); // d18 int
        sb.Append($"{model.Data19},"); // d19 int
        sb.Append($"{model.Data20}"); // d20 int
        sb.Append(");");

        HttpStatusCode result = await Execution(sb.ToString());
        return (short)result;
    }

    public async Task<short> Update()
    {
        HttpStatusCode result = await Execution("show stables;").ConfigureAwait(false);
        return (short)result;
    }

    public void Delete(CheckItemsModel model)
    {
        // do something.
    }

    public void Select()
    {
        // do something.
    }
}
