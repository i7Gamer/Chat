using Dapper.FluentMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class DapperConfiguration
    {
        private static bool mapped;

        public static void init()
        {
            if (mapped)
            {
                return;
            }
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UserMapper());
                config.AddMap(new ChatMapper());
                config.AddMap(new ChatMessageMapper());
                config.AddMap(new ChatMemberMapper());
            });
        }
    }
}
