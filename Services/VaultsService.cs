using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Vault> Get(string userId)
    {
      return _repo.Get(userId);
    }
    public Vault GetById(int id, string userId)
    {
      return _repo.GetById(id, userId);
    }

    public Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }
    public Vault Edit(Vault update)
    {
      Vault exists = _repo.GetById(update.Id, update.UserId);
      if (exists == null) { throw new Exception("You can't do that?"); }
      return _repo.Edit(update);
    }
    public void Delete(int id, string userId)
    {
      _repo.Delete(id, userId);
    }
  }
}