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

        //public void Add(Team team)
        //{
        //    Guid guid = Guid.NewGuid();
        //    string str = guid.ToString();
        //    team.TeamId = str;
        //    teamList.Add(team);
        //}

        public bool Add(Team team)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string str = guid.ToString();
                team.TeamId = str;
                team.Users = new List<string>();
                teamList.Add(team);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Remove(string teamId)
        {
            for (int i = 0; i < teamList.Count; i++)
            {
                Team temp = teamList.ElementAt(i);
                if (temp.TeamId.Equals(teamId))
                {
                    teamList.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public List<Team> getAllTeams()
        {
            return teamList;
        }

        public bool UpdateTeam(Team team)
        {
            for (int i = 0; i < teamList.Count; i++)
            {
                Team temp = teamList.ElementAt(i);
                if (temp.TeamId.Equals(team.TeamId))
                {
                    teamList[i] = team; //update user
                    return true;
                }
            }
            return false;
        }

        public Team getTeamById(string id)
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

        public bool addUser(string id, string teamId)
        {
            Team myTeam = getTeamById(teamId);
            if (myTeam != null)
            {
                myTeam.Users.Add(id);
                return true;
            }
            return false;
        }
    }
}