﻿namespace datingapp_api.Data.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
