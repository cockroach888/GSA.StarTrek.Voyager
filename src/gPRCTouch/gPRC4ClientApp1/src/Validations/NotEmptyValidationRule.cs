using System.Globalization;
using System.Windows.Controls;

namespace gPRC4ClientApp1.Validations
{
    internal class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "对不起！服务端连接地址不能为空。")
                : ValidationResult.ValidResult;
        }
    }
}
