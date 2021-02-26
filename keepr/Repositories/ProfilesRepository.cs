using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class ProfilesRepository
    {
        private readonly IDbConnection _db;
        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }
        internal Profile GetById(string id)
        {
            string sql = "SELECT * FROM profiles WHERE id = @id";
            return _db.QueryFirstOrDefault<Profile>(sql, new { id });
        }
        internal Profile Create(Profile newProfile)
        {
            string sql = @"
                INSERT INTO profiles
                (name, picture, email, id)
                VALUES
                (@Name, @Picture, @Email, @id)";
            _db.Execute(sql, newProfile);
            return newProfile;
        }
    }
}