using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using Codenation.Challenge.Exceptions;
using Source;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        private SortedList<long ,Player> TablePlayers = new SortedList<long, Player>();
        private SortedList<long, Team> TableTeams = new SortedList<long, Team>();

        public SoccerTeamsManager()
        {
        }
        
        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            try
            {          
                TableTeams.Add(id, new Team(id, name, createDate, mainShirtColor, secondaryShirtColor, 0));
            }
            catch (ArgumentException)
            {
                throw new UniqueIdentifierException();
            }
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            try
            {
                TablePlayers.Add(id, new Player(id, teamId, name, birthDate, skillLevel, salary, false));              
            }
            catch (ArgumentException)
            {
                throw new UniqueIdentifierException();
            }
            
        }

        public void SetCaptain(long playerId)
        {
            ValidatePlayer(playerId);

            var tempCap = TablePlayers.Where(x => x.Key.Equals(playerId));

            foreach (var player in tempCap)
            {
                TablePlayers[playerId].IsCaptain = true;
                TableTeams[TablePlayers[playerId].TeamId].CaptainId = playerId;
            }
        }

        public long GetTeamCaptain(long teamId)
        {
            ValidateTeam(teamId);
            ValidateCaptain(teamId);

            return TableTeams[teamId].CaptainId; 
        }

        public string GetPlayerName(long playerId)
        {
            ValidatePlayer(playerId);
            return TablePlayers[playerId].Name;
        }       
        

        public string GetTeamName(long teamId)
        {
            ValidateTeam(teamId);

            return TableTeams[teamId].Name.ToString();
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            ValidateTeam(teamId);

            List<long> idsList = new List<long>();
            var ids = TablePlayers.Where(x => x.Value.TeamId.Equals(teamId)).ToList();
            foreach (var item in ids)
            {
                idsList.Add(item.Key);
            }
            return idsList;
        }

        public long GetBestTeamPlayer(long teamId)
        {
            ValidateTeam(teamId);

            long temp = 0;
            long id = 0;
            var bestPlayer = TablePlayers.Where(x => x.Value.TeamId.Equals(teamId));
            foreach (var item in bestPlayer)
            {
                if(item.Value.SkillLevel > temp)
                {
                    temp = item.Value.SkillLevel;
                    id = item.Key;
                }
            }
            return id;                 
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            ValidateTeam(teamId);

            int temp = DateTime.Now.Year;
            long id = 0;
            var OlderTeamPlayer = TablePlayers.Where(x => x.Value.TeamId.Equals(teamId));
            foreach (var player in OlderTeamPlayer)
            {
                if(player.Value.BirthDate.Year < temp) //modificar aqui caso precise de desempate pelo id
                {
                    temp = player.Value.BirthDate.Year;
                    id = player.Key;
                }
            }
            return id;          
        }

        public List<long> GetTeams()
        {
            if (TableTeams == null)
            {
                return new List<long>();
            }

            return TableTeams.OrderBy(x => x.Key).Select(x => x.Key).ToList();            
        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            ValidateTeam(teamId);

            decimal tempSalary = 0;
            long tempId = 0;
            var HigherSalaryPlayer = TablePlayers.Where(x => x.Value.TeamId.Equals(teamId));
            foreach (var player in HigherSalaryPlayer)                
            {
                if(player.Value.Salary > tempSalary)
                {
                    tempSalary = player.Value.Salary;
                    tempId = player.Key;
                }
            }
            return tempId;            
        }

        public decimal GetPlayerSalary(long playerId)
        {
            ValidatePlayer(playerId);
            return TablePlayers[playerId].Salary;            
        }

        public List<long> GetTopPlayers(int top)
        {
            List<long> topPlayers = new List<long>();
            if (TablePlayers == null)
            {
                return topPlayers;
            }
            return TablePlayers
                .OrderByDescending(x => x.Value.SkillLevel)
                .Select(x => x.Key)
                .Take(top)
                .ToList();
        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId) //teamId = id time da casa visitorTeamId = id time de fora
        {
            if (!TableTeams.ContainsKey(teamId) || !TableTeams.ContainsKey(visitorTeamId))
            {
                throw new TeamNotFoundException();
            }

            if(TableTeams[teamId].MainShirtColor == TableTeams[visitorTeamId].MainShirtColor)
            {
                return TableTeams[visitorTeamId].SecondaryShirtColor.ToString();
            }
            else
            {
                return TableTeams[visitorTeamId].MainShirtColor.ToString();
            }

        }

        public void ValidateTeam(long teamId)
        {
            if (!TableTeams.Any(x => x.Key.Equals(teamId)))
            {
                throw new TeamNotFoundException();
            }
        }
        public void ValidatePlayer(long playerId)
        {
            if (!TablePlayers.Any(x => x.Key.Equals(playerId)))
            {
                throw new PlayerNotFoundException();
            }
        }
        public void ValidateCaptain(long teamId)
        {
            if (TableTeams[teamId].CaptainId == 0)
            {
                throw new CaptainNotFoundException();
            }
        }
    }
}
