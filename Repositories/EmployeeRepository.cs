using Dapper;
using GameRentalStore.Controllers.Models;
using GameRentalStore.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace GameRentalStore.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionDB;
        public EmployeeRepository()
        {
            _connectionDB = Configuration.ConnectionString;
        }

        public async Task<Employee> GetByEmail(string email)
        {
            var query = @"SELECT 
                            [Id],
                            [FirstName],
                            [LastName],
                            [PhoneNumber],
                            [Email],
                            [Address],
                            [Birthday],
                            [Role],
                            [Password]
                         FROM 
                            EMPLOYEE
                         WHERE 
                            [Email] = @email
                            ";

            using (var connection = new SqlConnection(_connectionDB))
            {
                var employee = await connection.QueryFirstOrDefaultAsync<Employee>(query, param: new
                {
                    email
                });
                return employee;
            }
        }

        public async Task CreateEmployee(Employee employee)
        {
            var query = @"
                            INSERT INTO [Employee] 
                                (
                                    [FirstName], 
                                    [LastName],
                                    [PhoneNumber],
                                    [Email],
                                    [Address],
                                    [Birthday],
                                    [Role],
                                    [Password]
                                  )
                            VALUES 
                                  (
                                    @firstName,
                                    @lastName,
                                    @phoneNumber,
                                    @email,
                                    @address,
                                    @birthday,
                                    @role,
                                    @password)
                            ";

            using (var connection = new SqlConnection(_connectionDB))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                { 
                    try
                    {
                        
                        await connection.ExecuteAsync(query, param: new
                        {
                            firstName = employee.FirstName,
                            lastName = employee.LastName,
                            phoneNumber = employee.PhoneNumber,
                            email = employee.Email,
                            address = employee.Address,
                            birthday = employee.Birthday,
                            role = employee.Role,
                            password = employee.Password
                        }, transaction);
                        transaction.Commit(); 
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        connection.Close();
                    }
                }
            }
        }
    }
}
