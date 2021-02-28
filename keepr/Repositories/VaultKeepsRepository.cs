using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal VaultKeep Create(VaultKeep vk)
        {
            string sql = @"
            INSERT INTO vaultkeeps
            (creatorId, vaultId, keepId)
            VALUES
            (@CreatorId, @VaultId, @KeepId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, vk);
            vk.Id = id;
            return vk;
        }
        internal VaultKeep GetById(int id)
        {
            string sql = "SELECT * FROM vaultkeeps WHERE id = @id;";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }
        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}