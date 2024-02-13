using Npgsql;

using Praksa.Model;
using Praksa.Repository.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Praksa.Repository;
using Praksa.Common;
namespace Praksa.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        const string connectionString = "Server=127.0.0.1;Port=5432;Database=FootballClub;User Id=postgres;Password=nikolaprpic;";

        IFootballClubRepository footballClubRepository;
        public PlayerRepository(IFootballClubRepository footballClubRepository)
        {
            this.footballClubRepository = footballClubRepository;
        }

        public async Task<List<Player>> GetAllPlayersAsync(Paging paging, Sorting sorting, PlayerFiltering filters)
        {
            List<Player> players = new List<Player>();
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                StringBuilder queryBuilder = new StringBuilder("SELECT * FROM \"FootballClub\" INNER JOIN \"Player\" ON \"FootballClub\".\"Id\" = \"Player\".\"FootballClubId\" WHERE 1=1");

                if (!string.IsNullOrEmpty(filters.FirstName))
                {
                    queryBuilder.Append($" AND \"FirstName\" LIKE @FirstName");
                    cmd.Parameters.AddWithValue("@FirstName", $"%{filters.FirstName}%");
                }

                if (!string.IsNullOrEmpty(filters.LastName))
                {
                    queryBuilder.Append($" AND \"LastName\" LIKE @LastName");
                    cmd.Parameters.AddWithValue("@LastName", $"%{filters.LastName}%");
                }

                if (filters.Age.HasValue)
                {
                    queryBuilder.Append($" AND \"Age\" = @Age");
                    cmd.Parameters.AddWithValue("@Age", $"{filters.Age}");
                }
                if (filters.FootballClubId.HasValue)
                {
                    queryBuilder.Append($" AND \"FootballClubId\" = @FootballClubId");
                    cmd.Parameters.AddWithValue("@FootballClubId", $"{filters.FootballClubId}");
                }

                if (!string.IsNullOrEmpty(sorting.SortBy))
                {
                    queryBuilder.Append($" ORDER BY \"{sorting.SortBy}\" {sorting.SortOrder}");
                }

                if (paging.PageNumber >= 0 && paging.PageSize > 0)
                {
                    queryBuilder.Append($" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");
                    cmd.Parameters.AddWithValue("@Offset", (paging.PageNumber - 1) * paging.PageSize);
                    cmd.Parameters.AddWithValue("@PageSize", paging.PageSize);
                }

                cmd.CommandText = queryBuilder.ToString();

                using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int id = (int)reader["Id"];
                        int? footballclubid = reader["FootballClubId"] as int?;
                        string firstname = reader["FirstName"] as string;
                        string lastname = reader["LastName"] as string;
                        int? age = reader["Age"] as int?;

                        Player player = new Player()
                        {
                            Id = id,
                            FootballClubId = footballclubid.Value,
                            FirstName = firstname,
                            LastName = lastname,
                            Age = age.Value,
                            FootballClub = null
                        };
                        players.Add(player);
                    }
                }

                foreach (Player player in players)
                {
                    if (player.FootballClubId.HasValue)
                    {
                        player.FootballClub = await footballClubRepository.GetClubByIdAsync(player.FootballClubId.Value);
                    }
                }

                await connection.CloseAsync();
                return players;
            }
            catch
            {
                return new List<Player>();
            }
        }



        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
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

                        if (footballclubid.HasValue)
                        {
                            footballclub = await footballClubRepository.GetClubByIdAsync(footballclubid.Value);
                        }

                        Player player = new Player()
                        {
                            Id = id,
                            FootballClubId = footballclubid ?? 0,
                            FirstName = firstname,
                            LastName = lastname,
                            Age = age ?? 0,
                            FootballClub = footballclub,
                        };

                        await reader.CloseAsync();
                        return player;
                    }
                    else
                    {
                        await reader.CloseAsync();
                        return null;
                    }
                }
                catch
                {
             
                    return null;
                }
            }
        }




        public async Task<HttpResponseMessage> CreatePlayerAsync(CreatedPlayer player)
        {
            if(player == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
                
            }
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
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

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Player created")
                };
            }

            catch(Exception e) 
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message)
                };

            }
        }

        public async Task<HttpResponseMessage> UpdatePlayerAsync(int id, UpdatedPlayer player)
        {
            if(player == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
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
                        return new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent("Player updated!")
                        };
                    }
                    catch(Exception ex)
                    {
                       return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
                    }

                    
                   
                }

            }
            catch 
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> DeletePlayerAsync(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
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

                        return new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent("Player deleted!")
                        };
                    }
                    catch(Exception ex)
                    {
                        return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent(ex.Message)
                        };
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
