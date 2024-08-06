using Microsoft.VisualBasic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FaxDemo2._1
{
    public enum FaxEvent
    {
        fax_completed,

    }
    public class FaxWebhook
    {
        [JsonPropertyName("event_type")]
        public FaxEvent EventType { get; set; }

        [JsonPropertyName("fax")]
        public Fax Fax { get; set; }
    }

    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Recipient
    {
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("retry_count")]
        public int RetryCount { get; set; }

        [JsonPropertyName("completed_at")]
        public DateTime? CompletedAt { get; set; }

        [JsonPropertyName("bitrate")]
        public int Bitrate { get; set; }

        [JsonPropertyName("resolution")]
        public int Resolution { get; set; }

        [JsonPropertyName("error_type")]
        public object ErrorType { get; set; }

        [JsonPropertyName("error_id")]
        public object ErrorId { get; set; }

        [JsonPropertyName("error_message")]
        public object ErrorMessage { get; set; }
    }

    public class SendFaxRequest {
        public string To { get; set; }
        [JsonPropertyName("caller_id")]
        public string CallerId { get; set; }
        [JsonPropertyName("content_url")]
        public string ContentUrl{ get; set; }


    }
    public class Fax
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("direction")]
        public string Direction { get; set; }

        [JsonPropertyName("num_pages")]
        public int NumPages { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("is_test")]
        public bool IsTest { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("caller_id")]
        public string CallerId { get; set; }

        [JsonPropertyName("from_number")]
        public string FromNumber { get; set; }

        [JsonPropertyName("completed_at")]
        public string CompletedAt { get; set; }

        [JsonPropertyName("caller_name")]
        public string CallerName { get; set; }

        [JsonPropertyName("cost")]
        public int Cost { get; set; }

        [JsonPropertyName("tags")]
        public Tags Tags { get; set; }

        [JsonPropertyName("recipients")]
        public List<Recipient>? Recipients { get; set; }

        [JsonPropertyName("to_number")]
        public string ToNumber { get; set; }

        [JsonPropertyName("error_id")]
        public string ErrorId { get; set; }

        [JsonPropertyName("error_type")]
        public object ErrorType { get; set; }

        [JsonPropertyName("error_message")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("barcodes")]
        public List<object> Barcodes { get; set; }
    }

    public class Tags
    {
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
    }


}