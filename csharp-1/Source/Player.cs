using System;
using System.Collections.Generic;
using System.Text;

namespace Source
{
    public class Player
    {
        public Player(long playerId,long teamId, string name, DateTime birthDate, int skillLevel, decimal salary, bool isCaptain)
        {
            PlayerId = playerId;
            TeamId = teamId;
            Name = name;
            BirthDate = birthDate;
            SkillLevel = skillLevel;
            Salary = salary;
            IsCaptain = isCaptain;
        }
        public long PlayerId { get; set; }
        public long TeamId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int SkillLevel { get; set; }
        public decimal Salary { get; set; }
        public bool IsCaptain { get; set; }


    }
}
