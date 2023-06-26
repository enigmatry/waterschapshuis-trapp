namespace Waterschapshuis.CatchRegistration.Data.AnonymiseDataTool.Services
{
    public class WaterschapshuisCodes
    {
        public string Code { get; set; }
        public string CodeV2 { get; set; }

        public static WaterschapshuisCodes FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');
            var codes = new WaterschapshuisCodes {Code = values[0], CodeV2 = values[1]};
            return codes;
        }
    }
}
