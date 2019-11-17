using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace backend.Models
{
    public class DatabaseAccessModel
    {
        public static string GetConnectionString()
        {
            return "Host=localhost;port=3306;Username=root;Password=;Database=saitynai;Allow User Variables=True";
        }

        //USER DATABASE COMMANDS

        public static User GetUserByUsername(string username)
        {
            User user = new User();
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from User where username = '" + username + "'";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader["userId"].Equals(null))
                    {
                        user.Username = reader["username"].ToString();
                        user.Password = reader["password"].ToString();
                    }
                    else
                    {
                        user = null;
                    }
                }
                conn.Close();
            }
            return user;
        }

        public static bool AddUserToDatabase(User user)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Insert into User(Username,password,age) VALUES(?username,?password,?age)";
                cmd.Parameters.AddWithValue("username", user.Username);
                cmd.Parameters.AddWithValue("password", user.Password);
                cmd.Parameters.AddWithValue("age", user.Age);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        public static IEnumerable<User> GetUserDataListFromSql()
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from User";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    yield return new User
                    {
                        id = int.Parse(reader["userid"].ToString()),
                        Username = reader["username"].ToString(),
                        Password = "",
                        Age = int.Parse(reader["Age"].ToString())
                    };
                }
            }
        }

        public static User GetUserDataListFromSqlById(int id)
        {
            User user = new User();
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from User where userId = '" + id + "'";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader["userId"].Equals(null))
                    {
                        user.id = int.Parse(reader["userId"].ToString());
                        user.Username = reader["username"].ToString();
                        user.Password = "";
                        user.Age = int.Parse(reader["age"].ToString());
                        try
                        {
                            user.fk_TeamId = int.Parse(reader["fk_teamId"].ToString());
                        }
                        catch (Exception ex)
                        { }
                    }
                    else
                    {
                        user = null;
                    }
                }
                conn.Close();
            }
            return user;
        }

        public static bool UpdateUserToDatabase(User user, int userid)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update User Set Username=?Username,age=?age where userId='" + userid + "'";
                cmd.Parameters.AddWithValue("Username", user.Username);
                cmd.Parameters.AddWithValue("age", user.Age);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        public static bool CheckUserExists(int id)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select Count(*) from User  where userId = '" + id + "'";
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        //TEAM DATABASE COMMANDS
        public static bool AddUserToDatabaseByTeam(User user, int teamid)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Insert into User(fk_teamId) VALUES(?teamId) where userId = ?userId";
                cmd.Parameters.AddWithValue("teamId", teamid);
                cmd.Parameters.AddWithValue("userId", user.id);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        public static bool AddNewUserToDatabaseByTeam(User user, int teamid)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Insert into User(username,password,age,fk_teamId) VALUES(?username,?password,?age,?teamId)";
                cmd.Parameters.AddWithValue("teamId", teamid);
                cmd.Parameters.AddWithValue("username", user.Username);
                cmd.Parameters.AddWithValue("password", user.Password);
                cmd.Parameters.AddWithValue("age", user.Age);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }
        public static IEnumerable<Team> GetTeamDataListFromSql()
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from Team";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    yield return new Team
                    {
                        id = int.Parse(reader["teamId"].ToString()),
                        Name = reader["name"].ToString(),
                        fk_MatchId = int.Parse(reader["fk_matchId"].ToString())
                    };
                }
            }
        }

        //public static IEnumerable<Team> GetTeamInMatchListFromSql(int matchId)
        //{
        //    using (var conn = new MySqlConnection(GetConnectionString()))
        //    {
        //        conn.Open();
        //        MySqlDataReader reader;
        //        MySqlCommand cmd = conn.CreateCommand();
        //        cmd.CommandText = "Select * from Team where fk_matchId=?matchId";
        //        cmd.Parameters.AddWithValue("matchId", matchId);
        //        reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            yield return new Team
        //            {
        //                id = int.Parse(reader["teamId"].ToString()),
        //                Name = reader["name"].ToString(),
        //                fk_MatchId = int.Parse(reader["fk_matchId"].ToString())
        //            };
        //        }
        //    }
        //}

        public static Team GetTeamDataListFromSqlbyId(int id)
        {
            Team team = new Team();
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from Team where teamId = '" + id + "'";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader["teamId"].Equals(null))
                    {
                        team.id = int.Parse(reader["teamId"].ToString());
                        team.Name = reader["name"].ToString();
                        team.fk_MatchId = int.Parse(reader["fk_matchId"].ToString());
                    }
                    else
                    {
                        team = null;
                    }
                }
                conn.Close();
            }
            return team;
        }
        public static bool AddTeamToDatabase(Team team)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Insert into Team(Name) VALUES(?name)";
                cmd.Parameters.AddWithValue("name", team.Name);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
        }

        public static bool UpdateTeamToDatabase(int id, Team team)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update Team Set name=?name where teamId='" + id + "'";
                cmd.Parameters.AddWithValue("name", team.Name);
                //cmd.Parameters.AddWithValue("fk_matchId", team.fk_MatchId);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        public static bool UpdateTeamToDatabaseByMatch(int teamId, int matchId, Team team)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update Team Set name=?name,fk_matchId=?fk_matchId where teamId='" + teamId + "'";
                cmd.Parameters.AddWithValue("name", team.Name);
                cmd.Parameters.AddWithValue("fk_matchId", matchId);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        public static bool CheckTeamExists(int id)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select Count(*) from Team  where teamId = '" + id + "'";
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        //MATCH DATABASE COMMANDS
        //       UPDATE/PUT
        public static bool AddTeamToDatabaseByMatch(Team team, int matchId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                //cmd.CommandText = "Insert into team(fk_matchId) VALUES(?matchId) where teamId = ?teamId";
                cmd.CommandText = "Insert into team(name, fk_matchId) Values(?name,?fk_matchId)";
                cmd.Parameters.AddWithValue("fk_matchId", matchId);
                cmd.Parameters.AddWithValue("name", team.Name);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        public static bool AddNewTeamToDatabaseByMatch(Team team, int matchId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Insert into team(name,fk_matchId) VALUES(?name,?matchId)";
                cmd.Parameters.AddWithValue("name", team.Name);
                cmd.Parameters.AddWithValue("matchId", matchId);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }
        public static IEnumerable<Match> GetMatchDataListFromSql()
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from Match";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    yield return new Match
                    {
                        id = int.Parse(reader["matchId"].ToString()),
                        Name = reader["name"].ToString(),
                        fk_TournamentId = int.Parse(reader["fk_tournamentId"].ToString())
                    };
                }
            }
        }
        public static Match GetMatchDataListFromSqlbyId(int id)
        {
            Match match = new Match();
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from matches where matchId = '" + id + "'";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader["matchId"].Equals(null))
                    {
                        match.id = int.Parse(reader["matchId"].ToString());
                        match.Name = reader["name"].ToString();
                        match.fk_TournamentId = int.Parse(reader["fk_tournamentId"].ToString());
                    }
                    else
                    {
                        match = null;
                    }
                }
                conn.Close();
            }
            return match;
        }
        public static bool AddMatchToDatabase(Match match)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Insert into matches(name,date) VALUES(?name,?date)";
                cmd.Parameters.AddWithValue("name", match.Name);
                cmd.Parameters.AddWithValue("date", match.Date);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
        }

        public static bool UpdateMatchToDatabase(int id, Match match)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update matches Set name=?name,date=?date where matchId='" + id + "'";
                cmd.Parameters.AddWithValue("name", match.Name);
                cmd.Parameters.AddWithValue("date", match.Date);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        public static bool CheckMatchExists(int id)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select Count(*) from matches where matchId = '" + id + "'";
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        //TOURNAMENT DATABASE COMMANDS
        //       UPDATE/PUT
        public static bool AddMatchToDatabaseByTournament(Match match, int tournamentId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Insert into matches(fk_tournamentId) VALUES(?tournamentId) where matchId = ?matchId";
                cmd.Parameters.AddWithValue("tournamentId", tournamentId);
                cmd.Parameters.AddWithValue("matchId", match.id);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        public static bool AddNewMatchToDatabaseByTournament(Match match, int tournamentId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Insert into matches(name,date,fk_tournamentId) VALUES(?name,?date,?tournamentId)";
                cmd.Parameters.AddWithValue("name", match.Name);
                cmd.Parameters.AddWithValue("date", match.Date);
                cmd.Parameters.AddWithValue("tournamentId", tournamentId);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }
        public static IEnumerable<Tournament> GetTournamentListFromSql()
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from tournament";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    yield return new Tournament
                    {
                        id = int.Parse(reader["tournamentId"].ToString()),
                        Name = reader["name"].ToString()
                    };
                }
            }
        }
        public static Tournament GetTournamentListFromSqlbyId(int id)
        {
            Tournament tournament = new Tournament();
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from tournament where tournamentId = '" + id + "'";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader["tournamentid"].Equals(null))
                    {
                        tournament.id = int.Parse(reader["tournamentid"].ToString());
                        tournament.Name = reader["name"].ToString();
                    }
                    else
                    {
                        tournament = null;
                    }
                }
                conn.Close();
            }
            return tournament;
        }
        public static bool AddTournamentToDatabase(Tournament tournament)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Insert into tournament(name) VALUES(?name)";
                cmd.Parameters.AddWithValue("name", tournament.Name);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
        }

        public static bool UpdateTournamentToDatabase(int id, Tournament tournament)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update Tournament Set name=?name where tournamentId='" + id + "'";
                cmd.Parameters.AddWithValue("name", tournament.Name);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        public static bool CheckTournamentExists(int id)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select Count(*) from tournament where tournamentId = '" + id + "'";
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        public static bool CheckMatchInTournamentExists(int tournamentId, int matchId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from matches where matchId=?matchId and fk_tournamentId=?tournamentId";
                cmd.Parameters.AddWithValue("matchId", matchId);
                cmd.Parameters.AddWithValue("tournamentId", tournamentId);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        public static bool CheckTeamInMatchExists(int matchId, int teamId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from team where teamId=?teamId and fk_matchId=?fk_matchId";
                cmd.Parameters.AddWithValue("teamId", teamId);
                cmd.Parameters.AddWithValue("fk_matchId", matchId);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }

            }
        }

        public static IEnumerable<Team> GetTeamListByMatchId(int matchId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from team where fk_matchId = '" + matchId + "'";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    yield return new Team
                    {
                        id = int.Parse(reader["teamId"].ToString()),
                        Name = reader["name"].ToString(),
                        fk_MatchId = int.Parse(reader["fk_matchId"].ToString())
                    };
                }
            }
        }

        public static IEnumerable<Match> GetMatchListByTournamentId(int tournamentId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from matches where fk_tournamentId = '" + tournamentId + "'";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    yield return new Match
                    {
                        id = int.Parse(reader["matchId"].ToString()),
                        Name = reader["name"].ToString(),
                        fk_TournamentId = int.Parse(reader["fk_tournamentId"].ToString())
                    };
                }
            }
        }

        public static bool DeleteUser(int userId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Delete from user where userId=?userId";
                cmd.Parameters.AddWithValue("userId", userId);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
        }

        public static bool DeleteTeam(int teamId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "update user set fk_teamId=null where fk_teamId=?teamId; Delete from team where teamId=?teamId";
                cmd.Parameters.AddWithValue("teamId", teamId);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
        }

        //public static void DeleteTeamFromMatch(int teamId, int matchId)
        //{ }

        public static bool DeleteMatchFromTournament(int matchId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select @temp := teamId from team where fk_matchId = ?matchId; update user set fk_teamId=null where fk_teamId = @temp; delete from team where fk_matchId=?matchId; delete from matches where matchId=?matchId";
                cmd.Parameters.AddWithValue("matchId", matchId);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
        }

        public static bool DeleteTournament(int tournamentId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select @m := matchId from matches where fk_tournamentId = ?tournamentId; select @t := teamId from team where fk_matchId = @m; update user set fk_teamId=null where fk_teamId = @t; delete from team where fk_matchId = @m; delete from matches where fk_tournamentId = ?tournamentId; delete from tournament where tournamentId=?tournamentId";
                cmd.Parameters.AddWithValue("tournamentId", tournamentId);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount >= 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
        }

        public static void AddTeam(Team team)
        { }
    }
}