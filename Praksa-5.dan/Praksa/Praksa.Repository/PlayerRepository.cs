using Npgsql;
using Praksa.Common;
using Praksa.Model;
using Praksa.Repository.Common;
using Praksa.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Praksa.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        FootballClubCommon fccommon = new FootballClubCommon();
        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();
            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {

                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"SELECT * FROM \"FootballClub\" INNER JOIN \"Player\" ON \"FootballClub\".\"Id\" = \"Player\".\"FootballClubId\"";
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        int? footballclubid = reader["FootballClubId"] as int?;
                        string firstname = reader["FirstName"] as string;
                        string lastname = reader["LastName"] as string;
                        int? age = reader["Age"] as int?;
                        FootballClub footballclub = null;
                        NpgsqlCommand cmdclub = new NpgsqlCommand();
                        cmdclub.Connection = connection;
                        NpgsqlDataReader clubReader = cmdclub.ExecuteReader();

                        if (clubReader.Read())
                        {
                            footballclub = new FootballClub
                            {
                                Id = (int)clubReader["Id"],
                                Name = clubReader["Name"] as string,
                                NumberOfTrophies = (int)clubReader["NumberOfTrophies"]
                            };
                        }
                        clubReader.Close();

                        Player player = new Player()
                        {
                            Id = id,
                            FootballClubId = footballclubid.Value,
                            FirstName = firstname,
                            LastName = lastname,
                            Age = age.Value,
                            FootballClub = footballclub,
                        };
                        players.Add(player);
                    }
                    reader.Close();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                return players;
            }
            catch 
            {
                return null;
            }
        }

        public Player GetPlayerById(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"SELECT * FROM \"Player\" WHERE \"Id\" = @Id";
                    cmd.Parameters.AddWithValue("Id", id);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int? footballclubid = reader["FootballClubId"] as int?;
                        string firstname = reader["FirstName"] as string;
                        string lastname = reader["LastName"] as string;
                        int? age = reader["Age"] as int?;

                        FootballClub footballclub = null;
                        NpgsqlCommand cmdclub = new NpgsqlCommand();
                        cmdclub.Connection = connection;
                        NpgsqlDataReader clubReader = cmdclub.ExecuteReader();

                        if (clubReader.Read())
                        {
                            footballclub = new FootballClub
                            {
                                Id = (int)clubReader["Id"],
                                Name = clubReader["Name"] as string,
                                NumberOfTrophies = (int)clubReader["NumberOfTrophies"]
                            };
                        }
                        clubReader.Close();

                        Player player = new Player()
                        {
                            Id = id,
                            FootballClubId = footballclubid.Value,
                            FirstName = firstname,
                            LastName = lastname,
                            Age = age.Value,
                            FootballClub = footballclub,
                        };

                        reader.Close();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        return player;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch 
            {
                return null;
            }
        }

        public void CreatePlayer(CreatedPlayer player)
        {
            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"INSERT INTO \"Player\" (\"Id\", \"FootballClubId\", \"FirstName\", \"LastName\", \"Age\") VALUES (@Id, @FootballClubId, @FirstName, @LastName, @Age)";
                    cmd.Parameters.AddWithValue("Id", player.Id);
                    cmd.Parameters.AddWithValue("FootballClubId", player.FootballClubId);
                    cmd.Parameters.AddWithValue("FirstName", player.FirstName);
                    cmd.Parameters.AddWithValue("LastName", player.LastName);
                    cmd.Parameters.AddWithValue("Age", player.Age);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                
            }

            catch 
            {
                throw;

            }
        }

        public void UpdatePlayer(int id, UpdatedPlayer player)
        {
            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand selectcmd = new NpgsqlCommand();
                    selectcmd.Connection = connection;
                    selectcmd.CommandText = $"SELECT COUNT(*) FROM \"Player\" WHERE \"Id\" = @Id";
                    selectcmd.Parameters.AddWithValue("Id", id);
                    int count = Convert.ToInt32(selectcmd.ExecuteScalar());
                    try
                    {
                        if (count > 0)
                        {
                            NpgsqlCommand updatecmd = new NpgsqlCommand();
                            updatecmd.Connection = connection;
                            updatecmd.CommandText = $"UPDATE \"Player\" SET \"FootballClubId\" = @FootballClubId, \"FirstName\" = @FirstName, \"LastName\" = @LastName, \"Age\" = @Age WHERE \"Id\" = @Id";
                            updatecmd.Parameters.AddWithValue("Id", id);
                            updatecmd.Parameters.AddWithValue("FootballClubId", player.FootballClubId);
                            updatecmd.Parameters.AddWithValue("FirstName", player.FirstName);
                            updatecmd.Parameters.AddWithValue("LastName", player.LastName);
                            updatecmd.Parameters.AddWithValue("Age", player.Age);
                            updatecmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    catch
                    {
                        throw;
                    }

                    
                   
                }

            }
            catch 
            {
                throw;
            }
        }

        public void DeletePlayer(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand selectcmd = new NpgsqlCommand();
                    selectcmd.Connection = connection;
                    selectcmd.CommandText = $"SELECT COUNT(*) FROM \"Player\" WHERE \"Id\" = @Id";
                    selectcmd.Parameters.AddWithValue("Id", id);
                    int count = Convert.ToInt32(selectcmd.ExecuteScalar());
                    try
                    {
                        if (count > 0)
                        {
                            NpgsqlCommand deletecmd = new NpgsqlCommand();
                            deletecmd.Connection = connection;
                            deletecmd.CommandText = $"DELETE FROM \"Player\" WHERE \"Id\" = @Id";
                            deletecmd.Parameters.AddWithValue("Id", id);
                            deletecmd.ExecuteNonQuery();
                            connection.Close();

                        }
                    }
                    catch
                    {
                        throw;
                    }
                    
                  
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
