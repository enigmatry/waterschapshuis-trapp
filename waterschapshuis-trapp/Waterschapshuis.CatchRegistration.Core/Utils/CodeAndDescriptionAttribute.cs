using System.ComponentModel;

namespace Waterschapshuis.CatchRegistration.Core.Utils
{
    public class CodeAndDescriptionAttribute : DescriptionAttribute
    {
        public CodeAndDescriptionAttribute(string code, string description): base(description) => Code = code;

        public string Code { get; }
    }
}
