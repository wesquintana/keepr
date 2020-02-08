using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;
namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }
    public VaultKeep Create(VaultKeep newVaultKeep)
    {
      return _repo.Create(newVaultKeep);
    }
    public IEnumerable<Keep> GetKeepsByVaultId(int id, string userId)
    {
      return _repo.GetKeepsByVaultId(id, userId);
    }
    public void DeleteRelationship(int vaultId, int keepId, string userId)
    {
      int i = _repo.DeleteRelationship(vaultId, keepId, userId);
      if (i == 0) { throw new Exception("I can't or won't do that."); }
    }
  }
}