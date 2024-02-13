using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Npgsql;
using System.Text;
using System.Threading.Tasks;
using Praksa.Repository.Common;

using System.Net.Http;

using System.Diagnostics;
using Praksa.Model;
using Praksa.Common;
namespace Praksa.Repository
{
    public class FootballClubRepository : IFootballClubRepository
    {
        const string connectionString = "Server=127.0.0.1;Port=5432;Database=FootballClub;User Id=postgres;Password=nikolaprpic;";

        public async Task<List<FootballClub>> GetAllClubsAsync(Paging paging, Sorting sorting, FootballClubFiltering filters)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                List<FootballClub> footballClubs = new List<FootballClub>();
                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;

                    StringBuilder queryBuilder = new StringBuilder("SELECT * FROM \"FootballClub\" WHERE 1=1");

                    if (!string.IsNullOrEmpty(filters.Name))
                    {
                        queryBuilder.Append($" AND \"Name\" LIKE @Name");
                        cmd.Parameters.AddWithValue("@Name", $"%{filters.Name}%");
                    }
                    if (filters.NumberOfTrophies.HasValue)
                    {
                        queryBuilder.Append($" AND \"NumberOfTrophies\" = @NumberOfTrophies");
                        cmd.Parameters.AddWithValue("@NumberOfTrophies", $"{filters.NumberOfTrophies}");
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

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
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
        public async Task<HttpResponseMessage> CreateFootballClubAsync(FootballClub footballClub)
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
                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    await connection.CloseAsync();

                }


                return new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent("Football club created successfully.")
                };
            }

            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }

        }
        public async Task<HttpResponseMessage> UpdateFootballClubAsync(int id, FootballClub footballclub)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                await connection.OpenAsync();

                NpgsqlCommand selectCmd = new NpgsqlCommand();
                selectCmd.Connection = connection;
                selectCmd.CommandText = $"SELECT COUNT(*) FROM \"FootballClub\" WHERE \"Id\" = @Id";
                selectCmd.Parameters.AddWithValue("Id", id);
                int count = Convert.ToInt32(await selectCmd.ExecuteScalarAsync());

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

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Football club updated!")
                    };
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Football club not found.")
                    };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }
        }

        public async Task<HttpResponseMessage> DeleteFootballClubAsync(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                await connection.OpenAsync();

                NpgsqlCommand selectcmd = new NpgsqlCommand();
                selectcmd.Connection = connection;
                selectcmd.CommandText = $"SELECT COUNT(*) FROM \"FootballClub\" WHERE \"Id\" = @Id";
                selectcmd.Parameters.AddWithValue("Id", id);
                int count = Convert.ToInt32(await selectcmd.ExecuteScalarAsync());

                if (count > 0)
                {
                    NpgsqlCommand deletecmd = new NpgsqlCommand();
                    deletecmd.Connection = connection;
                    deletecmd.CommandText = $"DELETE FROM \"FootballClub\" WHERE \"Id\" = @Id";
                    deletecmd.Parameters.AddWithValue("Id", id);
                    await deletecmd.ExecuteNonQueryAsync();
                    await connection.CloseAsync();

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Football club deleted!")
                    };
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Football club not found.")
                    };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }
        }

    }
}
            


  