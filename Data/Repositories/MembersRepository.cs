﻿using Data.Contexts;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class MembersRepository(DataContext context): BaseRepository<MemberEntity> (context)
    {

    }
}
