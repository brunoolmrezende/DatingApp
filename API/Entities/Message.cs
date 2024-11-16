﻿namespace API.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public required string RecipientUsername { get; set; }
        public required string SenderUsername { get; set; }
        public required string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime DateSent { get; set; } = DateTime.UtcNow;
        public bool RecipientDeletd { get; set; }
        public bool SenderDeletd { get; set; }

        // navigation properties
        public int RecipientId { get; set; }
        public AppUser Recipient { get; set; } = null!;
        public int SenderId { get; set; }
        public AppUser Sender { get; set; } = null!;
    }
}
