using System.ComponentModel;
using System.Reflection;

public enum MyEnum
{
    [Description("GrapperAScanScene")]
    GrapperAScanScene,
    [Description("GrapperBScanScene")]
    GrapperBScanScene,
    [Description("GrapperCScanScene")]
    GrapperCScanScene,
    [Description("LHScanScene")]
    LHScanScene,
    [Description("FieldDevicesScene")]
    FieldDevicesScene,
    [Description("LoginScene")]
    LoginScene,
    [Description("PLCBoxGrapA")]
    PLCBoxGrapA,
    [Description("PLCBoxGrapB")]
    PLCBoxGrapB,
    [Description("PLCBoxGrapC")]
    PLCBoxGrapC,
    [Description("PLCBoxLH")]
    PLCBoxLH,
    [Description("MenuScene")]
    MenuScene,
    [Description("SelectionsScene")]
    SelectionsScene,
}
public static class EnumExtensions
{
    public static string GetDescription(this MyEnum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        DescriptionAttribute attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute == null ? value.ToString() : attribute.Description;
    }
}
