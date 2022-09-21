﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplate.Application.Common.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get;}
    }
}
