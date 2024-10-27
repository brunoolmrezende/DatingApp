﻿namespace API.Helpers
{
    public class UserParams : PaginationParams
    {
        public string? Gender { get; set; }
        public string? CurrentUsername { get; set; }
        public int MaxAge { get; set; } = 100;
        public int MinAge { get; set; } = 18;
        public string OrderBy { get; set; } = "lastActive";
    }
}
