using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Keep> Get()
    {
      string sql = "SELECT * FROM keeps WHERE isPrivate = 0;";
      return _db.Query<Keep>(sql);
    }
    internal Keep GetById(int id, string userId)
    {
      string sql = @"SELECT * FROM keeps 
      WHERE id = @id AND (isPrivate = 0 OR userId = @userId);";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id, userId });
    }
    internal IEnumerable<Keep> GetByUser(string userId)
    {
      string sql = @"SELECT * FROM keeps WHERE userId=@userId;";
      return _db.Query<Keep>(sql, new { userId });
    }

    internal Keep Create(Keep KeepData)
    {
      string sql = @"
            INSERT INTO keeps 
            (name, description, img, isPrivate, views, shares, keeps, userId) 
            VALUES 
            (@Name, @Description, @Img, @IsPrivate, @Views, @Shares, @Keeps, @UserId);
            SELECT LAST_INSERT_ID();
            ";
      int id = _db.ExecuteScalar<int>(sql, KeepData);
      KeepData.Id = id;
      return KeepData;
    }
    internal int Edit(Keep KeepData)
    {
      string sql = @"UPDATE keeps 
      SET
      name= @Name,
      description=@Description,
       img= @Img,
       isPrivate=@IsPrivate,
       views=@Views,
       shares=@Shares,
       keeps=@Keeps
       WHERE (id=@Id AND userId = @UserId);
      ";
      return _db.Execute(sql, KeepData);
    }
    internal int ViewKeep(int id)
    {
      string sql = @"UPDATE keeps
      SET
      views=views+1
      WHERE id=@id;
      SELECT views FROM keeps WHERE id=@id";
      return _db.ExecuteScalar<int>(sql, new { id });
    }
    internal int KeepKeep(int id)
    {
      string sql = @"UPDATE keeps
      SET
      keeps=keeps+1
      WHERE id=@id;
      SELECT keeps FROM keeps WHERE id=@id";
      return _db.ExecuteScalar<int>(sql, new { id });
    }
    internal int Delete(int id, string userId)
    {
      string sql = @"DELETE FROM keeps WHERE id=@id AND userId = @userId";
      return _db.Execute(sql, new { id, userId });
    }
  }
}