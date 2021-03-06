﻿using HandicapTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.DAL
{
    public class CourseSqlDAO : ICourseDAO
    {
        private const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=GolfAppDB;Trusted_Connection=true;";


        internal static Course RowToObject(SqlDataReader rdr)
        {
            Course course = new Course();

            course.CourseId = Convert.ToInt32(rdr["CourseId"]);
            course.CourseName = Convert.ToString(rdr["CourseName"]);
            course.Par = Convert.ToInt32(rdr["Par"]);
            course.StreetAddress = Convert.ToString(rdr["StreetAddress"]);
            course.City = Convert.ToString(rdr["City"]);
            course.State = Convert.ToString(rdr["State"]);
            course.CountryCode = Convert.ToString(rdr["CountryCode"]);
            course.PostalCode = Convert.ToString(rdr["PostalCode"]);

            return course;
        }
        
    }
}
