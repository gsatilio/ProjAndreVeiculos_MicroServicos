using Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class Employee : Person
    {
        public readonly static string INSERT = " INSERT INTO EMPLOYEE (Document, RoleId, ComissionValue, Comission, Name, DateOfBirth, AddressId, Phone, Email) VALUES (@Document, @RoleId, @ComissionValue, @Comission, @Name, @DateOfBirth, @AddressId, @Phone, @Email); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = " SELECT A.DateOfBirth, A.Document, A.Email, A.Name, A.Comission, A.ComissionValue, A.Phone, B.CEP, B.City, B.Complement, B.Id, B.Neighborhood, B.Number, B.Street, B.StreetType, B.Uf, C.Id, C.Description FROM Employee A INNER JOIN Address B ON A.AddressId = B.Id INNER JOIN Role C ON A.RoleId = C.Id ";
        public readonly static string GET = " SELECT A.DateOfBirth, A.Document, A.Email, A.Name, A.Comission, A.ComissionValue, A.Phone, B.CEP, B.City, B.Complement, B.Id, B.Neighborhood, B.Number, B.Street, B.StreetType, B.Uf, C.Id, C.Description FROM Employee A INNER JOIN Address B ON A.AddressId = B.Id INNER JOIN Role C ON A.RoleId = C.Id WHERE A.Document = @Document ";
        [JsonProperty("role")]
        public Role Role { get; set; }
        [JsonProperty("comissionvalue")]
        public Decimal ComissionValue { get; set; }
        [JsonProperty("comission")]
        public Decimal Comission { get; set; }

        public Employee()
        {
            
        }
        public Employee(EmployeeDTO dto)
        {
            this.ComissionValue = dto.ComissionValue;
            this.Comission = dto.Comission;
            this.Name = dto.Name;
            this.Document = dto.Document;
            this.Document = dto.Document;
            this.DateOfBirth = dto.DateOfBirth;
            this.Phone = dto.Phone;
            this.Email = dto.Email;
        }
    }
}
