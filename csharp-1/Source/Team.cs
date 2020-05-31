using System;
using System.Collections.Generic;
using System.Text;

namespace Source
{
    public class Team
    {
        public Team(long teamId, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor, long captainId)
        {
            TeamId = teamId;
            Name = name;
            CreateDate = createDate;
            MainShirtColor = mainShirtColor;
            SecondaryShirtColor = secondaryShirtColor;
            CaptainId = captainId;
        }
        public long TeamId { get; set; }
        public string Name  { get; set; }
        public DateTime CreateDate { get; set; }
        public string MainShirtColor { get; set; }
        public string SecondaryShirtColor { get; set; }
        public long CaptainId { get; set; }
        
    }
}
