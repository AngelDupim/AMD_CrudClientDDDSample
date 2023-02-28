namespace AMD_CrudClientDDDSample.Infrastructure.Shared.Helper
{
    public class GenericParameterHelper
    {
        public static Dictionary<object, object> SetParameter(object field, object value)
        {
            var parameter = new Dictionary<object, object>() {
                { "Field", $"{field}" },
                { "Value", value}
            };
            return parameter;
        }
    }
}