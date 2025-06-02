using AdventOfCode.Application.Extensions;

namespace AdventOfCode.Application.Attributes;

public class TypedAttributes
{
    private readonly Dictionary<string, object> _attributes;

    internal IDictionary<string, object> Attributes => _attributes;

    public TypedAttributes()
    {
        _attributes = new Dictionary<string, object>();
    }

    internal TypedAttributes(Dictionary<string, object> attributes)
    {
        _attributes = attributes ?? new Dictionary<string, object>();
    }

    public bool HasAttribute(string attributeName) => _attributes.ContainsKey(attributeName);

    public int GetIntAttribute(string attributeName, int defaultValue = default) =>
        GetAttributeValue(attributeName).ToIntOrDefault(defaultValue);

    public void SetIntAttribute(string attributeName, int value) => AddOrUpdateAttributeValue(attributeName, value);

    public long GetLongAttribute(string attributeName, long defaultValue = default) =>
        GetAttributeValue(attributeName).ToLongOrDefault(defaultValue);

    public void SetLongAttribute(string attributeName, long value) =>
        AddOrUpdateAttributeValue(attributeName, value);

    public decimal GetDecimalAttribute(string attributeName, decimal defaultValue = default) =>
        GetAttributeValue(attributeName).ToDecimalOrDefault(defaultValue);

    public void SetDecimalAttribute(string attributeName, decimal value) =>
        AddOrUpdateAttributeValue(attributeName, value);

    public DateTime GetDateTimeAttribute(string attributeName, DateTime defaultValue = default) =>
        GetAttributeValue(attributeName).ToDateTimeOrDefault(defaultValue);

    public void SetDateTimeAttribute(string attributeName, DateTime value) =>
        AddOrUpdateAttributeValue(attributeName, value);

    public bool GetBoolAttribute(string attributeName, bool defaultValue = default) =>
        GetAttributeValue(attributeName).ToBoolOrDefault(defaultValue);

    public void SetBoolAttribute(string attributeName, bool value) =>
        AddOrUpdateAttributeValue(attributeName, value);

    public string GetStringAttribute(string attributeName, string defaultValue = default) =>
        GetAttributeValue(attributeName).ToStringOrDefault(defaultValue);

    public void SetStringAttribute(string attributeName, string value) =>
        AddOrUpdateAttributeValue(attributeName, value);

    private object GetAttributeValue(string attributeName)
    {
        if (_attributes.TryGetValue(attributeName, out var value))
            return value;

        return null;
    }

    private void AddOrUpdateAttributeValue(string attributeName, object value)
    {
        if (string.IsNullOrWhiteSpace(attributeName))
            throw new ArgumentException("You must specify an attribute name");
        if (attributeName.Contains(" "))
            throw new ArgumentException(
                "The attribute name is not allowed to contain white space. Use the same naming rules like you would for a regular property...");

        if (_attributes.ContainsKey(attributeName))
        {
            _attributes[attributeName] = value;
        }
        else
        {
            _attributes.Add(attributeName, value);
        }
    }
}
