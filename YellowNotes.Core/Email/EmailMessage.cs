﻿namespace YellowNotes.Core.Email
{
    public class EmailMessage
    {
        public string ToEmailAddress { get; set; }

        public string Subject { get; set; }
        
        public string Content { get; set; }
    }
}
