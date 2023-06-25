using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mati_Mongo.Models;

public class VehiclePointMongo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    //public ObjectId Id { get; set; }

    [BsonDateTimeOptions(DateOnly = false)]
    public DateTime DateTime { get; set; }

    [BsonElement("Longitude")]
    [JsonPropertyName("Longitude")]
    [Required(ErrorMessage = "Longitude is required")]
    public float Longitude { get; set; }

    public float Latitude { get; set; }
    public short Altitude { get; set; }
    public short Speed { get; set; }
    public float Temperature { get; set; }
    public byte Satellite { get; set; }
    public byte Batt { get; set; }
    public byte Status { get; set; }
    public short Humidity { get; set; }

    //[BsonRepresentation(BsonType.ObjectId)]
    //public List<string> Test { get; set; }
    //[BsonIgnore]
    //public List<Test> TestList { get; set; }
}