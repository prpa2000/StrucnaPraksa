using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Npgsql;
using System.Text;
using System.Threading.Tasks;
using Praksa.Repository.Common;

using System.Net.Http;
using Praksa.Common;
using System.Diagnostics;
using Praksa.Model;
namespace Praksa.Repository
{
    public class FootballClubRepository : IFootballClubRepository
    {
        FootballClubCommon fccommon = new FootballClubCommon();
        
        public async Task<List<FootballClub>> GetAllClubsAsync()
        {
            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {
                List<FootballClub> footballClubs = new List<FootballClub>();
                using (connection)
                {

                    await connection.OpenAsync();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"SELECT * FROM \"FootballClub\" ";
                    NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        FootballClub footballClub = new FootballClub
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            NumberOfTrophies = (int)reader["NumberOfTrophies"]
                        };

                        footballClubs.Add(footballClub);
                    }
                    return footballClubs;

                }
            }

            catch 
            {
                return null;
            }
        }
        public async Task<FootballClub> GetClubByIdAsync(int id)
        {

            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();

                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"SELECT * FROM \"FootballClub\" WHERE \"Id\" = @Id";
                    cmd.Parameters.AddWithValue("Id", id);
                    NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {



                        FootballClub footballclub = new FootballClub()
                        {
                            Id = id,
                            Name = reader["Name"].ToString(),
                            NumberOfTrophies = (int)reader["NumberOfTrophies"]
                        };
                        return footballclub;

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
        public async Task CreateFootballClubAsync(FootballClub footballClub)
        {

            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
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
                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }


                
            }
            catch 
            {
                throw;
            }

        }
        public async Task UpdateFootballClubAsync(int id, FootballClub footballclub)
        {
            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand selectCmd = new NpgsqlCommand();
                    selectCmd.Connection = connection;
                    selectCmd.CommandText = $"SELECT COUNT(*) FROM \"FootballClub\" WHERE \"Id\" = @Id";
                    selectCmd.Parameters.AddWithValue("Id", id);
                    int count = Convert.ToInt32(selectCmd.ExecuteScalarAsync());
                    try
                    {
                        if (count > 0)
                        {
                            NpgsqlCommand updateCmd = new NpgsqlCommand();
                            updateCmd.Connection = connection;
                            updateCmd.CommandText = $"UPDATE \"FootballClub\" SET \"Name\" = @Name, \"NumberOfTrophies\" = @NumberOfTrophies WHERE \"Id\" = @Id";
                            updateCmd.Parameters.AddWithValue("Id", id);
                            updateCmd.Parameters.AddWithValue("Name", footballclub.Name);
                            updateCmd.Parameters.AddWithValue("NumberOfTrophies", footballclub.NumberOfTrophies);
                            await updateCmd.ExecuteNonQueryAsync();
                            await connection.CloseAsync();
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
                
            }
        }
        public async Task DeleteFootballClubAsync(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand selectcmd = new NpgsqlCommand();
                    selectcmd.Connection = connection;
                    selectcmd.CommandText = $"SELECT COUNT(*) FROM \"FootballClub\" WHERE \"Id\" = @Id";
                    selectcmd.Parameters.AddWithValue("Id", id);
                    int count = Convert.ToInt32(selectcmd.ExecuteScalarAsync());
                    try
                    {
                        if (count > 0)
                        {
                            NpgsqlCommand deletecmd = new NpgsqlCommand();
                            deletecmd.Connection = connection;
                            deletecmd.CommandText = $"DELETE FROM \"FootballClub\" WHERE \"Id\" = @Id";
                            deletecmd.Parameters.AddWithValue("Id", id);
                            await deletecmd.ExecuteNonQueryAsync();
                            await connection.CloseAsync();
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
