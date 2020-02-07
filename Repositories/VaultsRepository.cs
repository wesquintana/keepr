using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Vault> Get(string userId)
    {
      string sql = "SELECT * FROM vaults WHERE userId =@userId;";
      return _db.Query<Vault>(sql, new { userId });
    }
    internal Vault GetById(int id, string userId)
    {
      string sql = @"SELECT * FROM vaults 
      WHERE id = @id AND (isPrivate = 0 OR userId = @userId);";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id, userId });
    }

    internal Vault Create(Vault VaultData)
    {
      string sql = @"
            INSERT INTO vaults 
            (name, description ) 
            VALUES 
            (@Name, @Description);
            SELECT LAST_INSERT_ID();
            ";
      int id = _db.ExecuteScalar<int>(sql, VaultData);
      VaultData.Id = id;
      return VaultData;
    }
    internal Vault Edit(Vault VaultData)
    {
      string sql = @"UPDATE vaults 
      SET
      name= @Name,
      description=@Description
       WHERE id=@Id AND userId = @UserId;
      ";
      return _db.ExecuteScalar<Vault>(sql, VaultData);
    }
    internal void Delete(int id, string userId)
    {
      string sql = @"DELETE FROM vaults WHERE id=@id AND userId = @userId";
      _db.Execute(sql, new { id, userId });
    }
  }
}