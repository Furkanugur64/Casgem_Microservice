﻿using MongoDB.Bson.Serialization.Attributes;

namespace CasgemMicroservices.Catalog.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CategoryID  { get; set; }
        [BsonIgnore]
        public Category Category { get; set; }
    }
}
