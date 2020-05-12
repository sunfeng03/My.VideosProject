using AutoMapper;
using My.Videos.EntityFramework.Authority;
using My.Videos.Model.Authority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.Common.AutoMap
{
    public class MapperProfile
    {
        public static void InitMapper()
        {
            Mapper.Initialize(x => x.CreateMap<TblUser, User>());
        }
    }
}
