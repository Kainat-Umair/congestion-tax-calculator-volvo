using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CongestionTaxCalculator.WebAPI.Respositories.Implementation
{
    public class CongestionTaxRepository : ICongestionTaxRepository
    {
        private readonly IDbConnection dbConnection;
        public CongestionTaxRepository(IDbConnection dbConnection)
        {
           this.dbConnection = dbConnection;
        }

        public int GetTollFee(DateTime date, string city)
        {
             
                string sql = @"SELECT TOP 1 tr.TaxRate 
                    FROM tbl_tax_rates tr
                    INNER JOIN tbl_city c ON tr.CityID = c.ID
                    WHERE c.CityName = @CityName 
                    AND DATEPART(HOUR, tr.DateTime) = DATEPART(HOUR, @DateTime)
                    AND DATEPART(MINUTE, tr.DateTime) <= DATEPART(MINUTE, @DateTime)
                    ORDER BY tr.DateTime DESC;";

                var result=  dbConnection.QueryFirstOrDefault<int?>(sql, new { CityName = city, DateTime = date });
                return (result == null) ? -1 : (int)result;

        }
        public List<DateTime> GetTollFreeDates(string cityName)
        {
           
                
                string sql = @"SELECT tfd.DateTime 
                       FROM tbl_tax_free_dates tfd
                       INNER JOIN tbl_city c ON tfd.CityID = c.ID
                       WHERE c.CityName = @CityName";

                return dbConnection.Query<DateTime>(sql, new { CityName = cityName }).ToList();
            

        }

        public List<string> GetTollFreeVehicles(string city)
        {
            
                
                string sql = @"SELECT VehicleType 
                       FROM tbl_vehicle v
                       INNER JOIN tbl_city c ON v.CityID = c.ID
                       WHERE v.IsTollFreeVehicle = 1 AND c.CityName = @CityName";

              return dbConnection.Query<string>(sql, new { CityName = city }).ToList();
                
            
        }

        public List<string> GetSingleChargeRule(string city)
        {
               
                string sql = @"SELECT CityName 
                       FROM tbl_city 
                       WHERE CityName = @CityName AND IsSingleCharge = 1";
                return dbConnection.Query<string>(sql, new { CityName = city}).ToList();
            
        }
    }
}
