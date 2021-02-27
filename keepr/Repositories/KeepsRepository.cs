using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;
        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal IEnumerable<Keep> GetAll()
        {
            string sql = @"
                SELECT
                keep.*,
                profile.*
                FROM keeps keep
                JOIN profiles profile ON keep.creatorId = profile.id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, splitOn: "id");
        }

        internal int Create(Keep newKeep)
        {
            string sql = @"
                INSERT INTO keeps
                (creatorId, name, description, img, views, shares, keeps)
                VALUES
                (@CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
                SELECT LAST_INSERT_ID()";
            return _db.ExecuteScalar<int>(sql, newKeep);
        }
    }
}