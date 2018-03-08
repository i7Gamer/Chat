﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseNew
{
    public static class ExtensionMethods
    {
        public static IDbDataParameter AddParameter(this IDbCommand command, string parameterName, DbType dbType, object value)
        {
            IDbDataParameter parameter = command.CreateParameter();
            parameter.DbType = dbType;
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            return parameter;
        }
    }
}
