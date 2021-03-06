﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Server.Utilities;

namespace Server.Models
{
    public class Relation
    {
        readonly int hashCode;
        
        public Relation(MD5Sum antecedentId, MD5Sum descendantId)
        {
            AntecedentId = antecedentId;
            DescendantId = descendantId;
            hashCode = antecedentId.GetHashCode() ^ descendantId.GetHashCode();
        }

		[JsonProperty("antecedentId")]
		[JsonConverter(typeof(IdentifierConverter))]
        public MD5Sum AntecedentId { get; }

		[JsonProperty("descendantId")]
		[JsonConverter(typeof(IdentifierConverter))]
        public MD5Sum DescendantId { get; }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            if (obj is Relation)
            {
                var other = (Relation)obj;
                return AntecedentId.Equals(other.AntecedentId) && DescendantId.Equals(other.DescendantId);
            }
            return false;
        }
    }
}
