using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Team
{
    public class TeamRequest
    {
        List<Team> teamList;
        static TeamRequest teamReq = null;

        private TeamRequest()
        {
            teamList = new List<Team>();
        }

        public static TeamRequest getInstance()
        {
            if (teamReq == null)
            {
                teamReq = new TeamRequest();
                return teamReq;
            }
            else
            {
                return teamReq;
            }
        }

        public void Add(Team team)
        {
            teamList.Add(team);
        }

        public String Remove(int teamId)
        {
            for (int i = 0; i < teamList.Count; i++)
            {
                Team temp = teamList.ElementAt(i);
                if (temp.TeamId.Equals(teamId))
                {
                    teamList.RemoveAt(i);
                    return "Delete successful";
                }
            }
            return "Delete unsuccessful";
        }

        public List<Team> getAllTeams()
        {
            return teamList;
        }

        public String UpdateTeam(Team team)
        {
            for (int i = 0; i < teamList.Count; i++)
            {
                Team temp = teamList.ElementAt(i);
                if (temp.TeamId.Equals(team.TeamId))
                {
                    teamList[i] = team; //update user
                    return "Update successful";
                }
            }
            return "Update unsuccessful";
        }

        public Team getTeamById(int id)
        {
            for (int i = 0; i < teamList.Count; i++)
            {
                Team temp = teamList.ElementAt(i);
                if (temp.TeamId.Equals(id))
                {
                    return temp;
                }
            }
            return null;
        }
    }
}