using System;

namespace Waterschapshuis.CatchRegistration.Core.GeoJSON.Models
{
    public class Properties
    {
        private Properties()
        {
            
        }
        public int Id { get; set; }
        public int Type { get; set; }
        public string Uurhok { get; set; } = String.Empty;

        public static Properties Create()
        {
            return new Properties();
        }
    }
}
