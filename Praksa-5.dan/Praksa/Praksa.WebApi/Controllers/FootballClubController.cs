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
using Praksa.Model;
namespace Praksa.WebApi.Controllers
{
   
    public class FootballClubController : ApiController
    {


        private const string connectionString = "Server=127.0.0.1;Port=5432;Database=FootballClub;User Id=postgres;Password=nikolaprpic;";
        List<FootballClub> footballClubs = new List<FootballClub>();
        public HttpResponseMessage GetAllClubs()
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"SELECT * FROM \"FootballClub\" ";
                   NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FootballClub footballClub = new FootballClub
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            NumberOfTrophies = (int)reader["NumberOfTrophies"]
                        };

                        footballClubs.Add(footballClub);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, footballClubs);
                    
                }
             }

        catch(Exception e) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }


        }

        public HttpResponseMessage GetClubById(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                   
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"SELECT * FROM \"FootballClub\" WHERE \"Id\" = @Id";
                    cmd.Parameters.AddWithValue("Id", id);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        
                        

                        FootballClub footballclub = new FootballClub()
                        {
                            Id = id,
                            Name = reader["Name"].ToString(),
                            NumberOfTrophies = (int)reader["NumberOfTrophies"]
                        };
                        return Request.CreateResponse(HttpStatusCode.OK, footballclub);
                      
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid index");
                    }
                    
                    
                }
            }


            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        

        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateFootballClub (FootballClub footballClub)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"INSERT INTO \"FootballClub\" (\"Id\", \"Name\", \"NumberOfTrophies\") VALUES (@Id, @Name, @NumberOfTrophies)";
                    cmd.Parameters.AddWithValue("Id", footballClub.Id);
                    cmd.Parameters.AddWithValue("Name", footballClub.Name);
                    cmd.Parameters.AddWithValue("NumberOfTrophies", footballClub.NumberOfTrophies);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

               
                return Request.CreateResponse(HttpStatusCode.OK, "Club created!");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }

        }
        



        [System.Web.Http.HttpPut]
        public HttpResponseMessage UpdateFootballClub(int id, FootballClub footballclub)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand selectCmd = new NpgsqlCommand();
                    selectCmd.Connection = connection;
                    selectCmd.CommandText = $"SELECT COUNT(*) FROM \"FootballClub\" WHERE \"Id\" = @Id";
                    selectCmd.Parameters.AddWithValue("Id", id);
                    int count = Convert.ToInt32(selectCmd.ExecuteScalar());
                    if (count == 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid index.");
                    }

                    NpgsqlCommand updateCmd = new NpgsqlCommand();
                    updateCmd.Connection = connection;
                    updateCmd.CommandText = $"UPDATE \"FootballClub\" SET \"Name\" = @Name, \"NumberOfTrophies\" = @NumberOfTrophies WHERE \"Id\" = @Id";
                    updateCmd.Parameters.AddWithValue("Id", id);
                    updateCmd.Parameters.AddWithValue("Name", footballclub.Name);
                    updateCmd.Parameters.AddWithValue("NumberOfTrophies", footballclub.NumberOfTrophies);
                    updateCmd.ExecuteNonQuery();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Club updated!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        public HttpResponseMessage DeleteFootballClub(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand selectcmd = new NpgsqlCommand();
                    selectcmd.Connection = connection;
                    selectcmd.CommandText = $"SELECT COUNT(*) FROM \"FootballClub\" WHERE \"Id\" = @Id";
                    selectcmd.Parameters.AddWithValue("Id", id);
                    int count = Convert.ToInt32(selectcmd.ExecuteScalar());
                    if(count == 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid index!");
                    }
                    
                    NpgsqlCommand deletecmd = new NpgsqlCommand();
                    deletecmd.Connection = connection;
                    deletecmd.CommandText = $"DELETE FROM \"FootballClub\" WHERE \"Id\" = @Id";
                    deletecmd.Parameters.AddWithValue("Id", id);
                    deletecmd.ExecuteNonQuery();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Club deleted!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
       
    }
}
