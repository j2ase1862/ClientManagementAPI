namespace ClientManagementAPI.Models
{
    public class ClientData
    {
        public int Id { get; set; }
        public string ClientType { get; set; } // Client Type (e.g., Vision, AMR)
        public string IpAddress { get; set; } // IP Address
        public string Description { get; set; } // Description of the client
        public string CreateAt { get; set; } // Creation timestamp (ISO 8601 format)
    }
}