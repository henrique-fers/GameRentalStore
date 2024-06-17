using Dapper;
using GameRentalStore.Controllers.Models;
using GameRentalStore.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace GameRentalStore.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly string _connectionDB;
        public PromotionRepository()
        {
            _connectionDB = Configuration.ConnectionString;
        }


        public async Task<List<Promotion>> GetAllPromotion()
        {
            var query = @"SELECT  
                          	[Id], 
                            [Code],
                            [Value],
                          	[Description], 
                          	[Validation], 
                          	[IdPromotionType]  
                          FROM
                          	[dbo].[Promotion]";

            using (var connection = new SqlConnection(_connectionDB))
            {
                var promotions = await connection.QueryAsync<Promotion>(query);
                return promotions.ToList();
            }
        }

        public async Task<Promotion> GetPromotionForDescription(string description)
        {
            var query = @"SELECT  
                          	[Id], 
                            [Code],
                            [Value],
                          	[Description], 
                          	[Validation], 
                          	[IdPromotionType]  
                          FROM
                          	[dbo].[Promotion]
                          Where Description = @description";

            using (var connection = new SqlConnection(_connectionDB))
            {
                var promotion = await connection.QueryFirstOrDefaultAsync<Promotion>(query, param: new {
                    description = description
                });
                return promotion;
            }
        }

        public async Task<Promotion> GetPromotionById(int id)
        {
            try { 
            var query = @"SELECT  
                          	[Id], 
                          	[Code],
                            [Value],
                            [Description], 
                          	[Validation], 
                          	[IdPromotionType]  
                          FROM
                          	[dbo].[Promotion]
                          WHERE Id = @id";

            using (var connection = new SqlConnection(_connectionDB))
            {
                var promotion = await connection.QueryFirstOrDefaultAsync<Promotion>(query, param: new
                {
                    id = id.ToString()
                });
                return promotion;
            }
            }catch(Exception ex) {
                return null;
            }
        }

        public async Task<List<Promotion>> GetAllValidsPromotion()
        {
            var query = @"SELECT  
                          	[Id], 
                            [Code],
                            [Value],
                          	[Description], 
                          	[Validation], 
                          	[IdPromotionType]  
                          FROM
                          	[dbo].[Promotion]
                          WHERE [Validation] < GETDATE()";

            using (var connection = new SqlConnection(_connectionDB))
            {
                var promotions = await connection.QueryAsync<Promotion>(query);
                return promotions.ToList();
            }
        }

        public async Task CreatePromotion(Promotion promotion)
        {
            var query = @"INSERT INTO [Promotion] 
                        OUTPUT INSERTED.Id 
                        VALUES (@Code,@Value, @Description, @Validation, @PromotionType)";

            using (var connection = new SqlConnection(_connectionDB))
            {
                using (var transaction = connection.BeginTransaction())
                    try
                    {
                        await connection.ExecuteAsync(query, param: new
                        {
                            Code = promotion.Code,
                            Value = promotion.Value,
                            Description = promotion.Description,
                            Validation = promotion.Validation,
                            PromotionType = promotion.PromotionType,
                        });

                        transaction.Commit();
                    }
                    catch (Exception ex) { transaction.Rollback(); }
                    finally { connection.Close(); }
            }
        }

        public async Task DeletePromotion(int id)
        {
            var query = @"DELETE FROM [Promotion] WHERE Id = @id";

            using (var connection = new SqlConnection(_connectionDB)){
                using (var transaction = connection.BeginTransaction())
                try
                {
                    await connection.ExecuteAsync(query, param: new
                    {
                        id = id
                    });

                    transaction.Commit();
                }
                catch (Exception ex) { transaction.Rollback(); }
                finally { connection.Close(); }
            }
        }

        public async Task UpdatePromotion(Promotion promotion)
        {
            var query = @"UPDATE
                        	[Promotion]
                        SET 
                            [Code] = @Code,
                            [Value] = @Value,
                        	[Description] = @description, 
                        	[Validation] = @validation, 
                        	[IdPromotionType] = @idPromotionType
                        WHERE 
                        	Id = @id";

            using (var connection = new SqlConnection(_connectionDB))
            {
                using (var transaction = connection.BeginTransaction())
                    try
                    {
                        await connection.ExecuteAsync(query, param: new
                        {
                            
                            Code = promotion.Code,
                            Value = promotion.Value,
                            description = promotion.Description,
                            validation = promotion.Validation,
                            idPromotionType = promotion.PromotionType,
                            id = promotion.Id
                        });

                        transaction.Commit();
                    }
                    catch (Exception ex) { transaction.Rollback(); }
                    finally { connection.Close(); }
            }
        }
    }
}
