using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PokerClub.Models
{

    public class PokerTable
    {
        public PokerTable()
        {
            PokerTableHasMembers = new HashSet<PokerTableHasMembers>();

        }
        public int Id { get; set; }
        //public string Name { get; set; }
        public string Host { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public ICollection<PokerTableHasMembers> PokerTableHasMembers { get; set; }

    }

    public class PokerTableHasMembers
    {
        public int Id { get; set; }
        public string MemberId { get; set; }
        public int PokerTableId { get; set; }
        public virtual PokerTable PokerTable { get; set; }

    }

    public class ValueCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PokerTableValuation
    {
        public int Id { get; set; }
        public string MemberId { get; set; }
        public int PokerTableId { get; set; }
        public virtual PokerTable PokerTable { get; set; }
        public int ValueCategoryId { get; set; }
        public virtual ValueCategory ValueCategory { get; set; }
        public double Amount { get; set; }
    }
}

