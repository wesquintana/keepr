using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    public VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"INSERT INTO vaultkeeps
        (vaultId, keepId, userId)
        VALUES
        (@VaultId, @KeepId, @UserId);
        SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
    }
    public IEnumerable<Keep> GetKeepsByVaultId(int id, string userId)
    {
      string sql = @"SELECT k.* FROM vaultkeeps vk
      INNER JOIN keeps k ON k.id = vk.keepId
      WHERE (vaultId = @id AND vk.userId = @userId)";
      return _db.Query<Keep>(sql, new { id, userId });
    }
    public int DeleteRelationship(int vaultId, int keepId, string userId)
    {
      string sql = @"DELETE FROM vaultkeeps 
      WHERE vaultId=@vaultId AND keepId=@keepId AND userId=@userId";
      return _db.Execute(sql, new { vaultId, keepId, userId });
    }
  }
}