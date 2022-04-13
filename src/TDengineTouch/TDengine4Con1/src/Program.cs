using System.Text;
using TDengineDriver;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("TDengine 时间序列数据库控制台示例程序。");
Console.WriteLine();
Console.WriteLine();


IntPtr conn = TDengine.Connect("192.168.16.221", "root", "taosdata", "EdgeDetectionDB", 0);
Console.WriteLine($"数据库连接句柄：{conn}");
Console.WriteLine();



StringBuilder sqlString86004 = new();
sqlString86004.Append("insert into m860004 using check_items tags (587723, 860004, 'O7234', '广东蒙牛一厂', '缺陷1|缺陷2|缺陷3|缺陷4') values ("); //

//sqlString86004.Append($"{DateTimeHelper.GetTimeStamp()},"); // ts timestamp
sqlString86004.Append($"'{DateTime.Now:G}',"); // ts timestamp
sqlString86004.Append("23000,"); // product_id int
sqlString86004.Append("0,"); // good_result int
sqlString86004.Append("1,"); // bad_result int
sqlString86004.Append("0,"); // exception_result int

// note nchar(1000)
sqlString86004.Append("'0:好品-MC4yMjI=(2219 1141 50 200)$0$0,错误2-MC4wMDE=(140 1406 24 25)$0$1,错误3-MzMz(1091 77 94 6)$0$2;1:好品-MC4yMjI=(1822 987 66 200)$0$0,错误2-MC4wMDE=(2230 65 34 1)$0$1,错误3-MzMz(1018 1840 52 9)$0$2;2:好品-MC4yMjI=(951 340 26 200)$0$0,错误2-MC4wMDE=(1666 661 15 10)$0$1,错误3-MzMz(2232 1555 108 3)$0$2;3:好品-MC4yMjI=(213 1738 86 200)$0$0,错误2-MC4wMDE=(189 1439 24 27)$0$1,错误3-MzMz(177 1051 128 9)$0$2;',");

// save_path nchar(200)
sqlString86004.Append("'/20211206/719493b9-5662-11ec-a5a6-005056b82349/',");


sqlString86004.Append("1,"); // d1 int
sqlString86004.Append("1,"); // d2 int
sqlString86004.Append("0,"); // d3 int
sqlString86004.Append("0,"); // d4 int
sqlString86004.Append("0,"); // d5 int
sqlString86004.Append("0,"); // d6 int
sqlString86004.Append("0,"); // d7 int
sqlString86004.Append("0,"); // d8 int
sqlString86004.Append("0,"); // d9 int
sqlString86004.Append("0,"); // d10 int
sqlString86004.Append("0,"); // d11 int
sqlString86004.Append("0,"); // d12 int
sqlString86004.Append("0,"); // d13 int
sqlString86004.Append("0,"); // d14 int
sqlString86004.Append("0,"); // d15 int
sqlString86004.Append("0,"); // d16 int
sqlString86004.Append("0,"); // d17 int
sqlString86004.Append("0,"); // d18 int
sqlString86004.Append("0,"); // d19 int
sqlString86004.Append("0"); // d20 int

sqlString86004.Append(");");

//ExecuteSQL(conn, sqlString86004.ToString());
IntPtr result86004 = TDengine.Query(conn, sqlString86004.ToString());
if (result86004 != IntPtr.Zero || TDengine.ErrorNo(result86004) != 0)
{
    string errorMsg = TDengine.Error(result86004);
    Console.WriteLine($"reason: {errorMsg}");
}
//TDengine.QueryAsync(conn, sqlString86004.ToString(), (param, taoRes, code) => { }, IntPtr.Zero);
int affectRows86004 = TDengine.AffectRows(result86004);
int fieldCount86004 = TDengine.FieldCount(result86004);
TDengine.FreeResult(result86004);

Console.WriteLine($"数据表 m860004 行数：{affectRows86004}");
Console.WriteLine($"数据表 m860004 字段数量：{fieldCount86004}");
Console.WriteLine();
Console.WriteLine();



StringBuilder sqlString175 = new();
sqlString175.Append("insert into m175 values ("); // using check_items tags (9570,175, '202109130183', '三元食品', '瓶身|瓶盖|瓶底|瓶脖')

//sqlString175.Append($"{DateTimeHelper.GetTimeStamp()},"); // ts timestamp
sqlString175.Append($"'{DateTime.Now:G}',"); // ts timestamp
sqlString175.Append("23000,"); // product_id int
sqlString175.Append("1,"); // good_result int
sqlString175.Append("0,"); // bad_result int
sqlString175.Append("0,"); // exception_result int

// note nchar(1000)
sqlString175.Append("'0:好品-MC4yMjI=(2219 1141 50 200)$0$0,错误2-MC4wMDE=(140 1406 24 25)$0$1,错误3-MzMz(1091 77 94 6)$0$2;1:好品-MC4yMjI=(1822 987 66 200)$0$0,错误2-MC4wMDE=(2230 65 34 1)$0$1,错误3-MzMz(1018 1840 52 9)$0$2;2:好品-MC4yMjI=(951 340 26 200)$0$0,错误2-MC4wMDE=(1666 661 15 10)$0$1,错误3-MzMz(2232 1555 108 3)$0$2;3:好品-MC4yMjI=(213 1738 86 200)$0$0,错误2-MC4wMDE=(189 1439 24 27)$0$1,错误3-MzMz(177 1051 128 9)$0$2;',");

// save_path nchar(200)
sqlString175.Append("'/20211206/719493b9-5662-11ec-a5a6-005056b82349/',");


sqlString175.Append("1,"); // d1 int
sqlString175.Append("0,"); // d2 int
sqlString175.Append("1,"); // d3 int
sqlString175.Append("1,"); // d4 int
sqlString175.Append("0,"); // d5 int
sqlString175.Append("0,"); // d6 int
sqlString175.Append("0,"); // d7 int
sqlString175.Append("0,"); // d8 int
sqlString175.Append("0,"); // d9 int
sqlString175.Append("0,"); // d10 int
sqlString175.Append("0,"); // d11 int
sqlString175.Append("0,"); // d12 int
sqlString175.Append("0,"); // d13 int
sqlString175.Append("0,"); // d14 int
sqlString175.Append("0,"); // d15 int
sqlString175.Append("0,"); // d16 int
sqlString175.Append("0,"); // d17 int
sqlString175.Append("0,"); // d18 int
sqlString175.Append("0,"); // d19 int
sqlString175.Append("0"); // d20 int

sqlString175.Append(");");

//ExecuteSQL(conn, sqlString175.ToString());
IntPtr result175 = TDengine.Query(conn, sqlString175.ToString());
if (result175 != IntPtr.Zero || TDengine.ErrorNo(result175) != 0)
{
    string errorMsg = TDengine.Error(result175);
    Console.WriteLine($"reason: {errorMsg}");
}
//TDengine.QueryAsync(conn, sqlString175.ToString(), (param, taoRes, code) => { }, IntPtr.Zero);
int affectRows175 = TDengine.AffectRows(result175);
int fieldCount175 = TDengine.FieldCount(result175);
TDengine.FreeResult(result175);

Console.WriteLine($"数据表 m175 行数：{affectRows175}");
Console.WriteLine($"数据表 m175 字段数量：{fieldCount175}");
Console.WriteLine();
Console.WriteLine();



int closeResult = TDengine.Close(conn);
Console.WriteLine($"数据库关闭结果：{closeResult}");
Console.WriteLine();

TDengine.Cleanup();



Console.WriteLine("按任意键结束程序...");
Console.ReadKey();



void ExecuteSQL(IntPtr conn, string sql)
{
    IntPtr res = TDengine.Query(conn, sql);
    // 检查查询是否成功
    if ((res == IntPtr.Zero) || (TDengine.ErrorNo(res) != 0))
    {
        Console.Write(sql + " failure, ");
        // 当Res是一个非空指针时，获得错误信息。
        if (res != IntPtr.Zero)
        {
            Console.Write("reason:" + TDengine.Error(res));
        }
    }
    else
    {
        Console.Write(sql + " success, {0} rows affected", TDengine.AffectRows(res));
        //... do something with res ...

        // 重要：需要释放结果以避免内存泄漏。
        TDengine.FreeResult(res);
    }
}



/// <summary>
/// 日期时间辅助类
/// </summary>
static class DateTimeHelper
{
    private static readonly DateTime _time1970 = new DateTime(1970, 01, 01).ToLocalTime();


    /// <summary>
    /// 获取从 1970-01-01 到现在的毫秒数（时间戳）。
    /// </summary>
    /// <returns>时间戳</returns>
    public static long GetTimeStamp()
    {
        return (long)(DateTime.Now.ToLocalTime() - _time1970).TotalSeconds;
    }

    /// <summary>
    /// 计算 1970-01-01 到指定 <see cref="DateTime"/> 的毫秒数（时间戳）。
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>时间戳</returns>
    public static long GetTimeStamp(DateTime dateTime)
    {
        return (long)(dateTime.ToLocalTime() - _time1970).TotalSeconds;
    }
}
