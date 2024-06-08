using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Models
{
    public class Car
    {
        public static readonly string INSERT = "INSERT INTO Car (LicensePlate, Name, ModelYear, FabricationYear, Color, Sold) VALUES (@LicensePlate, @Name, @ModelYear, @FabricationYear, @Color, @Sold)";

        public static readonly string GET = " SELECT LicensePlate, Name, ModelYear, FabricationYear, Color, Sold From Car WHERE LicensePlate = @LicensePlate ";
        public static readonly string GETALL = " SELECT LicensePlate, Name, ModelYear, FabricationYear, Color, Sold FROM Car ";
        [JsonProperty("licensePlate")]
        [Key]
        public string LicensePlate { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("modelYear")]
        public int ModelYear { get; set; }
        [JsonProperty("fabricationYear")]
        public int FabricationYear { get; set; }
        [JsonProperty("cor")]
        public string Color { get; set; }
        [JsonProperty("sold")]
        public bool Sold { get; set; }

        public XElement? GetXMLDocument()
        {
            return new XElement("car",
                    new XElement("licensePlate", LicensePlate),
                    new XElement("name", Name),
                    new XElement("modelYear", ModelYear),
                    new XElement("fabricationYear", FabricationYear),
                    new XElement("color", Color)
            );
        }
        public override string ToString()
        {
            return $"Placa: {LicensePlate}, Nome: {Name}, Ano Modelo: {ModelYear}, Ano Fabricação: {FabricationYear}, Cor: {Color}";
        }
    }
}
