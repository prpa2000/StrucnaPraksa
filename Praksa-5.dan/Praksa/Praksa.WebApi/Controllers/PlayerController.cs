using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using Npgsql;
using System.Security.Cryptography;
using Microsoft.Ajax.Utilities;
using System.Numerics;
using Praksa.Model;
namespace Praksa.WebApi.Controllers
{
    public class PlayerController : ApiController
    {
        private const string connectionString = "Server=127.0.0.1;Port=5432;Database=FootballClub;User Id=postgres;Password=nikolaprpic;";
        List<Player> players = new List<Player>();



        public HttpResponseMessage GetAllPlayers()
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
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
                return Request.CreateResponse(HttpStatusCode.OK, players);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public HttpResponseMessage GetPlayerById(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
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
                        return Request.CreateResponse(HttpStatusCode.OK, player);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid index!");
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [System.Web.Http.HttpPost]
            public HttpResponseMessage CreatePlayer(CreatedPlayer player)
            {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
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
                   
                    return Request.CreateResponse(HttpStatusCode.OK, "Player created!");
                }

                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);

                }
            }


        [System.Web.Http.HttpPut]
        public HttpResponseMessage UpdatePlayer(int id, UpdatedPlayer player)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
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
                    if(count == 0) {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid index!");
                    }

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
                    return Request.CreateResponse(HttpStatusCode.OK, "Player updated!");
                }
                
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public HttpResponseMessage DeletePlayer(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand selectcmd = new NpgsqlCommand();
                    selectcmd.Connection = connection;
                    selectcmd.CommandText = $"SELECT COUNT(*) FROM \"Player\" WHERE \"Id\" = @Id";
                    selectcmd.Parameters.AddWithValue("Id", id);
                    int count = Convert.ToInt32 (selectcmd.ExecuteScalar());
                    if(count == 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid index!");
                    }
                    NpgsqlCommand deletecmd = new NpgsqlCommand();
                    deletecmd.Connection = connection;
                    deletecmd.CommandText = $"DELETE FROM \"Player\" WHERE \"Id\" = @Id";
                    deletecmd.Parameters.AddWithValue("Id", id);
                    deletecmd.ExecuteNonQuery();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Player deleted!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
