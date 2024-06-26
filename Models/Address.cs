﻿using Models.DTO;
using Models.MyAPI;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Address
    {
        public readonly static string INSERT = " INSERT INTO Address (Street, CEP, Neighborhood, StreetType, Complement, Number, Uf, City) " +
            "VALUES (@Street, @CEP, @Neighborhood, @StreetType, @Complement, @Number, @Uf, @City); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = " SELECT Street, CEP, Neighborhood, ISNULL(StreetType,'') StreetType, ISNULL(Complement,'') Complement, Number, Uf, City, Id FROM Address";
        public readonly static string GET = " SELECT Street, CEP, Neighborhood, ISNULL(StreetType,'') StreetType, ISNULL(Complement,'') Complement, Number, Uf, City, Id FROM Address WHERE Id = @Id";
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        [JsonProperty("logradouro")]
        public string Street { get; set; }
        [JsonProperty("cep")]
        public string CEP { get; set; }
        [JsonProperty("bairro")]
        public string Neighborhood { get; set; }
        public string? StreetType { get; set; }
        [JsonProperty("complemento")]
        public string? Complement { get; set; }
        public int Number { get; set; }
        [JsonProperty("uf")]
        public string Uf { get; set; }
        [JsonProperty("localidade")]
        public string City { get; set; }

        public Address()
        {
            
        }

        public Address (AddressDTO addressDTO)
        {
            this.StreetType = addressDTO.StreetType;
            this.CEP = addressDTO.CEP;
            this.Complement = addressDTO.Complement;
            this.Number = addressDTO.Number;
        }
        public Address(AddressAPI addressAPI)
        {
            this.Id = addressAPI.Id;
            this.Street = addressAPI.Street;
            this.CEP = addressAPI.CEP;
            this.Neighborhood = addressAPI.Neighborhood;
            this.StreetType = addressAPI.StreetType;
            this.Number = addressAPI.Number;
            this.Complement = addressAPI.Uf;
            this.Uf = addressAPI.Uf;
            this.City = addressAPI.City;
        }
    }
}
