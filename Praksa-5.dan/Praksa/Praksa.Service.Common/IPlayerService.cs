﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praksa.Model;

namespace Praksa.Service.Common
{
    public interface IPlayerService
    {
        List<Player> GetAllPlayers();
        Player GetPlayerById(int id);
        void CreatePlayer(CreatedPlayer player);
        void UpdatePlayer(int id, UpdatedPlayer player);
        void DeletePlayer(int id);
    }
}
