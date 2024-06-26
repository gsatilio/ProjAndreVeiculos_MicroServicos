﻿using Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Pix
    {
        public readonly static string INSERT = " INSERT INTO PIX (IdPixType, PixKey) VALUES (@IdPixType, @PixKey); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = " SELECT A.Id, A.PixKey, B.Id, B.Name FROM Pix A INNER JOIN PixType B ON A.PixTypeId = B.Id ";
        public readonly static string GET = " SELECT A.Id, A.PixKey, B.Id, B.Name FROM Pix A INNER JOIN PixType B ON A.PixTypeId = B.Id WHERE A.Id = @IdPix ";
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("pixtype")]
        public PixType PixType { get; set; }
        [JsonProperty("pixkey")]
        public string PixKey { get; set; }

        public Pix()
        {
            
        }

        public Pix(PixDTO pixDTO)
        {
            this.PixKey = pixDTO.PixKey;
        }
    }
}
