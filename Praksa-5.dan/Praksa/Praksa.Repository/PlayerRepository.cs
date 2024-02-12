using Npgsql;
using Praksa.Common;
using Praksa.Model;
using Praksa.Repository.Common;

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
        public async Task<List<Player>> GetAllPlayersAsync()
        {
            List<Player> players = new List<Player>();
            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {

                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"SELECT * FROM \"FootballClub\" INNER JOIN \"Player\" ON \"FootballClub\".\"Id\" = \"Player\".\"FootballClubId\"";
                    NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int id = (int)reader["Id"];
                        int? footballclubid = reader["FootballClubId"] as int?;
                        string firstname = reader["FirstName"] as string;
                        string lastname = reader["LastName"] as string;
                        int? age = reader["Age"] as int?;
                        FootballClub footballclub = null;
                        NpgsqlCommand cmdclub = new NpgsqlCommand();
                        cmdclub.Connection = connection;
                        NpgsqlDataReader clubReader = await cmdclub.ExecuteReaderAsync();

                        if (await clubReader.ReadAsync())
                        {
                            footballclub = new FootballClub
                            {
                                Id = (int)clubReader["Id"],
                                Name = clubReader["Name"] as string,
                                NumberOfTrophies = (int)clubReader["NumberOfTrophies"]
                            };
                        }
                        await clubReader.CloseAsync();

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
                    await reader.CloseAsync();
                    await cmd.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
                return players;
            }
            catch 
            {
                return null;
            }
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"SELECT * FROM \"Player\" WHERE \"Id\" = @Id";
                    cmd.Parameters.AddWithValue("Id", id);
                    NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        int? footballclubid = reader["FootballClubId"] as int?;
                        string firstname = reader["FirstName"] as string;
                        string lastname = reader["LastName"] as string;
                        int? age = reader["Age"] as int?;

                        FootballClub footballclub = null;
                        NpgsqlCommand cmdclub = new NpgsqlCommand();
                        cmdclub.Connection = connection;
                        NpgsqlDataReader clubReader = await cmdclub.ExecuteReaderAsync();

                        if (await clubReader.ReadAsync())
                        {
                            footballclub = new FootballClub
                            {
                                Id = (int)clubReader["Id"],
                                Name = clubReader["Name"] as string,
                                NumberOfTrophies = (int)clubReader["NumberOfTrophies"]
                            };
                        }
                        await clubReader.CloseAsync();

                        Player player = new Player()
                        {
                            Id = id,
                            FootballClubId = footballclubid.Value,
                            FirstName = firstname,
                            LastName = lastname,
                            Age = age.Value,
                            FootballClub = footballclub,
                        };

                        await reader.CloseAsync();
                        await cmd.ExecuteNonQueryAsync();
                        await connection.CloseAsync();
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

        public async Task CreatePlayerAsync(CreatedPlayer player)
        {
            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"INSERT INTO \"Player\" (\"Id\", \"FootballClubId\", \"FirstName\", \"LastName\", \"Age\") VALUES (@Id, @FootballClubId, @FirstName, @LastName, @Age)";
                    cmd.Parameters.AddWithValue("Id", player.Id);
                    cmd.Parameters.AddWithValue("FootballClubId", player.FootballClubId);
                    cmd.Parameters.AddWithValue("FirstName", player.FirstName);
                    cmd.Parameters.AddWithValue("LastName", player.LastName);
                    cmd.Parameters.AddWithValue("Age", player.Age);
                    await cmd.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }

                
            }

            catch 
            {
                throw;

            }
        }

        public async Task UpdatePlayerAsync(int id, UpdatedPlayer player)
        {
            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand selectcmd = new NpgsqlCommand();
                    selectcmd.Connection = connection;
                    selectcmd.CommandText = $"SELECT COUNT(*) FROM \"Player\" WHERE \"Id\" = @Id";
                    selectcmd.Parameters.AddWithValue("Id", id);
                    int count = Convert.ToInt32(selectcmd.ExecuteScalarAsync());
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
                            await updatecmd.ExecuteNonQueryAsync();
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

        public async Task DeletePlayerAsync(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(fccommon.ConnectionString);
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand selectcmd = new NpgsqlCommand();
                    selectcmd.Connection = connection;
                    selectcmd.CommandText = $"SELECT COUNT(*) FROM \"Player\" WHERE \"Id\" = @Id";
                    selectcmd.Parameters.AddWithValue("Id", id);
                    int count = Convert.ToInt32(selectcmd.ExecuteScalarAsync());
                    try
                    {
                        if (count > 0)
                        {
                            NpgsqlCommand deletecmd = new NpgsqlCommand();
                            deletecmd.Connection = connection;
                            deletecmd.CommandText = $"DELETE FROM \"Player\" WHERE \"Id\" = @Id";
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
