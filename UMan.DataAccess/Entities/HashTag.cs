﻿namespace UMan.DataAccess.Entities
{
    public class HashTag
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public List<Paper> Papers { get; set; }
    }
}
