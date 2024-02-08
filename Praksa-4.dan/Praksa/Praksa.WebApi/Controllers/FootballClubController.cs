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
namespace Praksa.WebApi.Controllers
{
   
    public class FootballClubController : ApiController
    {


        private const string CONNECTION_STRING = "Server=127.0.0.1;Port=5432;Database=FootballClub;User Id=postgres;Password=nikolaprpic;";

        NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);


       


        public HttpResponseMessage GetAllClubs()
        {
            try
            {
                List<FootballClub> footballClubs = new List<FootballClub>();
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
        public HttpResponseMessage UpdateFootballClub(int id,  FootballClub footballclub)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"UPDATE \"FootballClub\" SET \"Name\" = @Name, \"NumberOfTrophies\" = @NumberOfTrophies WHERE \"Id\" = @Id";
                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.Parameters.AddWithValue("Name", footballclub.Name);
                    cmd.Parameters.AddWithValue("NumberOfTrophies", footballclub.NumberOfTrophies);
                    cmd.ExecuteNonQuery ();
                    connection.Close();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Club updated!");
            }
            catch(Exception e)
            {
                return Request.CreateResponse (HttpStatusCode.InternalServerError, e.Message);  
            }
        }
        

        public HttpResponseMessage DeleteFootballClub(int id)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"DELETE FROM \"FootballClub\" WHERE \"Id\" = @Id";
                    cmd.Parameters.AddWithValue("Id", id);
                    int rowsAffected;
                    rowsAffected = cmd.ExecuteNonQuery();
                    if(rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Football club deleted!");
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
       
    }
}
